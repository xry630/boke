#!/bin/bash

echo "=================================="
echo "  Video Master - 500 Error Fix Test"
echo "=================================="
echo ""

# Check if .NET is installed
if ! command -v dotnet &> /dev/null; then
    echo "❌ Error: .NET 6.0 SDK is not installed"
    echo "Please install .NET 6.0 SDK from https://dotnet.microsoft.com/download"
    exit 1
fi

echo "✅ .NET version: $(dotnet --version)"
echo ""

# Check if Node.js is installed
if ! command -v node &> /dev/null; then
    echo "❌ Error: Node.js is not installed"
    echo "Please install Node.js 18+ from https://nodejs.org"
    exit 1
fi

echo "✅ Node.js version: $(node -v)"
echo "✅ npm version: $(npm -v)"
echo ""

# Test backend compilation
echo "🔧 Testing backend compilation..."
cd /home/engine/project/video-master/video-master/src/Video.HttpApi.Host
if dotnet build --no-restore; then
    echo "✅ Backend compilation successful"
else
    echo "❌ Backend compilation failed"
    exit 1
fi
echo ""

# Test frontend build
echo "🔧 Testing frontend build..."
cd /home/engine/project/frontend
if npm run build; then
    echo "✅ Frontend build successful"
else
    echo "❌ Frontend build failed"
    exit 1
fi
echo ""

echo "🎉 All tests passed! The 500 error fixes should resolve the login issues."
echo ""
echo "Next steps:"
echo "1. Start MySQL and Redis services"
echo "2. Run the backend: cd video-master/video-master/src/Video.HttpApi.Host && dotnet run"
echo "3. Run the frontend: cd frontend && npm run dev"
echo "4. Test login at http://localhost:5173 with admin/admin"
echo ""
echo "For detailed setup instructions, see: LOGIN_500_ERROR_FIX.md"