# Git 快速上传速查表

## 🚀 一键上传到 GitHub

### 前置条件
- ✅ 已关联远程仓库
- ✅ 在正确的分支上 (`chore-boke-categorize-projects-by-feature`)

### 快速操作

```bash
# 1. 查看状态
git status

# 2. 添加所有文件
git add .

# 3. 提交更改
git commit -m "feat: 重构项目结构，按功能分类管理

- 创建功能分类目录：backend/, frontend/, docs/, configs/, deployment/
- 移动并整理项目文件
- 更新所有配置和路径引用
- 添加项目管理文档
- 创建向后兼容符号链接"

# 4. 推送到远程
git push origin chore-boke-categorize-projects-by-feature
```

## 🔧 如果遇到问题

### 推送被拒绝
```bash
git pull origin chore-boke-categorize-projects-by-feature --rebase
git push origin chore-boke-categorize-projects-by-feature
```

### 需要强制推送
```bash
git push origin chore-boke-categorize-projects-by-feature --force-with-lease
```

### 切换分支
```bash
git checkout main  # 或 master
git merge chore-boke-categorize-projects-by-feature
git push origin main
```

## ✅ 验证上传成功

```bash
# 检查远程状态
git status
git log --oneline -3

# 在 GitHub 上查看仓库
# 确认文件结构和符号链接正确显示
```

---

**📝 详细操作请参考 [Git上传指南](GIT_UPLOAD_GUIDE.md)**
