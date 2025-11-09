# 项目访问指南

## 🚨 重要提示

项目结构已按功能重新组织，原有路径已变更。请使用以下新的路径访问项目：

## 📁 新的项目结构

### 后端项目
```bash
# 原路径: video-master/video-master/
# 新路径: backend/video-master/video-master/

cd backend/video-master/video-master/
```

### 前端项目
```bash
# 路径保持不变
cd frontend/
```

## 🛠️ 开发命令

### 启动后端
```bash
cd backend/video-master/video-master/src/Video.HttpApi.Host
dotnet restore
dotnet run
```

### 启动前端
```bash
cd frontend
npm install
npm run dev
```

### 部署
```bash
cd deployment
docker-compose up -d
```

## 🔧 如果需要兼容原有路径

如果某些工具或脚本仍需要访问 `video-master/video-master/` 路径，可以创建符号链接：

```bash
# 创建符号链接（可选）
ln -s backend/video-master video-master
```

## 📋 项目结构总览

```
.
├── backend/video-master/          # 后端项目
├── frontend/                      # 前端项目
├── docs/                         # 文档
├── configs/                      # 配置
├── deployment/                   # 部署文件
├── README.md                     # 项目说明
└── ACCESS_GUIDE.md              # 本访问指南
```

---

**注意**: 所有开发、构建、部署操作都应使用新的目录结构。
