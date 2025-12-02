using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Video.Domain.Migrations
{
    /// <inheritdoc />
    public partial class hasData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Code", "CreatedTime", "Name" },
                values: new object[,]
                {
                    { "8a310af8-81a4-4b50-ad2d-8780cd1b965b", "admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin" },
                    { "b23b5f8c-9c7e-4f1a-8b9d-3e4f2a1c5d6e", "user", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user" }
                });

            migrationBuilder.InsertData(
                table: "UserInfos",
                columns: new[] { "Id", "Avatar", "CreatedTime", "Enable", "Name", "Password", "Username" },
                values: new object[,]
                {
                    { "admin-user-id", "", new DateTime(2022, 10, 16, 11, 33, 54, 0, DateTimeKind.Unspecified), true, "admin", "admin", "admin" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[,]
                {
                    { "admin-role-id", "8a310af8-81a4-4b50-ad2d-8780cd1b965b", "admin-user-id" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: "admin-role-id");

            migrationBuilder.DeleteData(
                table: "UserInfos",
                keyColumn: "Id",
                keyValue: "admin-user-id");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8a310af8-81a4-4b50-ad2d-8780cd1b965b");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "b23b5f8c-9c7e-4f1a-8b9d-3e4f2a1c5d6e");
        }
    }
}