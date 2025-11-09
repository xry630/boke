# Git 上传到GitHub 操作指南

本文档详细说明如何将重构后的 boke 仓库项目结构上传到 GitHub。

## 🎯 操作目标

将本地重构后的项目结构提交并推送到 GitHub 远程仓库。

## 📋 前置条件

1. 本地已安装 Git
2. 已有 GitHub 账号
3. 已在 GitHub 创建好仓库（或使用现有仓库）
4. 本地仓库已关联远程仓库

## 🔧 操作流程

### 1. 检查当前 Git 状态

```bash
# 查看当前状态
git status

# 查看分支
git branch

# 查看远程仓库
git remote -v
```

### 2. 添加所有更改到暂存区

```bash
# 添加所有新文件和更改
git add .

# 或者分步添加
git add backend/
git add frontend/
git add docs/
git add configs/
git add deployment/
git add *.md
git add video-master  # 符号链接
```

### 3. 提交更改

```bash
# 提交重构更改
git commit -m "feat: 重构项目结构，按功能分类管理

- 创建功能分类目录：backend/, frontend/, docs/, configs/, deployment/
- 移动后端项目到 backend/video-master/
- 整理文档到 docs/ 目录
- 集中部署文件到 deployment/ 目录
- 添加项目配置到 configs/ 目录
- 创建符号链接保持向后兼容
- 更新所有路径引用和配置文件
- 新增详细的项目管理文档

Breaking Change: 项目目录结构已重新组织，请查看 README.md 了解新结构"
```

### 4. 推送到远程仓库

#### 方案A：推送到现有分支（推荐）

```bash
# 推送到当前分支
git push origin chore-boke-categorize-projects-by-feature

# 如果需要强制推送（谨慎使用）
git push origin chore-boke-categorize-projects-by-feature --force-with-lease
```

#### 方案B：合并到主分支

```bash
# 切换到主分支
git checkout main
# 或者 git checkout master

# 合并功能分支
git merge chore-boke-categorize-projects-by-feature

# 推送到远程主分支
git push origin main
```

#### 方案C：创建新的主分支

```bash
# 创建新分支作为主分支
git checkout -b restructured-main

# 推送新分支到远程
git push origin restructured-main

# 在 GitHub 上设置新分支为默认分支
```

### 5. 验证上传结果

```bash
# 查看远程状态
git status

# 查看提交历史
git log --oneline -10

# 查看分支状态
git branch -a
```

## 🚨 重要注意事项

### 1. 符号链接处理

Git 默认会跟踪符号链接，但某些 Git 配置可能忽略符号链接：

```bash
# 检查 Git 是否跟踪符号链接
git config core.symlinks

# 如果需要启用符号链接跟踪
git config core.symlinks true
```

### 2. 大文件处理

如果项目中有大文件（如视频文件、大型数据库文件等）：

```bash
# 检查是否有大文件
find . -type f -size +50M -ls

# 考虑使用 Git LFS
git lfs track "*.mp4"
git lfs track "*.db"
git add .gitattributes
```

### 3. 敏感信息检查

确保没有提交敏感信息：

```bash
# 检查可能的敏感文件
find . -name "*.key" -o -name "*.pem" -o -name "appsettings.Production.json"

# 检查配置文件中的敏感信息
grep -r "password\|secret\|token" --include="*.json" --include="*.config" .
```

## 🔍 上传后验证清单

### 在 GitHub 上检查：

- [ ] 所有文件都已上传
- [ ] 目录结构正确显示
- [ ] 符号链接正确显示（如果支持）
- [ ] README.md 文件内容正确
- [ ] 没有敏感信息泄露

### 本地验证：

```bash
# 克隆远程仓库到新目录测试
git clone https://github.com/用户名/仓库名.git test-repo
cd test-repo

# 验证目录结构
ls -la

# 验证符号链接
ls -la video-master

# 验证构建
cd video-master/video-master/src/Video.HttpApi.Host
dotnet build
```

## 🛠️ 常见问题解决

### 问题1：推送被拒绝

```bash
# 拉取最新更改
git pull origin chore-boke-categorize-projects-by-feature --rebase

# 解决冲突后重新推送
git push origin chore-boke-categorize-projects-by-feature
```

### 问题2：符号链接在 GitHub 上显示为文件

这是正常现象，GitHub 会在界面上显示符号链接的目标内容。

### 问题3：文件大小限制

```bash
# 使用 Git LFS 处理大文件
git lfs install
git lfs track "大型文件扩展名"
git add .gitattributes
git commit -m "chore: 添加 Git LFS 配置"
```

## 📝 后续操作建议

### 1. 更新 CI/CD 配置

如果项目有 CI/CD 流水线，需要更新路径配置：

```yaml
# GitHub Actions 示例
- name: Build Backend
  run: |
    cd backend/video-master/video-master/src/Video.HttpApi.Host
    dotnet build

- name: Build Frontend  
  run: |
    cd frontend
    npm install
    npm run build
```

### 2. 通知团队成员

- 发送通知邮件说明新的项目结构
- 更新开发文档和 Wiki
- 提供迁移指南

### 3. 清理旧分支（可选）

```bash
# 删除已合并的分支
git branch -d chore-boke-categorize-projects-by-feature
git push origin --delete chore-boke-categorize-projects-by-feature
```

## 🎉 完成确认

当看到以下内容时，说明上传成功：

```bash
# Git 状态显示
On branch chore-boke-categorize-projects-by-feature
nothing to commit, working tree clean

# 远程状态显示
* [new branch]      chore-boke-categorize-projects-by-feature -> chore-boke-categorize-projects-by-feature
```

在 GitHub 网页端能看到完整的项目结构和所有文件。

---

**🎊 恭喜！项目结构重构已成功上传到 GitHub！**
