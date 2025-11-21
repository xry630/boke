# 多阶段构建 Dockerfile

# 阶段1: 构建阶段
FROM node:18-alpine AS builder

WORKDIR /app

# 复制前端项目的 package.json 和 package-lock.json
COPY ../frontend/package*.json ./

# 安装依赖
RUN npm install

# 复制前端源代码
COPY ../frontend/ .

# 构建生产版本
RUN npm run build

# 阶段2: 运行阶段
FROM nginx:alpine

# 复制构建产物到 Nginx
COPY --from=builder /app/dist /usr/share/nginx/html

# 复制 Nginx 配置（可选）
# COPY nginx.conf /etc/nginx/conf.d/default.conf

# 暴露端口
EXPOSE 80

# 启动 Nginx
CMD ["nginx", "-g", "daemon off;"]
