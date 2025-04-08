using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TucGolfklubb.Data.Migrations
{
    /// <inheritdoc />
    public partial class Uppdateradproduktkatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "En kraftfull driver med låg spinn och stor sweetspot som förlåter snedträffar. Den aerodynamiska formen ger dig extra fart i svingen utan att du behöver ta i för kung och fosterland. Justerbart loft gör det enkelt att finjustera till just din sving.", "/images/shop-klubbor", "Driver – PowerDrive X", 2999.00m, 7 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "Cavity-back-designen ger dig en bra kombination av kontroll och förlåtelse. Lång järn? Distans. Kort järn? Precision. Det är klubbor som växer med dig – oavsett om du är stabil 15 i handicap eller på väg ner mot singel.", "/images/shop-klubbor", "Järnklubba – Precision 7", 1299.00m, 10 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "Den frästa träffytan ger maximalt grepp och kontroll på korta slag. SpinMaster Pro är din bästa vän från bunkern, ruffen eller fairway 80 meter in. Med flera loft att välja mellan kan du bygga ett närspel som passar just ditt spel.", "/images/shop-klubbor", "Wedge – SpinMaster Pro", 1199.00m, 5 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "Klassisk bladputter med face-milled träffyta för mjuk bollstart och jämnt rull. Den är lätt att rikta, ger stabil feedback och hjälper dig hålla nerverna i schack på de där svettiga parputtarna. En favorit bland både traditionsälskare och resultatinriktade golfare.", "/images/shop-klubbor", "Putter – TrueRoll Classic", 1499.00m, 8 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "Den här mjuka fleecetröjan är framtagen för att ge dig full rörelsefrihet i svingen, samtidigt som den håller dig varm. Perfekt lager att ha under jackan eller över en piké. Diskret klubbmärke på ärmen för en snygg och stilren look.", "/images/shop-kläder", "Tröja - Fairway Fleece", 499.00m, 23 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "Våra golfbyxor är tillverkade i ett följsamt, stretchigt material som anpassar sig efter ditt spel. Lika sköna att bära som de ser bra ut. Andas väl under varma dagar och har en ren, klassisk look. Självklart med subtil klubbbranding – för dig som spelar med stil.", "/images/shop-kläder", "Byxor - Greenline Flex", 649.00m, 18 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "Vår klassiska klubbjacka är perfekt för kyliga morgonrundor eller blåsiga eftermiddagar. Lätt, vindtät och vattenavvisande – samtidigt som den andas. Med klubbens broderade logotyp på bröstet visar du stolt var du hör hemma. Passar lika bra på banan som på uteserveringen efter 18 hål.", "/images/shop-kläder", "Jacka - Windbreaker Classic", 699.00m, 12 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "En keps är inte bara praktisk – den är en del av looken. Vår Club Cap skyddar mot sol och ger ett skönt avslappnat intryck. Justerbar passform, slitstarkt tyg och broderad klubbemblem framtill. En självklarhet i varje golfbags utrustning.", "/images/shop-kläder", "Keps - Club Cap", 249.00m, 42 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "GripTech Tour ger dig stabilitet i varje sving tack vare sina utbytbara softspikes. Skon är vattentät men andas, vilket gör den perfekt för heldagar på banan i alla väder. Ett måste för dig som prioriterar fäste, särskilt på blöta eller kuperade banor.", "/images/shop-skor", "Spikade skor – GripTech Tour", 1299.00m, 8 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "FlexWalk Pro är en stilren spikfri golfsko som funkar lika bra på banan som i klubbhuset. Greppig undersula i gummi ger stabilitet utan att du känner dig stel i steget. En favorit bland golfare som vill ha funktion och stil i ett.", "/images/shop-skor", "Spikfria skor – FlexWalk Pro", 1099.00m, 5 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "Med extra dämpning i häl och framfot ger ComfortDrive Max överlägsen komfort hela rundan. Mjuk innestruktur och stötdämpande sula minskar tröttheten i fötterna rejält. Idealisk för dig som går mycket på banan.", "/images/shop-skor", "Dämpade skor – ComfortDrive Max", 1199.00m, 12 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "Stadig och rymlig vagnväska med 14-delad topp och gott om smarta fack. ProTour 14 är perfekt för dig som spelar med vagn eller elvagn och vill ha full översikt. Vattentåliga fickor och integrerat regnskydd ingår.", "/images/shop-väskor", "Caddyväska – ProTour 14", 2199.00m, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "CarryLite 6 är en praktisk bärbag med dubbelbärsele, lättviktsmaterial och stabila ben. Trots sin lätta vikt får du plats med klubbor, kläder och tillbehör för en hel runda. Perfekt för snabbrundor och sommarspel.", "/images/shop-väskor", "Bärväska – CarryLite 6", 1499.00m, 5 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "Miljövänliga trätees i klassisk modell – hållbara, snygga och skonsamma mot miljön. Kommer i 50-pack så att du klarar dig över många rundor. Finns i flera längder.", "/images/shop-tillbehör", "Tee – EcoTee 50-pack", 69.00m, 71 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "GripFit-handsken är tillverkad i mjukt cabrettaläder för maximal känsla mot greppet. Den sitter tajt men ventilerar väl, även under varma rundor. En favorit bland både singelhandicapare och nybörjare.", "/images/shop-tillbehör", "Handskar – GripFit Tour Glove", 179.00m, 16 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "StormShield XL är ett stort, dubbelventilerat paraply som står emot vind och regn. Bekvämt handtag och snabböppning gör det lätt att använda även med en hand. Passar i de flesta vagnhållare.", "/images/shop-tillbehör", "Paraply – StormShield XL", 349.00m, 37 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "Mjuk men slitstark mikrofiberhandduk med klubbens logga. Lätt att fästa med karbinhake och torkar snabbt efter rengöring. En självklarhet i varje bag.", "/images/shop-tillbehör", "Handduk – ClubTowel Pro", 129.00m, 33 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "Tourklassad boll med mjuk kärna och hög spinnkontroll runt green. Passar både låg- och medelhandicapare som vill ha det bästa av två världar. Säljs i 12-pack.", "/images/shop-bollar", "Bollar – SoftSpin Tour 12-pack", 289.00m, 55 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "En slitstark, hoprullbar puttingmatta med markerade avstånd och hål. Perfekt för träning hemma eller på kontoret. Ger en jämn och realistisk rull för bättre känsla.", "/images/shop-träningsutrustning", "Puttingmatta – HomeGreen Roll-Up", 699.00m, 4 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "SwingTrainer Pro hjälper dig bygga upp rytm, timing och styrka i svingen. Fungerar både som uppvärmning före runda och träning hemma. Lätt att använda, men svår att släppa.", "/images/shop-träningsutrustning", "Träningsredskap – SwingTrainer Pro", 499.00m, 14 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "GolfNav Mini är en smidig GPS-klocka med tusentals banor förladdade. Visar avstånd till greenens framkant, mitten och bakkant, samt hinder. Batteritid för flera rundor.", "/images/shop-elektronik", "GPS-enhet – GolfNav Mini", 1499.00m, 4 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "Snabb och exakt avståndsmätare med flaggsökning och vibrationsfeedback. Räckvidd upp till 600 meter och tydlig display. Kommer med skyddsfodral och batteri.", "/images/shop-elektronik", "Avståndsmätare – PinPoint Laser 600", 1799.00m, 7 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "ShotSync är en smart sensorsnäppa som fästs på klubban och analyserar din sving i realtid. Kopplas till app där du kan se tempo, plan och vinkel. Perfekt för tekniknörden som vill utvecklas på riktigt.", "/images/shop-elektronik", "Extra elektronik – ShotSync SwingSensor", 1099.00m, 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Järnklubbor", 6000.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Wedges", 1500.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "En putter är en golfklubba som används för att rulla bollen på greenen mot hålet. Den har ett platt klubbhuvud och en kortare shaft än andra klubbor, vilket ger mer kontroll och precision vid kortare slag. Putterklubbor är designade för att ge spelaren maximal stabilitet och precision när bollen ska rulla över kortare avstånd, och det är den klubba man använder för att avsluta ett hål. Det finns olika typer av putters, såsom bladputters och malletputters, som skiljer sig åt i både form och design för att passa olika spelares behov och spelstil.", "default.jpg", "Putters", 2500.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Tröjor", 800.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Byxor och shorts", 1200.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Jackor", 1500.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Kepsar", 300.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Spikade skor", 1800.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Spikfria skor", 1800.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Stöd och dämpning", 1800.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Caddyväskor", 2500.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Bärväskor", 1200.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Tee", 100.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Handskar", 299.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Paraplyer", 500.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Handdukar", 200.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Bollar", 400.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Puttingmattor", 1000.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Träningsredskap", 1000.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "GPS-enheter", 2500.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Avståndsmätare", 1500.00m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Description", "Image", "Name", "Price", "Stock" },
                values: new object[] { "", "default.jpg", "Appar", 1000.00m, 0 });
        }
    }
}
