# 部署指南

## 📦 部署前准备

### 1. 检查项目
```bash
# 进入前端目录
cd frontend

# 确保项目可以正常构建
npm run build

# 检查构建产物
ls -la dist/
```

### 2. 环境变量
确保配置了正确的环境变量：
- `.env.production` - 生产环境配置

## 🚀 部署方案

### 方案 1: Nginx 部署（推荐）

#### 步骤 1: 构建项目
```bash
cd frontend
npm run build
```

#### 步骤 2: 上传到服务器
```bash
# 使用 scp
scp -r dist/* user@server:/var/www/video-frontend/

# 或使用 rsync
rsync -avz dist/ user@server:/var/www/video-frontend/
```

#### 步骤 3: 配置 Nginx
创建配置文件 `/etc/nginx/sites-available/video-frontend`:

```nginx
server {
    listen 80;
    server_name your-domain.com;
    
    root /var/www/video-frontend;
    index index.html;
    
    location / {
        try_files $uri $uri/ /index.html;
    }
    
    location /api {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
    }
    
    # 静态资源缓存
    location ~* \.(jpg|jpeg|png|gif|ico|css|js)$ {
        expires 30d;
        add_header Cache-Control "public, immutable";
    }
    
    # Gzip 压缩
    gzip on;
    gzip_vary on;
    gzip_min_length 1024;
    gzip_types text/plain text/css text/javascript application/javascript application/json;
}
```

#### 步骤 4: 启用站点
```bash
# 创建符号链接
sudo ln -s /etc/nginx/sites-available/video-frontend /etc/nginx/sites-enabled/

# 测试配置
sudo nginx -t

# 重载 Nginx
sudo systemctl reload nginx
```

### 方案 2: Docker 部署

#### 步骤 1: 构建镜像
```bash
cd frontend
docker build -t video-frontend:latest .
```

#### 步骤 2: 运行容器
```bash
docker run -d \
  --name video-frontend \
  -p 80:80 \
  --restart unless-stopped \
  video-frontend:latest
```

#### 步骤 3: 查看日志
```bash
docker logs -f video-frontend
```

### 方案 3: Docker Compose 部署

#### 步骤 1: 启动服务
```bash
cd deployment
docker-compose up -d
```

#### 步骤 2: 查看状态
```bash
docker-compose ps
```

#### 步骤 3: 查看日志
```bash
docker-compose logs -f
```

### 方案 4: Apache 部署

#### 步骤 1: 构建项目
```bash
cd frontend
npm run build
```

#### 步骤 2: 配置 Apache
创建配置文件：

```apache
<VirtualHost *:80>
    ServerName your-domain.com
    DocumentRoot /var/www/video-frontend
    
    <Directory /var/www/video-frontend>
        Options -Indexes +FollowSymLinks
        AllowOverride All
        Require all granted
        
        # SPA 路由支持
        RewriteEngine On
        RewriteBase /
        RewriteRule ^index\.html$ - [L]
        RewriteCond %{REQUEST_FILENAME} !-f
        RewriteCond %{REQUEST_FILENAME} !-d
        RewriteRule . /index.html [L]
    </Directory>
    
    # API 代理
    ProxyPass /api http://localhost:5000/api
    ProxyPassReverse /api http://localhost:5000/api
</VirtualHost>
```

#### 步骤 3: 启用模块和站点
```bash
sudo a2enmod rewrite proxy proxy_http
sudo a2ensite video-frontend
sudo systemctl reload apache2
```

## 🔒 HTTPS 配置（推荐）

### 使用 Let's Encrypt (Certbot)

#### 步骤 1: 安装 Certbot
```bash
# Ubuntu/Debian
sudo apt install certbot python3-certbot-nginx

# CentOS/RHEL
sudo yum install certbot python3-certbot-nginx
```

#### 步骤 2: 获取证书
```bash
sudo certbot --nginx -d your-domain.com
```

#### 步骤 3: 自动续期
```bash
# 测试自动续期
sudo certbot renew --dry-run
```

### 手动配置 HTTPS

```nginx
server {
    listen 443 ssl http2;
    server_name your-domain.com;
    
    ssl_certificate /path/to/certificate.crt;
    ssl_certificate_key /path/to/private.key;
    
    ssl_protocols TLSv1.2 TLSv1.3;
    ssl_ciphers HIGH:!aNULL:!MD5;
    ssl_prefer_server_ciphers on;
    
    # 其他配置同 HTTP...
}

# HTTP 重定向到 HTTPS
server {
    listen 80;
    server_name your-domain.com;
    return 301 https://$server_name$request_uri;
}
```

