# Vue 前端系统对接指南

## 项目概述

本项目为 .NET Core 视频管理系统提供了完整的 Vue 3 前端界面，实现了用户登录、注册等核心功能。

## 快速开始

### 1. 启动后端服务

首先确保 .NET 后端服务已启动：

```bash
cd video-master/video-master/src/Video.HttpApi.Host
dotnet run
```

后端服务默认运行在 `http://localhost:5000`

### 2. 启动前端服务

在新的终端窗口中：

```bash
cd frontend
npm install    # 首次运行需要安装依赖
npm run dev    # 启动开发服务器
```

前端服务将运行在 `http://localhost:5173`

### 3. 访问应用

在浏览器中打开 `http://localhost:5173`

## 功能演示

### 注册新用户

1. 访问 http://localhost:5173/register
2. 填写表单：
   - **昵称**: 输入显示名称（例如：张三）
   - **用户名**: 输入登录用户名（例如：zhangsan）
   - 点击"获取验证码"，系统会返回验证码
   - **验证码**: 输入获取到的验证码
   - **密码**: 输入密码
   - **确认密码**: 再次输入密码
3. 点击"注册"按钮
4. 注册成功后自动登录并跳转到首页

### 用户登录

1. 访问 http://localhost:5173/login
2. 输入用户名和密码
3. 点击"登录"按钮
4. 登录成功后跳转到首页

### 退出登录

在首页点击"退出登录"按钮

## 技术架构

```
┌─────────────────────────────────────────────┐
│            Vue 3 Frontend (Port 5173)       │
│  ┌────────────────────────────────────────┐ │
│  │  Views (页面组件)                       │ │
│  │  - Login.vue (登录页)                   │ │
│  │  - Register.vue (注册页)                │ │
│  │  - Home.vue (首页)                      │ │
│  └────────────────────────────────────────┘ │
│  ┌────────────────────────────────────────┐ │
│  │  Router (路由管理)                      │ │
│  │  - 路由守卫                              │ │
│  │  - 权限验证                              │ │
│  └────────────────────────────────────────┘ │
│  ┌────────────────────────────────────────┐ │
│  │  Pinia Store (状态管理)                 │ │
│  │  - auth.js (认证状态)                   │ │
│  │  - Token 管理                           │ │
│  │  - 用户信息                              │ │
│  └────────────────────────────────────────┘ │
│  ┌────────────────────────────────────────┐ │
│  │  API Layer (接口层)                     │ │
│  │  - auth.js (登录/注册)                  │ │
│  │  - code.js (验证码)                     │ │
│  │  - request.js (HTTP封装)                │ │
│  └────────────────────────────────────────┘ │
└─────────────────────────────────────────────┘
                     ↓ HTTP/REST API
┌─────────────────────────────────────────────┐
│       .NET Core Backend (Port 5000)         │
│  ┌────────────────────────────────────────┐ │
│  │  AuthController                         │ │
│  │  - POST /api/Auth (登录)                │ │
│  │  - POST /api/Auth/register (注册)       │ │
│  └────────────────────────────────────────┘ │
│  ┌────────────────────────────────────────┐ │
│  │  CodeController                         │ │
│  │  - GET /api/Code (获取验证码)           │ │
│  └────────────────────────────────────────┘ │
│  ┌────────────────────────────────────────┐ │
│  │  JWT Authentication                     │ │
│  │  - Token 生成                           │ │
│  │  - Token 验证                           │ │
│  └────────────────────────────────────────┘ │
└─────────────────────────────────────────────┘
```

## API 接口对接详情

### 1. 登录接口

**前端调用**:
```javascript
// src/api/auth.js
authApi.login({
  username: 'zhangsan',
  password: '123456'
})
```

**后端接口**:
```
POST /api/Auth
Content-Type: application/json

{
  "username": "zhangsan",
  "password": "123456"
}
```

**响应**: JWT Token 字符串

### 2. 注册接口

**前端调用**:
```javascript
// src/api/auth.js
authApi.register({
  name: '张三',
  username: 'zhangsan',
  password: '123456',
  code: '123456',
  avatar: ''
})
```

**后端接口**:
```
POST /api/Auth/register
Content-Type: application/json

{
  "name": "张三",
  "username": "zhangsan",
  "password": "123456",
  "code": "123456",
  "avatar": ""
}
```

