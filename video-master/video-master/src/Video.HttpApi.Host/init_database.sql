-- Create tables based on EF Core migrations
-- Roles table
CREATE TABLE IF NOT EXISTS "Roles" (
    "Id" TEXT NOT NULL PRIMARY KEY,
    "Code" TEXT,
    "CreatedTime" TEXT NOT NULL,
    "Name" TEXT
);

-- UserInfos table
CREATE TABLE IF NOT EXISTS "UserInfos" (
    "Id" TEXT NOT NULL PRIMARY KEY,
    "Avatar" TEXT,
    "CreatedTime" TEXT NOT NULL,
    "Enable" INTEGER NOT NULL,
    "Name" TEXT,
    "Password" TEXT,
    "Username" TEXT
);

-- Classifys table
CREATE TABLE IF NOT EXISTS "Classifys" (
    "Id" TEXT NOT NULL PRIMARY KEY,
    "CreatedTime" TEXT NOT NULL,
    "Name" TEXT
);

-- UserRoles table
CREATE TABLE IF NOT EXISTS "UserRoles" (
    "Id" TEXT NOT NULL PRIMARY KEY,
    "RoleId" TEXT,
    "UserId" TEXT
);

-- Videos table
CREATE TABLE IF NOT EXISTS "Videos" (
    "Id" TEXT NOT NULL PRIMARY KEY,
    "ClassifyId" TEXT,
    "Concern" INTEGER NOT NULL,
    "CreatedTime" TEXT NOT NULL,
    "Description" TEXT,
    "Enable" INTEGER NOT NULL,
    "Name" TEXT,
    "Path" TEXT,
    "Release" INTEGER NOT NULL,
    "UserId" TEXT
);

-- BeanVermicellis table
CREATE TABLE IF NOT EXISTS "BeanVermicellis" (
    "Id" TEXT NOT NULL PRIMARY KEY,
    "BeFocusedId" TEXT,
    "CreatedTime" TEXT NOT NULL,
    "UserId" TEXT
);

-- Comments table
CREATE TABLE IF NOT EXISTS "Comments" (
    "Id" TEXT NOT NULL PRIMARY KEY,
    "Content" TEXT,
    "CreatedTime" TEXT NOT NULL,
    "ParentId" TEXT,
    "UserId" TEXT,
    "VideoId" TEXT
);

-- Likes table
CREATE TABLE IF NOT EXISTS "Likes" (
    "Id" TEXT NOT NULL PRIMARY KEY,
    "CreatedTime" TEXT NOT NULL,
    "UserId" TEXT,
    "VideoId" TEXT
);

-- Create indexes
CREATE INDEX IF NOT EXISTS "IX_BeanVermicellis_BeFocusedId" ON "BeanVermicellis" ("BeFocusedId");
CREATE INDEX IF NOT EXISTS "IX_BeanVermicellis_UserId" ON "BeanVermicellis" ("UserId");
CREATE INDEX IF NOT EXISTS "IX_Classifys_Id" ON "Classifys" ("Id");
CREATE INDEX IF NOT EXISTS "IX_Comments_Id" ON "Comments" ("Id");
CREATE INDEX IF NOT EXISTS "IX_Comments_ParentId" ON "Comments" ("ParentId");
CREATE INDEX IF NOT EXISTS "IX_Comments_UserId" ON "Comments" ("UserId");
CREATE INDEX IF NOT EXISTS "IX_Likes_Id" ON "Likes" ("Id");
CREATE INDEX IF NOT EXISTS "IX_Likes_UserId" ON "Likes" ("UserId");
CREATE INDEX IF NOT EXISTS "IX_Likes_VideoId" ON "Likes" ("VideoId");
CREATE INDEX IF NOT EXISTS "IX_Roles_Id" ON "Roles" ("Id");
CREATE INDEX IF NOT EXISTS "IX_UserInfos_Id" ON "UserInfos" ("Id");
CREATE UNIQUE INDEX IF NOT EXISTS "IX_UserInfos_Username" ON "UserInfos" ("Username");
CREATE INDEX IF NOT EXISTS "IX_UserRoles_Id" ON "UserRoles" ("Id");
CREATE INDEX IF NOT EXISTS "IX_UserRoles_RoleId" ON "UserRoles" ("RoleId");
CREATE INDEX IF NOT EXISTS "IX_UserRoles_UserId" ON "UserRoles" ("UserId");
CREATE INDEX IF NOT EXISTS "IX_Videos_ClassifyId" ON "Videos" ("ClassifyId");
CREATE INDEX IF NOT EXISTS "IX_Videos_Id" ON "Videos" ("Id");
CREATE INDEX IF NOT EXISTS "IX_Videos_UserId" ON "Videos" ("UserId");

-- Insert initial data
INSERT OR IGNORE INTO "Roles" ("Id", "Code", "CreatedTime", "Name") VALUES 
('8a310af8-81a4-4b50-ad2d-8780cd1b965b', 'admin', '0001-01-01 00:00:00', 'admin'),
('b23b5f8c-9c7e-4f1a-8b9d-3e4f2a1c5d6e', 'user', '0001-01-01 00:00:00', 'user');

INSERT OR IGNORE INTO "UserInfos" ("Id", "Avatar", "CreatedTime", "Enable", "Name", "Password", "Username") VALUES 
('admin-user-id', '', '2022-10-16 11:33:54', 1, 'admin', 'admin', 'admin');

INSERT OR IGNORE INTO "UserRoles" ("Id", "RoleId", "UserId") VALUES 
('admin-role-id', '8a310af8-81a4-4b50-ad2d-8780cd1b965b', 'admin-user-id');