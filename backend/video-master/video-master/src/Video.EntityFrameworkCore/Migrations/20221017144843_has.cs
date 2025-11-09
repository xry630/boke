using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Video.Domain.Migrations
{
    public partial class has : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Code", "CreatedTime", "Name" },
                values: new object[] { new Guid("8a310af8-81a4-4b50-ad2d-8780cd1b965b"), "admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin" });

            migrationBuilder.InsertData(
                table: "UserInfos",
                columns: new[] { "Id", "Avatar", "CreatedTime", "Enable", "Name", "Password", "Username" },
                values: new object[] { new Guid("c923adff-cdf2-45fd-b8a5-e4bb57382288"), "", new DateTime(2022, 10, 17, 22, 48, 43, 364, DateTimeKind.Local).AddTicks(7927), true, "admin", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedTime", "RoleId", "UserId" },
                values: new object[] { new Guid("fa995300-4d41-4105-a3a0-6b78293ba5cb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a310af8-81a4-4b50-ad2d-8780cd1b965b"), new Guid("c923adff-cdf2-45fd-b8a5-e4bb57382288") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8a310af8-81a4-4b50-ad2d-8780cd1b965b"));

            migrationBuilder.DeleteData(
                table: "UserInfos",
                keyColumn: "Id",
                keyValue: new Guid("c923adff-cdf2-45fd-b8a5-e4bb57382288"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("fa995300-4d41-4105-a3a0-6b78293ba5cb"));
        }
    }
}
