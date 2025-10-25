#!/bin/bash

echo "=================================="
echo "  视频管理系统前端 - 启动脚本"
echo "=================================="
echo ""

# 检查 Node.js 是否安装
if ! command -v node &> /dev/null; then
    echo "❌ 错误: 未检测到 Node.js，请先安装 Node.js"
    exit 1
fi

echo "✅ Node.js 版本: $(node -v)"
echo "✅ npm 版本: $(npm -v)"
echo ""

# 检查是否已安装依赖
if [ ! -d "node_modules" ]; then
    echo "📦 未检测到 node_modules，开始安装依赖..."
    npm install
    echo ""
fi

echo "🚀 启动开发服务器..."
echo "📝 访问地址: http://localhost:5173"
echo "⏹️  按 Ctrl+C 停止服务器"
echo ""

npm run dev