## 🌐 CDN 配置

### 使用阿里云 OSS + CDN

#### 步骤 1: 上传到 OSS
```bash
# 使用 ossutil
ossutil cp -r dist/ oss://your-bucket/video-frontend/
```

#### 步骤 2: 配置 CDN
- 绑定域名
- 配置回源规则
- 设置缓存策略

#### 步骤 3: 配置 CORS
```json
{
  "AllowOrigin": ["*"],
  "AllowMethod": ["GET", "POST", "PUT", "DELETE"],
  "AllowHeader": ["*"]
}
```

## 📊 性能优化

### 1. Gzip 压缩
已在 Vite 构建时自动启用

### 2. 资源缓存
```nginx
location ~* \.(jpg|jpeg|png|gif|ico|css|js|svg|woff|woff2)$ {
    expires 1y;
    add_header Cache-Control "public, immutable";
}
```

### 3. HTTP/2
```nginx
listen 443 ssl http2;
```

### 4. 预压缩
```bash
# 进入前端目录
cd frontend

# 安装 gzip 插件
npm install -D vite-plugin-compression

# 配置 vite.config.js
import viteCompression from 'vite-plugin-compression'

export default defineConfig({
  plugins: [
    vue(),
    viteCompression()
  ]
})
```

## 🔍 监控和日志

### Nginx 日志
```nginx
access_log /var/log/nginx/video-frontend-access.log;
error_log /var/log/nginx/video-frontend-error.log;
```

### Docker 日志
```bash
docker logs -f video-frontend
```

### 性能监控
考虑使用：
- Google Analytics
- Sentry (错误监控)
- New Relic
- Prometheus + Grafana

## 🐛 问题排查

### 问题 1: 404 错误
**原因**: Nginx 没有配置 SPA 路由支持

**解决方案**:
```nginx
location / {
    try_files $uri $uri/ /index.html;
}
```

### 问题 2: API 请求失败
**原因**: 后端地址配置错误

**解决方案**:
1. 检查 Nginx 代理配置
2. 确保后端服务正常运行
3. 检查防火墙规则

### 问题 3: CORS 错误
**原因**: 后端没有配置 CORS

**解决方案**:
使用 Nginx 反向代理，不要直接跨域访问

### 问题 4: 白屏
**原因**: JavaScript 加载失败或路径错误

**解决方案**:
1. 检查浏览器控制台
2. 查看 Network 标签
3. 确认资源路径正确

## 📋 部署检查清单

- [ ] 代码已构建成功
- [ ] 环境变量已配置
- [ ] 静态文件已上传
- [ ] Web 服务器已配置
- [ ] 域名已解析
- [ ] HTTPS 已配置
- [ ] 防火墙规则已设置
- [ ] 后端 API 可访问
- [ ] 浏览器测试通过
- [ ] 移动端测试通过
- [ ] 性能测试通过
- [ ] 监控已部署
- [ ] 备份已配置

## 🔄 CI/CD 配置

### GitHub Actions 示例

创建 `.github/workflows/deploy.yml`:

```yaml
name: Deploy

on:
  push:
    branches: [ main ]

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v2
    
    - name: Setup Node.js
      uses: actions/setup-node@v2
      with:
        node-version: '18'
    
    - name: Install dependencies
      run: npm install
      working-directory: ./frontend
    
    - name: Build
      run: npm run build
      working-directory: ./frontend
    
    - name: Deploy to server
      uses: appleboy/scp-action@master
      with:
        host: ${{ secrets.HOST }}
        username: ${{ secrets.USERNAME }}
        key: ${{ secrets.SSH_KEY }}
        source: "frontend/dist/*"
        target: "/var/www/video-frontend"
```

## 🔐 安全建议

1. **使用 HTTPS**
   - 保护数据传输
   - 提升 SEO 排名

2. **设置安全头**
```nginx
add_header X-Frame-Options "SAMEORIGIN";
add_header X-Content-Type-Options "nosniff";
add_header X-XSS-Protection "1; mode=block";
add_header Content-Security-Policy "default-src 'self'";
```

3. **限制请求频率**
```nginx
limit_req_zone $binary_remote_addr zone=api:10m rate=10r/s;

location /api {
    limit_req zone=api burst=20;
    proxy_pass http://localhost:5000;
}
```

4. **隐藏服务器信息**
```nginx
server_tokens off;
```

## 📞 获取帮助

如果遇到部署问题，请：
1. 查看服务器日志
2. 检查浏览器控制台
3. 参考官方文档
4. 提交 Issue

---

**祝部署顺利！** 🎉
