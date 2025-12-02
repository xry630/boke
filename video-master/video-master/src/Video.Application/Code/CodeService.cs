using FreeRedis;
using Video.Application.Contract.Code;
using Video.Application.Contract.Code.Dtos;
using Video.Domain.Shared;

namespace Video.Application.Code;

public class CodeService : ICodeService
{
    private readonly RedisClient _redisClient;

    public CodeService(RedisClient redisClient)
    {
        _redisClient = redisClient;
    }

    public async Task<string> GetAsync(CodeInput input)
    {
        try
        {
            var value = new Random().Next(9999).ToString("0000");
            var key = $"{input.Type}:{input.Value}";
            
            await _redisClient.SetExAsync(key, 60, value);

            // For debugging: log the key and value
            Console.WriteLine($"Verification code stored: Key={key}, Value={value}");

            return value;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Redis error in CodeService: {ex.Message}");
            throw new BusinessException("Redis服务不可用，请稍后重试");
        }
    }
}