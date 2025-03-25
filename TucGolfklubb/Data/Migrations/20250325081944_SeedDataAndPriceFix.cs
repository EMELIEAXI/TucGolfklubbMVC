using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TucGolfklubb.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataAndPriceFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ForumPosts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PostedAt", "UserId" },
                values: new object[] { new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "system" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ForumPosts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PostedAt", "UserId" },
                values: new object[] { new DateTime(2025, 3, 25, 9, 11, 57, 623, DateTimeKind.Local).AddTicks(5340), "seed-user-id" });
        }
    }
}
