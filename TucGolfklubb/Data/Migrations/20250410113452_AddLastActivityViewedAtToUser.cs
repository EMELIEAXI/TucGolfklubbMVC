using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TucGolfklubb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLastActivityViewedAtToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastActivityViewedAt",
                schema: "TucUserMngt",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastActivityViewedAt",
                schema: "TucUserMngt",
                table: "Users");
        }
    }
}
