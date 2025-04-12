using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TucGolfklubb.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedExampleForumPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ForumPosts",
                columns: new[] { "Id", "Content", "ForumId", "PostedAt", "UserId" },
                values: new object[] { 1, "Välkommen till forumet! Vad tycker ni om den nya golfbanan?", 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ForumPosts",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
