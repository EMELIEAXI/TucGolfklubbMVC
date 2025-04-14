using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TucGolfklubb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Replies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Forums",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ForumPosts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "ForumPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Forums",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Forums");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ForumPosts");
        }
    }
}
