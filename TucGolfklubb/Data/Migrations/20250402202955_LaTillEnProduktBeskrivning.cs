using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TucGolfklubb.Data.Migrations
{
    /// <inheritdoc />
    public partial class LaTillEnProduktBeskrivning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "En putter är en golfklubba som används för att rulla bollen på greenen mot hålet. Den har ett platt klubbhuvud och en kortare shaft än andra klubbor, vilket ger mer kontroll och precision vid kortare slag. Putterklubbor är designade för att ge spelaren maximal stabilitet och precision när bollen ska rulla över kortare avstånd, och det är den klubba man använder för att avsluta ett hål. Det finns olika typer av putters, såsom bladputters och malletputters, som skiljer sig åt i både form och design för att passa olika spelares behov och spelstil.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "");
        }
    }
}
