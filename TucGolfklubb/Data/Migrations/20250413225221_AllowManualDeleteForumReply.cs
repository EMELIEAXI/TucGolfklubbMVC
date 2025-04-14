using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TucGolfklubb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AllowManualDeleteForumReply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Replies_ParentReplyId",
                table: "Replies");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Replies_ParentReplyId",
                table: "Replies",
                column: "ParentReplyId",
                principalTable: "Replies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Replies_ParentReplyId",
                table: "Replies");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Replies_ParentReplyId",
                table: "Replies",
                column: "ParentReplyId",
                principalTable: "Replies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
