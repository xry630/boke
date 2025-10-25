# 视频管理系统 - 前端

这是一个使用 Vue 3 开发的视频管理系统前端界面，用于与 .NET Core JWT 认证后台对接。

## 技术栈

- Vue 3 - 渐进式 JavaScript 框架
- Vue Router - 官方路由管理器
- Pinia - 状态管理库
- Axios - HTTP 客户端
- Vite - 前端构建工具

## 功能特性

- ✅ 用户登录
- ✅ 用户注册（含验证码）
- ✅ JWT Token 管理
- ✅ 路由守卫
- ✅ 响应式设计
- ✅ 优雅的UI界面

## 项目结构

```
frontend/
├── src/
│   ├── api/           # API 接口定义
│   │   ├── auth.js    # 认证相关接口
│   │   └── code.js    # 验证码接口
│   ├── router/        # 路由配置
│   ├── stores/        # Pinia 状态管理
│   │   └── auth.js    # 认证状态
│   ├── utils/         # 工具函数
│   │   └── request.js # Axios 封装
│   ├── views/         # 页面组件
│   │   ├── Login.vue  # 登录页
│   │   ├── Register.vue # 注册页
│   │   └── Home.vue   # 首页
│   ├── App.vue        # 根组件
│   ├── main.js        # 入口文件
│   └── style.css      # 全局样式
├── index.html         # HTML 模板
├── vite.config.js     # Vite 配置
└── package.json       # 项目依赖

```

## 安装依赖

```bash
cd frontend
npm install
```

## 开发模式

```bash
npm run dev
```

访问 http://localhost:5173

## 生产构建

```bash
npm run build
```

构建产物将输出到 `dist` 目录。

## API 配置

默认配置代理到 `http://localhost:5000`，可在 `vite.config.js` 中修改：

```javascript
server: {
  port: 5173,
  proxy: {
    '/api': {
      target: 'http://localhost:5000',  // 修改为你的后端地址
      changeOrigin: true
    }
  }
}
```

## API 接口说明

### 登录接口
- **接口**: `POST /api/Auth`
- **参数**: 
  ```json
  {
    "username": "string",
    "password": "string"
  }
  ```
- **返回**: JWT Token 字符串

### 注册接口
- **接口**: `POST /api/Auth/register`
- **参数**: 
  ```json
  {
    "name": "string",
    "username": "string",
    "password": "string",
    "code": "string",
    "avatar": "string"
  }
  ```
- **返回**: JWT Token 字符串

### 获取验证码接口
- **接口**: `GET /api/Code`
- **参数**: 
  ```
  value: string (用户名)
  type: number (0=注册)
  ```
- **返回**: 验证码字符串

## 使用说明

### 1. 注册账号
1. 访问注册页面
2. 填写昵称和用户名
3. 点击"获取验证码"按钮
4. 输入收到的验证码
5. 设置密码并确认
6. 点击注册按钮

### 2. 登录
1. 访问登录页面
2. 输入用户名和密码
3. 点击登录按钮
4. 成功后将自动跳转到首页

### 3. Token 管理
- Token 自动存储在 localStorage
- 所有 API 请求自动携带 Token
- Token 失效时自动跳转登录页

## 注意事项

1. 请确保后端服务已启动并运行在配置的地址
2. 首次使用需要先注册账号
3. 验证码在开发环境下会直接显示在界面上
4. 生产环境建议配置环境变量管理 API 地址

## 浏览器支持

- Chrome (推荐)
- Firefox
- Safari
- Edge

## License

MIT
