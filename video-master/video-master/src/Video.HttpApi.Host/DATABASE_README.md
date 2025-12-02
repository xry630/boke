# Database Setup Guide

## Overview
This project uses SQLite as the database. The database file `video.db` should be located in the `src/Video.HttpApi.Host` directory.

## Initial Setup

### Option 1: Using the Setup Script (Recommended)
1. Navigate to the HttpApi.Host directory:
   ```bash
   cd src/Video.HttpApi.Host
   ```

2. Run the database initialization script:
   ```bash
   ./init_database.sh
   ```

### Option 2: Manual Setup
1. Navigate to the HttpApi.Host directory:
   ```bash
   cd src/Video.HttpApi.Host
   ```

2. Run the SQL script manually:
   ```bash
   sqlite3 video.db < init_database.sql
   ```

## Verification
After setup, you can verify the database was created correctly:

```bash
# List all tables
sqlite3 video.db ".tables"

# Check admin user
sqlite3 video.db "SELECT * FROM UserInfos;"

# Check roles
sqlite3 video.db "SELECT * FROM Roles;"
```

## Default Admin Account
- **Username:** admin
- **Password:** admin
- **Role:** admin

## Database Schema
The database includes the following tables:
- `UserInfos` - User accounts
- `Roles` - User roles (admin, user)
- `UserRoles` - User-role relationships
- `Videos` - Video information
- `Comments` - Video comments
- `Likes` - Video likes
- `Classifys` - Video categories
- `BeanVermicellis` - User follow relationships

## Connection String
The database connection string is configured in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "Default": "Data Source=video.db"
  }
}
```

## Troubleshooting

### Issue: "no such table" errors
**Solution:** The database hasn't been initialized. Run the setup script as described above.

### Issue: Database file not found
**Solution:** Ensure the `video.db` file exists in the `src/Video.HttpApi.Host` directory after running the setup.

### Issue: Permission denied
**Solution:** Make sure the setup script is executable:
```bash
chmod +x init_database.sh
```

## Using .NET EF Core Migrations (if .NET CLI is available)
If you have the .NET CLI installed, you can also use EF Core migrations:

```bash
# Add a new migration
dotnet ef migrations add [MigrationName] --project src/Video.EntityFrameworkCore --startup-project src/Video.HttpApi.Host

# Apply migrations
dotnet ef database update --project src/Video.EntityFrameworkCore --startup-project src/Video.HttpApi.Host
```

## Notes
- The database file (`video.db`) should be committed to version control if it contains initial seed data
- SQLite doesn't support concurrent writes, but is perfect for development and small-scale deployments
- All timestamps are stored as TEXT in ISO 8601 format