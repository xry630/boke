# Boke 项目仓库

这是一个按功能分类管理的多项目仓库，包含视频管理系统及相关组件。

## 项目结构

```
.
├── backend/               # 后端项目
│   └── video-master/      # .NET Core 视频管理后端
│       ├── src/
│       │   ├── Video.Domain/              # 领域模型
│       │   ├── Video.Domain.Shared/       # 共享定义
│       │   ├── Video.Application/         # 应用服务层
│       │   ├── Video.Application.Contract/ # 服务契约
│       │   ├── Video.EntityFrameworkCore/ # EF Core 持久层
│       │   └── Video.HttpApi.Host/        # HTTP API 主机
│       └── ...
│
├── frontend/              # 前端项目
│   ├── src/
│   │   ├── api/          # API 接口
│   │   ├── components/   # 组件
│   │   ├── router/       # 路由
│   │   ├── stores/       # 状态管理
│   │   ├── utils/        # 工具函数
│   │   └── views/        # 页面视图
│   ├── package.json
│   └── vite.config.js
│
├── docs/                 # 文档
│   ├── README.md                     # 项目总体说明
│   ├── FRONTEND_GUIDE.md            # 前端系统指南
│   ├── PROJECT_OVERVIEW.md          # 项目概览
│   ├── QUICKSTART.md                # 快速开始
│   ├── VERSION.md                   # 版本信息
│   ├── 使用说明.md                   # 中文使用教程
│   └── 项目检查.md                   # 项目检查清单
│
├── configs/              # 配置文件
│   └── PROJECT_SUMMARY.txt         # 项目总结
│
├── deployment/           # 部署相关
│   ├── docker-compose.yml           # Docker Compose 配置
│   ├── Dockerfile                   # Docker 镜像构建
│   ├── nginx.conf.example           # Nginx 配置示例
│   ├── DEPLOYMENT.md                # 部署指南
│   └── 启动脚本.sh                   # 启动脚本
│
└── .gitignore           # Git 忽略文件
```

## 功能分类

### 🖥️ 后端项目 (backend/)
- **video-master**: ASP.NET Core 6.0 视频管理后端
  - 分层架构设计
  - Entity Framework Core + MySQL
  - Redis 缓存
  - JWT 认证
  - Swagger API 文档

### 🌐 前端项目 (frontend/)
- **Vue 3 应用**: 现代化前端界面
  - Vue Router 4 路由管理
  - Pinia 状态管理
  - Axios HTTP 客户端
  - 响应式设计

### 📚 文档 (docs/)
- 项目说明文档
- 使用指南
- 部署教程
- API 文档链接

### ⚙️ 配置 (configs/)
- 项目配置文件
- 环境设置
- 构建配置

### 🚀 部署 (deployment/)
- Docker 容器化配置
- Nginx 反向代理配置
- 部署脚本和指南

## 快速开始

### 1. 启动后端

```bash
cd backend/video-master/src/Video.HttpApi.Host
dotnet restore
dotnet run
```

### 2. 启动前端

```bash
cd frontend
npm install
npm run dev
```

### 3. 访问应用

- 前端应用: http://localhost:5173
- 后端 API: http://localhost:5000
- API 文档: http://localhost:5000/swagger

## 技术栈

### 后端
- ASP.NET Core 6.0
- Entity Framework Core
- MySQL + Redis
- JWT Bearer 认证
- Serilog 日志

### 前端
- Vue 3 + Composition API
- Vue Router 4
- Pinia 状态管理
- Axios + Vite

## 添加新项目

当需要添加新的不同项目时，请按以下规则组织：

1. **后端项目** → `backend/` 目录
2. **前端项目** → `frontend/` 目录  
3. **移动端项目** → `mobile/` 目录
4. **工具项目** → `tools/` 目录
5. **共享库** → `shared/` 目录

每个项目应包含独立的 README.md 说明文档。

## 贡献指南

1. Fork 本仓库
2. 在相应功能目录下开发
3. 提交 Pull Request
4. 遵循现有代码规范

---

**Happy Coding!** 🚀
