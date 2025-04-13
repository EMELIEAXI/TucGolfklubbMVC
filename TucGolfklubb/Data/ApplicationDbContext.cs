using TucGolfklubb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TucGolfklubb.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<ForumReply> Replies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<UserFollow> UserFollows { get; set; }
        public DbSet<UserActivity> Activities { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Enable cascade delete between Order and OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)       // OrderItem has one Order
                .WithMany(o => o.OrderItems)  // Order has many OrderItems
                .HasForeignKey(oi => oi.OrderId) // Foreign key property in OrderItem
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("Users", "TucUserMngt");
            });
            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable("Roles", "TucUserMngt");
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles", "TucUserMngt");
            });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", "TucUserMngt");
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", "TucUserMngt");
            });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims", "TucUserMngt");
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", "TucUserMngt");
            });
 
            modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)"); 

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ForumPost>()
                .HasOne(fp => fp.User)
                .WithMany()
                .HasForeignKey(fp => fp.UserId)
                .IsRequired(false);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Klubbor" },
                new Category { Id = 2, Name = "Kläder" },
                new Category { Id = 3, Name = "Skor" },
                new Category { Id = 4, Name = "Väskor" },
                new Category { Id = 5, Name = "Tillbehör" },
                new Category { Id = 6, Name = "Bollar" },
                new Category { Id = 7, Name = "Träningsutrustning" },
                new Category { Id = 8, Name = "Elektronik" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Driver – PowerDrive X", Description = "En kraftfull driver med låg spinn och stor sweetspot som förlåter snedträffar. Den aerodynamiska formen ger dig extra fart i svingen utan att du behöver ta i för kung och fosterland. Justerbart loft gör det enkelt att finjustera till just din sving.", Price = 2999.00m, CategoryId = 1, Image="shop-klubbor.jpg", Stock = 7 },
                new Product { Id = 2, Name = "Järnklubba – Precision 7", Description = "Cavity-back-designen ger dig en bra kombination av kontroll och förlåtelse. Lång järn? Distans. Kort järn? Precision. Det är klubbor som växer med dig – oavsett om du är stabil 15 i handicap eller på väg ner mot singel.", Price = 1299.00m, CategoryId = 1, Image = "shop-klubbor.jpg", Stock = 10 },
                new Product { Id = 3, Name = "Wedge – SpinMaster Pro", Description = "Den frästa träffytan ger maximalt grepp och kontroll på korta slag. SpinMaster Pro är din bästa vän från bunkern, ruffen eller fairway 80 meter in. Med flera loft att välja mellan kan du bygga ett närspel som passar just ditt spel.", Price = 1199.00m, CategoryId = 1, Image = "shop-klubbor.jpg", Stock = 5 },
                new Product { Id = 4, Name = "Putter – TrueRoll Classic", Description = "Klassisk bladputter med face-milled träffyta för mjuk bollstart och jämnt rull. Den är lätt att rikta, ger stabil feedback och hjälper dig hålla nerverna i schack på de där svettiga parputtarna. En favorit bland både traditionsälskare och resultatinriktade golfare.", Price = 1499.00m, CategoryId = 1, Image = "shop-klubbor.jpg", Stock = 8 },
                new Product { Id = 5, Name = "Tröja - Fairway Fleece", Description = "Den här mjuka fleecetröjan är framtagen för att ge dig full rörelsefrihet i svingen, samtidigt som den håller dig varm. Perfekt lager att ha under jackan eller över en piké. Diskret klubbmärke på ärmen för en snygg och stilren look.", Price = 499.00m, CategoryId = 2, Image = "shop-kläder.jpg", Stock = 23 },
                new Product { Id = 6, Name = "Byxor - Greenline Flex", Description = "Våra golfbyxor är tillverkade i ett följsamt, stretchigt material som anpassar sig efter ditt spel. Lika sköna att bära som de ser bra ut. Andas väl under varma dagar och har en ren, klassisk look. Självklart med subtil klubbbranding – för dig som spelar med stil.", Price = 649.00m, CategoryId = 2, Image = "shop-kläder.jpg", Stock = 18 },
                new Product { Id = 7, Name = "Jacka - Windbreaker Classic", Description = "Vår klassiska klubbjacka är perfekt för kyliga morgonrundor eller blåsiga eftermiddagar. Lätt, vindtät och vattenavvisande – samtidigt som den andas. Med klubbens broderade logotyp på bröstet visar du stolt var du hör hemma. Passar lika bra på banan som på uteserveringen efter 18 hål.", Price = 699.00m, CategoryId = 2, Image = "shop-kläder.jpg", Stock = 12 },
                new Product { Id = 8, Name = "Keps - Club Cap", Description = "En keps är inte bara praktisk – den är en del av looken. Vår Club Cap skyddar mot sol och ger ett skönt avslappnat intryck. Justerbar passform, slitstarkt tyg och broderad klubbemblem framtill. En självklarhet i varje golfbags utrustning.", Price = 249.00m, CategoryId = 2, Image = "shop-kläder.jpg", Stock = 42 },
                new Product { Id = 9, Name = "Spikade skor – GripTech Tour", Description = "GripTech Tour ger dig stabilitet i varje sving tack vare sina utbytbara softspikes. Skon är vattentät men andas, vilket gör den perfekt för heldagar på banan i alla väder. Ett måste för dig som prioriterar fäste, särskilt på blöta eller kuperade banor.", Price = 1299.00m, CategoryId = 3, Image = "shop-skor.jpg", Stock = 8 },
                new Product { Id = 10, Name = "Spikfria skor – FlexWalk Pro", Description = "FlexWalk Pro är en stilren spikfri golfsko som funkar lika bra på banan som i klubbhuset. Greppig undersula i gummi ger stabilitet utan att du känner dig stel i steget. En favorit bland golfare som vill ha funktion och stil i ett.", Price = 1099.00m, CategoryId = 3, Image = "shop-skor.jpg", Stock = 5 },
                new Product { Id = 11, Name = "Dämpade skor – ComfortDrive Max", Description = "Med extra dämpning i häl och framfot ger ComfortDrive Max överlägsen komfort hela rundan. Mjuk innestruktur och stötdämpande sula minskar tröttheten i fötterna rejält. Idealisk för dig som går mycket på banan.", Price = 1199.00m, CategoryId = 3, Image = "shop-skor.jpg", Stock = 12 },
                new Product { Id = 12, Name = "Caddyväska – ProTour 14", Description = "Stadig och rymlig vagnväska med 14-delad topp och gott om smarta fack. ProTour 14 är perfekt för dig som spelar med vagn eller elvagn och vill ha full översikt. Vattentåliga fickor och integrerat regnskydd ingår.", Price = 2199.00m, CategoryId = 4, Image = "shop-väskor.jpg", Stock = 3 },
                new Product { Id = 13, Name = "Bärväska – CarryLite 6", Description = "CarryLite 6 är en praktisk bärbag med dubbelbärsele, lättviktsmaterial och stabila ben. Trots sin lätta vikt får du plats med klubbor, kläder och tillbehör för en hel runda. Perfekt för snabbrundor och sommarspel.", Price = 1499.00m, CategoryId = 4, Image = "shop-väskor.jpg", Stock = 5 },
                new Product { Id = 14, Name = "Tee – EcoTee 50-pack", Description = "Miljövänliga trätees i klassisk modell – hållbara, snygga och skonsamma mot miljön. Kommer i 50-pack så att du klarar dig över många rundor. Finns i flera längder.", Price = 69.00m, CategoryId = 5, Image = "shop-tillbehör.jpg", Stock = 71 },
                new Product { Id = 15, Name = "Handskar – GripFit Tour Glove", Description = "GripFit-handsken är tillverkad i mjukt cabrettaläder för maximal känsla mot greppet. Den sitter tajt men ventilerar väl, även under varma rundor. En favorit bland både singelhandicapare och nybörjare.", Price = 179.00m, CategoryId = 5, Image = "shop-tillbehör.jpg", Stock = 16 },
                new Product { Id = 16, Name = "Paraply – StormShield XL", Description = "StormShield XL är ett stort, dubbelventilerat paraply som står emot vind och regn. Bekvämt handtag och snabböppning gör det lätt att använda även med en hand. Passar i de flesta vagnhållare.", Price = 349.00m, CategoryId = 5, Image = "shop-tillbehör.jpg", Stock = 37 },
                new Product { Id = 17, Name = "Handduk – ClubTowel Pro", Description = "Mjuk men slitstark mikrofiberhandduk med klubbens logga. Lätt att fästa med karbinhake och torkar snabbt efter rengöring. En självklarhet i varje bag.", Price = 129.00m, CategoryId = 5, Image = "shop-tillbehör.jpg", Stock = 33 },
                new Product { Id = 18, Name = "Bollar – SoftSpin Tour 12-pack", Description = "Tourklassad boll med mjuk kärna och hög spinnkontroll runt green. Passar både låg- och medelhandicapare som vill ha det bästa av två världar. Säljs i 12-pack.", Price = 289.00m, CategoryId = 6, Image = "shop-bollar.jpg", Stock = 55 },
                new Product { Id = 19, Name = "Puttingmatta – HomeGreen Roll-Up", Description = "En slitstark, hoprullbar puttingmatta med markerade avstånd och hål. Perfekt för träning hemma eller på kontoret. Ger en jämn och realistisk rull för bättre känsla.", Price = 699.00m, CategoryId = 7, Image = "shop-träningsutrustning.jpg", Stock = 4 },
                new Product { Id = 20, Name = "Träningsredskap – SwingTrainer Pro", Description = "SwingTrainer Pro hjälper dig bygga upp rytm, timing och styrka i svingen. Fungerar både som uppvärmning före runda och träning hemma. Lätt att använda, men svår att släppa.", Price = 499.00m, CategoryId = 7, Image = "shop-träningsutrustning.jpg", Stock = 14 },
                new Product { Id = 21, Name = "GPS-enhet – GolfNav Mini", Description = "GolfNav Mini är en smidig GPS-klocka med tusentals banor förladdade. Visar avstånd till greenens framkant, mitten och bakkant, samt hinder. Batteritid för flera rundor.", Price = 1499.00m, CategoryId = 8, Image = "shop-elektronik.jpg", Stock = 4 },
                new Product { Id = 22, Name = "Avståndsmätare – PinPoint Laser 600", Description = "Snabb och exakt avståndsmätare med flaggsökning och vibrationsfeedback. Räckvidd upp till 600 meter och tydlig display. Kommer med skyddsfodral och batteri.", Price = 1799.00m, CategoryId = 8, Image = "shop-elektronik.jpg", Stock = 7 },
                new Product { Id = 23, Name = "Extra elektronik – ShotSync SwingSensor", Description = "ShotSync är en smart sensorsnäppa som fästs på klubban och analyserar din sving i realtid. Kopplas till app där du kan se tempo, plan och vinkel. Perfekt för tekniknörden som vill utvecklas på riktigt.", Price = 1099.00m, CategoryId = 8, Image = "shop-elektronik.jpg", Stock = 3 }
            );

            modelBuilder.Entity<Forum>().HasData(
                new Forum { Id = 1, Title = "Allmänt om golf", Description = "Diskussioner om allt möjligt relaterat till golf" }
            );

            modelBuilder.Entity<ForumPost>().HasData(
                new ForumPost
                {
                    Id = 1,
                    Content = "Välkommen till forumet! Vad tycker ni om den nya golfbanan?",
                    PostedAt = new DateTime(2024, 01, 01, 10, 00, 00),
                    ForumId = 1,
                    //UserId = null // Can be null if User is optional
                }
            );

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .IsRequired(false);

            modelBuilder.Entity<ForumReply>()
                .HasOne(r => r.ParentReply)
                .WithMany(r => r.ChildReplies)
                .HasForeignKey(r => r.ParentReplyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFollow>()
                .HasOne(uf => uf.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(uf => uf.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFollow>()
                .HasOne(uf => uf.Followee)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.FolloweeId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}