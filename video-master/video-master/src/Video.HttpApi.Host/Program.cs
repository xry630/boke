using FreeRedis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
using Video.Application;
using Video.Domain;
using Video.HttpApi.Host.Filters;
using Video.HttpApi.Host.Options;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .ReadFrom.Configuration(new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",// 根据环境变量加载指定配置 
                     optional: true).Build())
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/log/", "log"),
                               rollingInterval: RollingInterval.Day)) // 写入日志到文件 
    .WriteTo.Async(c => c.Console())
    .CreateLogger();

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    Args= args,
    ContentRootPath = AppContext.BaseDirectory
});

// 使用Serilog
builder.Host.UseSerilog();
#region Swagger

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1.0", new OpenApiInfo
    {
        Version = "v1.0",  // 版本
        Title = "Video Api 文档",  // 标题
        Description = "Video Api 文档", // 描述 
        Contact = new OpenApiContact
        {
            Name = "Simple",  // 作者
            Email = "239573049@qq.com", // 邮箱
            Url = new Uri("https://gitee.com/hejiale010426")  // 可以放Github地址
        }
    });

    // 加载xml文档 显示Swagger的注释
    string[] files = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");//获取api文档 
    string[] array = files;
    foreach (string filePath in array)
    {
        option.IncludeXmlComments(filePath, includeControllerXmlComments: true);
    }

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                                              {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                                }
                        },
                        Array.Empty<string>()
                        }
                });

    // 添加Authorization的输入框
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value,Format: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
});

#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddVideoApplication();

// 添加过滤器
builder.Services.AddMvcCore(options =>
{
    options.Filters.Add<ResponseFilter>();
    options.Filters.Add<ExceptionFilter>();
});

#region Options
var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
var jwtsection = configuration.GetSection(nameof(JWTOptions));
builder.Services.Configure<JWTOptions>(jwtsection);

var fileOptions = configuration.GetSection(nameof(VideoFileOptions));
builder.Services.Configure<VideoFileOptions>(fileOptions);

#endregion
var jwt = jwtsection.Get<JWTOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, //是否在令牌期间验证签发者 
            ValidateAudience = true, //是否验证接收者 
            ValidateLifetime = true, //是否验证失效时间 
            ValidateIssuerSigningKey = true, //是否验证签名 
            ValidAudience = jwt.Audience, //接收者 
            ValidIssuer = jwt.Issuer, //签发者，签发的Token的人 
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey!)) // 密钥 
        };
    });

// 注入redis
builder.Services.AddSingleton(new RedisClient(configuration["RedisConnection"]));

// 注入efcore服务
builder.Services.AddVideoEntityFrameworkCore();

var app = builder.Build();

#region Swagger

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Video Web Api");
    c.DocExpansion(DocExpansion.None);
    c.DefaultModelsExpandDepth(-1);
    c.RoutePrefix = string.Empty;
});

#endregion

app.UseAuthentication(); // 认证 
app.UseAuthorization(); // 授权

var provider = new FileExtensionContentTypeProvider();

provider.Mappings[".exe"] = "application/octet-stream";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});
app.MapControllers();

app.Run();
