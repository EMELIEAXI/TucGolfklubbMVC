using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TucGolfklubb.Data.Migrations
{
    /// <inheritdoc />
    public partial class FinalSeedingFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ForumPosts",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ForumPosts",
                columns: new[] { "Id", "Content", "ForumId", "PostedAt", "UserId" },
                values: new object[] { 1, "Vad är den bästa golfbanan i Sverige?", 1, new DateTime(2024, 3, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null });
        }
    }
}
