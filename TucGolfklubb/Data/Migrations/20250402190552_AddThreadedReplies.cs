using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TucGolfklubb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddThreadedReplies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentReplyId",
                table: "Replies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ParentReplyId",
                table: "Replies",
                column: "ParentReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Replies_ParentReplyId",
                table: "Replies",
                column: "ParentReplyId",
                principalTable: "Replies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Replies_ParentReplyId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_ParentReplyId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "ParentReplyId",
                table: "Replies");
        }
    }
}
