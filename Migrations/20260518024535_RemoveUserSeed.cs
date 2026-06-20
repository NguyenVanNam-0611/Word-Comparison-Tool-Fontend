using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diff_tool.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "IsActive", "PasswordHash", "RoleId", "Username" },
                values: new object[] { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "$2a$11$9G7TvKjqZqZqZqZqZqZqZuKjqZqZqZqZqZqZqZqZqZqZqZqZqZqZ", 1, "admin" });
        }
    }
}
