using TucGolfklubb.Models;
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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeda kategorier
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Klubbor" },
                new Category { Id = 2, Name = "Bollar" },
                new Category { Id = 3, Name = "Kläder" }
            );

            // Specificera datatyp för Price
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // Seeda produkter
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Callaway Järnset",
                    Description = "Komplett set med järnklubbor för nybörjare och proffs.",
                    Price = 5999.00m,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Titleist Pro V1",
                    Description = "Tourklassad golfboll med maximal kontroll och känsla.",
                    Price = 549.00m,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 3,
                    Name = "Puma Golf Pikétröja",
                    Description = "Andningsaktiv piké perfekt för varma golfrundor.",
                    Price = 399.00m,
                    CategoryId = 3
                }
            );

            // Seeding för Forum
            modelBuilder.Entity<Forum>().HasData(
                new Forum
                {
                    Id = 1,
                    Title = "Allmänt om golf",
                    Description = "Diskussioner om allt möjligt relaterat till golf"
                }
            );

            // Seeding för ForumPost 
            modelBuilder.Entity<ForumPost>().HasData(
     new ForumPost
     {
         Id = 1,
         ForumId = 1,
         UserId = "system", // se till att detta är tillåtet i din modell
         Content = "Vad är den bästa golfbanan i Sverige?",
         PostedAt = new DateTime(2024, 01, 01, 12, 0, 0)
     }
            );
        }

    }
    
}
