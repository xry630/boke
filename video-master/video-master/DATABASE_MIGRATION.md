# 数据库迁移说明：从 MySQL 到 SQLite

## 变更概述

本项目已将数据库后端从 MySQL 迁移到 SQLite，以简化部署和配置。

## 主要变更

### 1. 新增 SQLite EntityFrameworkCore 项目
- 创建了 `Simple.EntityFrameworkCore.Sqlite` 项目
- 添加了 `Microsoft.EntityFrameworkCore.Sqlite` NuGet 包
- 实现了 `SimpleEntityFrameworkCoreSqliteExtension` 扩展方法

### 2. 更新项目引用
- `Video.EntityFrameworkCore` 项目现在引用 `Simple.EntityFrameworkCore.Sqlite` 而不是 `Simple.EntityFrameworkCore.MySql`
- 更新了 `VideoEntityFrameworkCoreExtension.cs` 中的扩展方法调用

### 3. 配置文件更新
- `appsettings.json` 中的连接字符串从 MySQL 格式更改为 SQLite 格式：
  ```json
  "ConnectionStrings": {
    "Default": "Data Source=video.db"
  }
  ```

### 4. 数据库模型调整
- 移除了 SQLite 不支持的 `HasComment()` 方法调用
- 保持了所有表结构和索引配置

### 5. 迁移文件
- 删除了原有的 MySQL 迁移文件
- 创建了新的 SQLite 迁移文件：
  - `20240101000000_Init.cs` - 创建表结构
  - `20240101000001_hasData.cs` - 插入初始数据
- 更新了 `VideoDbContextModelSnapshot.cs`

### 6. 解决方案文件
- 在 `Video.sln` 中添加了新的 SQLite 项目引用

### 7. Git 忽略规则
- 在 `.gitignore` 中添加了 SQLite 数据库文件忽略规则：
  ```
  *.db
  *.sqlite
  *.sqlite3
  ```

## 部署说明

1. **数据库文件位置**：SQLite 数据库文件 `video.db` 将在应用程序运行目录中自动创建
2. **无需数据库服务器**：SQLite 是文件数据库，不需要单独的数据库服务器
3. **备份**：只需备份 `video.db` 文件即可
4. **迁移数据**：如需从现有 MySQL 迁移数据，需要编写数据迁移脚本

## 优势

- **简化部署**：无需安装和配置 MySQL 服务器
- **减少依赖**：减少了外部依赖和服务
- **便携性**：数据库文件可以直接复制和移动
- **适合中小型应用**：对于中小型应用，SQLite 性能完全满足需求

## 注意事项

- SQLite 在高并发写入场景下性能有限
- 不支持 MySQL 特有的一些高级功能
- 如需扩展到大规模应用，可考虑切换到 PostgreSQL 或其他数据库