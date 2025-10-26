# 快速开始 - Vue 前端

## 🚀 一分钟启动

```bash
# 1. 安装依赖（首次运行）
npm install

# 2. 启动开发服务器
npm run dev

# 3. 浏览器访问
# http://localhost:5173
```

## 📝 快速测试

### 测试注册流程

1. 访问: http://localhost:5173/register
2. 填写信息:
   - 昵称: `测试用户`
   - 用户名: `testuser`
3. 点击"获取验证码"
4. 复制显示的验证码
5. 设置密码: `123456`
6. 点击注册

### 测试登录流程

1. 访问: http://localhost:5173/login
2. 输入:
   - 用户名: `testuser`
   - 密码: `123456`
3. 点击登录

## 🔧 常用命令

```bash
# 开发
npm run dev

# 构建
npm run build

# 预览构建
npm run preview

# 安装新包
npm install 包名

# 清除缓存
rm -rf node_modules package-lock.json
npm install
```

## 📂 重要文件

| 文件 | 说明 |
|------|------|
| `src/main.js` | 应用入口 |
| `src/App.vue` | 根组件 |
| `src/router/index.js` | 路由配置 |
| `src/stores/auth.js` | 认证状态 |
| `src/views/Login.vue` | 登录页面 |
| `src/views/Register.vue` | 注册页面 |
| `vite.config.js` | 构建配置 |

## 🌐 API 地址

默认代理到: `http://localhost:5000`

修改地址: 编辑 `vite.config.js`

```javascript
proxy: {
  '/api': {
    target: 'http://your-backend-url',
    changeOrigin: true
  }
}
```

## 🎯 路由列表

| 路径 | 页面 | 说明 |
|------|------|------|
| `/` | 首页 | 需要登录 |
| `/login` | 登录 | 访客页面 |
| `/register` | 注册 | 访客页面 |

## 📦 项目结构

```
src/
├── api/          # API 接口
├── components/   # 公共组件
├── router/       # 路由配置
├── stores/       # 状态管理
├── utils/        # 工具函数
├── views/        # 页面组件
├── App.vue       # 根组件
├── main.js       # 入口文件
└── style.css     # 全局样式
```

## 💡 快速提示

### Token 存储
- 自动存储在 `localStorage`
- 刷新页面不丢失
- 退出登录自动清除

### 错误处理
- API 错误自动提示
- 401 自动跳转登录
- 网络错误友好提示

### 开发技巧
1. 安装 Vue DevTools 扩展
2. 使用浏览器开发者工具
3. 查看 Network 标签调试 API

## 📚 更多文档

- [完整使用说明](./使用说明.md)
- [项目 README](./README.md)
- [前端指南](../FRONTEND_GUIDE.md)

## ⚡ 常见问题

**Q: 端口被占用?**
```bash
# 修改 vite.config.js 中的 port
server: { port: 5174 }
```

**Q: CORS 错误?**
```bash
# 检查后端是否启动
# 检查代理配置是否正确
```

**Q: 依赖安装失败?**
```bash
# 清除缓存重试
npm cache clean --force
rm -rf node_modules
npm install
```

---

**开始使用吧！** 🎉
