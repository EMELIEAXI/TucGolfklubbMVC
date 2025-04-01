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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Flytta Identity-tabeller till schema "TucUserMngt"
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

            // Lämna dina egna modeller i standardschemat (dbo)
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
                new Product { Id = 1, Name = "Drivers", Description = "", Price = 3000.00m, CategoryId = 1 },
                new Product { Id = 2, Name = "Järnklubbor", Description = "", Price = 6000.00m, CategoryId = 1 },
                new Product { Id = 3, Name = "Wedges", Description = "", Price = 1500.00m, CategoryId = 1 },
                new Product { Id = 4, Name = "Putters", Description = "", Price = 2500.00m, CategoryId = 1 },
                new Product { Id = 5, Name = "Tröjor", Description = "", Price = 800.00m, CategoryId = 2 },
                new Product { Id = 6, Name = "Byxor och shorts", Description = "", Price = 1200.00m, CategoryId = 2 },
                new Product { Id = 7, Name = "Jackor", Description = "", Price = 1500.00m, CategoryId = 2 },
                new Product { Id = 8, Name = "Kepsar", Description = "", Price = 300.00m, CategoryId = 2 },
                new Product { Id = 9, Name = "Spikade skor", Description = "", Price = 1800.00m, CategoryId = 3 },
                new Product { Id = 10, Name = "Spikfria skor", Description = "", Price = 1800.00m, CategoryId = 3 },
                new Product { Id = 11, Name = "Stöd och dämpning", Description = "", Price = 1800.00m, CategoryId = 3 },
                new Product { Id = 12, Name = "Caddyväskor", Description = "", Price = 2500.00m, CategoryId = 4 },
                new Product { Id = 13, Name = "Bärväskor", Description = "", Price = 1200.00m, CategoryId = 4 },
                new Product { Id = 14, Name = "Tee", Description = "", Price = 100.00m, CategoryId = 5 },
                new Product { Id = 15, Name = "Handskar", Description = "", Price = 299.00m, CategoryId = 5 },
                new Product { Id = 16, Name = "Paraplyer", Description = "", Price = 500.00m, CategoryId = 5 },
                new Product { Id = 17, Name = "Handdukar", Description = "", Price = 200.00m, CategoryId = 5 },
                new Product { Id = 18, Name = "Bollar", Description = "", Price = 400.00m, CategoryId = 6 },
                new Product { Id = 19, Name = "Puttingmattor", Description = "", Price = 1000.00m, CategoryId = 7 },
                new Product { Id = 20, Name = "Träningsredskap", Description = "", Price = 1000.00m, CategoryId = 7 },
                new Product { Id = 21, Name = "GPS-enheter", Description = "", Price = 2500.00m, CategoryId = 8 },
                new Product { Id = 22, Name = "Avståndsmätare", Description = "", Price = 1500.00m, CategoryId = 8 },
                new Product { Id = 23, Name = "Appar", Description = "", Price = 1000.00m, CategoryId = 8 }
            );

            modelBuilder.Entity<Forum>().HasData(
                new Forum { Id = 1, Title = "Allmänt om golf", Description = "Diskussioner om allt möjligt relaterat till golf" }
                    );

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .IsRequired(false);
        }
    }
}
