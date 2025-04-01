using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TucGolfklubb.Data.Migrations
{
    /// <inheritdoc />
    public partial class Uppdateradproduktlista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Kläder");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Skor");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Väskor" },
                    { 5, "Tillbehör" },
                    { 6, "Bollar" },
                    { 7, "Träningsutrustning" },
                    { 8, "Elektronik" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Drivers", 3000.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { 1, "", "default.jpg", "Järnklubbor", 6000.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { 1, "", "default.jpg", "Wedges", 1500.00m, 0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 4, 1, "", "default.jpg", "Putters", 2500.00m, 0 },
                    { 5, 2, "", "default.jpg", "Tröjor", 800.00m, 0 },
                    { 6, 2, "", "default.jpg", "Byxor och shorts", 1200.00m, 0 },
                    { 7, 2, "", "default.jpg", "Jackor", 1500.00m, 0 },
                    { 8, 2, "", "default.jpg", "Kepsar", 300.00m, 0 },
                    { 9, 3, "", "default.jpg", "Spikade skor", 1800.00m, 0 },
                    { 10, 3, "", "default.jpg", "Spikfria skor", 1800.00m, 0 },
                    { 11, 3, "", "default.jpg", "Stöd och dämpning", 1800.00m, 0 },
                    { 12, 4, "", "default.jpg", "Caddyväskor", 2500.00m, 0 },
                    { 13, 4, "", "default.jpg", "Bärväskor", 1200.00m, 0 },
                    { 14, 5, "", "default.jpg", "Tee", 100.00m, 0 },
                    { 15, 5, "", "default.jpg", "Handskar", 299.00m, 0 },
                    { 16, 5, "", "default.jpg", "Paraplyer", 500.00m, 0 },
                    { 17, 5, "", "default.jpg", "Handdukar", 200.00m, 0 },
                    { 18, 6, "", "default.jpg", "Bollar", 400.00m, 0 },
                    { 19, 7, "", "default.jpg", "Puttingmattor", 1000.00m, 0 },
                    { 20, 7, "", "default.jpg", "Träningsredskap", 1000.00m, 0 },
                    { 21, 8, "", "default.jpg", "GPS-enheter", 2500.00m, 0 },
                    { 22, 8, "", "default.jpg", "Avståndsmätare", 1500.00m, 0 },
                    { 23, 8, "", "default.jpg", "Appar", 1000.00m, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Bollar");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Kläder");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Komplett set...", "Callaway Järnset", 5999.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 2, "Tourklassad golfboll...", "Titleist Pro V1", 549.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 3, "Andningsaktiv piké...", "Puma Golf Pikétröja", 399.00m });
        }
    }
}
