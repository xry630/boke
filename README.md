# 视频管理系统

一个完整的视频管理解决方案，包含 .NET Core 后端和 Vue 3 前端。

## 项目结构

```
.
├── video-master/          # .NET Core 后端
│   └── video-master/      # ASP.NET Core 应用
│       ├── src/
│       │   ├── Video.Domain/              # 领域模型
│       │   ├── Video.Domain.Shared/       # 共享定义
│       │   ├── Video.Application/         # 应用服务层
│       │   ├── Video.Application.Contract/ # 服务契约
│       │   ├── Video.EntityFrameworkCore/ # EF Core 持久层
│       │   └── Video.HttpApi.Host/        # HTTP API 主机
│       └── ...
│
└── frontend/              # Vue 3 前端
    ├── src/
    │   ├── api/          # API 接口
    │   ├── components/   # 组件
    │   ├── router/       # 路由
    │   ├── stores/       # 状态管理
    │   ├── utils/        # 工具函数
    │   └── views/        # 页面视图
    ├── package.json
    └── vite.config.js
```

## 技术栈

### 后端
- **框架**: ASP.NET Core 6.0
- **架构**: 分层架构（领域驱动设计）
- **ORM**: Entity Framework Core
- **数据库**: MySQL
- **缓存**: Redis (FreeRedis)
- **认证**: JWT Bearer
- **日志**: Serilog
- **文档**: Swagger/OpenAPI

### 前端
- **框架**: Vue 3
- **路由**: Vue Router 4
- **状态管理**: Pinia
- **HTTP客户端**: Axios
- **构建工具**: Vite
- **UI**: 自定义响应式设计

## 功能特性

### 已实现功能
- ✅ 用户注册（含验证码）
- ✅ 用户登录
- ✅ JWT 认证
- ✅ 角色权限管理
- ✅ 视频发布与审核
- ✅ 视频分类管理
- ✅ 文件上传/删除
- ✅ 关注/取关功能

### 前端功能
- ✅ 响应式登录界面
- ✅ 注册页面（含验证码验证）
- ✅ JWT Token 自动管理
- ✅ 路由守卫
- ✅ 用户信息展示

## 快速开始

### 前置要求
- .NET 6.0 SDK
- Node.js 16+
- MySQL 5.7+
- Redis (可选)

### 1. 启动后端

```bash
cd video-master/video-master/src/Video.HttpApi.Host
dotnet restore
dotnet run
```

后端将运行在 `http://localhost:5000`

### 2. 启动前端

```bash
cd frontend
npm install
npm run dev
```

前端将运行在 `http://localhost:5173`

### 3. 访问应用

打开浏览器访问 `http://localhost:5173`

## 详细文档

- [前端使用指南](./FRONTEND_GUIDE.md) - 前端系统详细说明
- [前端中文说明](./frontend/使用说明.md) - 中文使用教程
- [前端 README](./frontend/README.md) - 前端项目说明

## API 文档

启动后端后，访问 Swagger 文档: `http://localhost:5000/swagger`

主要 API 端点：

### 认证
- `POST /api/Auth` - 用户登录
- `POST /api/Auth/register` - 用户注册

### 验证码
- `GET /api/Code` - 获取验证码

### 用户管理
- `GET /api/UserInfo` - 获取用户信息
- `PUT /api/UserInfo` - 更新用户信息

### 视频管理
- `GET /api/Video` - 获取视频列表
- `POST /api/Video` - 发布视频

### 文件管理
- `POST /api/File` - 上传文件
- `DELETE /api/File/{id}` - 删除文件

## 开发指南

### 后端开发

```bash
# 还原依赖
dotnet restore

# 运行
dotnet run

# 构建
dotnet build

# 发布
dotnet publish -c Release
```

### 前端开发

```bash
# 安装依赖
npm install

# 开发模式
npm run dev

# 生产构建
npm run build

# 预览构建
npm run preview
```

## 部署

### 使用 Docker

```bash
# 构建前端镜像
cd frontend
docker build -t video-frontend .

# 运行前端容器
docker run -d -p 8080:80 video-frontend

# 使用 docker-compose
docker-compose up -d
```

### 使用 Nginx

参考 `frontend/nginx.conf.example` 配置文件。

## 配置

### 后端配置

编辑 `appsettings.json`:
- 数据库连接字符串
- Redis 连接
- JWT 配置
- CORS 设置

### 前端配置

编辑 `frontend/vite.config.js`:
- API 代理地址
- 端口配置

## 环境变量

### 前端
```bash
# .env.development
VITE_API_BASE_URL=/api

# .env.production
VITE_API_BASE_URL=/api
```

## 贡献指南

欢迎提交 Issue 和 Pull Request！

1. Fork 本仓库
2. 创建特性分支 (`git checkout -b feature/AmazingFeature`)
3. 提交更改 (`git commit -m 'Add some AmazingFeature'`)
4. 推送到分支 (`git push origin feature/AmazingFeature`)
5. 开启 Pull Request

## 常见问题

### Q: 如何解决 CORS 跨域问题？
A: 前端开发环境已配置代理，生产环境使用 Nginx 反向代理。

### Q: Token 在哪里存储？
A: Token 存储在浏览器的 localStorage 中。

### Q: 如何修改后端 API 地址？
A: 编辑 `frontend/vite.config.js` 中的 proxy 配置。

### Q: 数据库如何初始化？
A: 参考后端项目的迁移文件和初始化脚本。

## 许可证

MIT License

## 联系方式

如有问题，请提交 Issue 或联系开发团队。

---

**Happy Coding!** 🚀