**响应**: JWT Token 字符串

### 3. 获取验证码接口

**前端调用**:
```javascript
// src/api/code.js
codeApi.getCode({
  value: 'zhangsan',
  type: 0  // 0 表示注册验证码
})
```

**后端接口**:
```
GET /api/Code?value=zhangsan&type=0
```

**响应**: 验证码字符串

## JWT Token 处理

### Token 存储
- Token 存储在 `localStorage` 中
- 刷新页面后 Token 自动加载

### Token 使用
- 所有需要认证的 API 请求自动添加 `Authorization: Bearer {token}` 头
- Token 由 Axios 拦截器自动处理

### Token 失效处理
- API 返回 401 状态码时，自动清除 Token
- 自动重定向到登录页

## 路由守卫

### 需要认证的路由
- `/` (首页) - 需要登录

### 访客路由
- `/login` (登录) - 已登录用户访问会重定向到首页
- `/register` (注册) - 已登录用户访问会重定向到首页

## 目录结构详解

```
frontend/
├── public/              # 静态资源目录
├── src/
│   ├── api/            # API 接口定义
│   │   ├── auth.js     # 认证相关接口（登录、注册）
│   │   └── code.js     # 验证码接口
│   ├── components/     # 可复用组件（预留）
│   ├── router/         # 路由配置
│   │   └── index.js    # 路由定义和守卫
│   ├── stores/         # Pinia 状态管理
│   │   └── auth.js     # 认证状态（Token、用户信息）
│   ├── utils/          # 工具函数
│   │   └── request.js  # Axios 封装（拦截器）
│   ├── views/          # 页面组件
│   │   ├── Home.vue    # 首页
│   │   ├── Login.vue   # 登录页
│   │   └── Register.vue # 注册页
│   ├── App.vue         # 根组件
│   ├── main.js         # 应用入口
│   └── style.css       # 全局样式
├── .env.development    # 开发环境配置
├── .env.production     # 生产环境配置
├── .gitignore          # Git 忽略文件
├── index.html          # HTML 模板
├── package.json        # 项目依赖
├── README.md           # 项目说明
└── vite.config.js      # Vite 配置
```

## 常见问题

### 1. CORS 跨域问题

前端配置了开发代理，所有 `/api` 请求会被代理到 `http://localhost:5000`。

如果后端地址不同，修改 `vite.config.js`:

```javascript
server: {
  proxy: {
    '/api': {
      target: 'http://your-backend-url:port',
      changeOrigin: true
    }
  }
}
```

### 2. 验证码显示在界面上

这是正常的开发行为。在实际生产环境中，验证码应该：
- 发送到用户邮箱
- 发送到用户手机
- 使用图形验证码

当前后端返回的验证码直接显示在前端，便于开发测试。

### 3. 如何修改后端地址

**开发环境**：修改 `vite.config.js` 中的 proxy 配置

**生产环境**：修改 `.env.production` 或在打包时配置 Nginx 反向代理

### 4. Token 过期时间

Token 过期时间由后端配置，前端会在 Token 失效时自动跳转到登录页。

## 生产部署

### 构建

```bash
npm run build
```

### 部署

将 `dist` 目录内容部署到 Web 服务器（如 Nginx）。

### Nginx 配置示例

```nginx
server {
    listen 80;
    server_name your-domain.com;
    
    # 前端静态文件
    location / {
        root /path/to/dist;
        try_files $uri $uri/ /index.html;
    }
    
    # API 反向代理
    location /api {
        proxy_pass http://localhost:5000;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
    }
}
```

## 扩展功能

当前实现了基础的登录注册功能，可以继续扩展：

1. **用户信息管理**
   - 修改用户资料
   - 上传头像
   - 修改密码

2. **视频管理**
   - 视频列表
   - 视频上传
   - 视频播放

3. **角色权限**
   - 权限判断
   - 菜单控制
   - 按钮权限

4. **其他功能**
   - 关注/取消关注
   - 评论功能
   - 搜索功能

## 技术支持

如有问题，请查看：
- Vue 3 文档: https://vuejs.org/
- Vue Router 文档: https://router.vuejs.org/
- Pinia 文档: https://pinia.vuejs.org/
- Axios 文档: https://axios-http.com/

## License

MIT
