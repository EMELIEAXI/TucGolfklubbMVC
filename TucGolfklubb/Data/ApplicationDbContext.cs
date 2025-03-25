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

            modelBuilder.Entity<ForumPost>()
                .HasOne(fp => fp.User)
                .WithMany()
                .HasForeignKey(fp => fp.UserId)
                .IsRequired(false); // Gör relationen valfri!

            // Seeda kategorier
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Klubbor" },
                new Category { Id = 2, Name = "Bollar" },
                new Category { Id = 3, Name = "Kläder" }
            );

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

            // Seeda forum
            modelBuilder.Entity<Forum>().HasData(
                new Forum
                {
                    Id = 1,
                    Title = "Allmänt om golf",
                    Description = "Diskussioner om allt möjligt relaterat till golf"
                }
            );

            // ForumPost-seeding kommenterad till senare (kräver verkligt UserId)
            // modelBuilder.Entity<ForumPost>().HasData(
            //     new ForumPost
            //     {
            //         Id = 1,
            //         ForumId = 1,
            //         UserId = null,
            //         Content = "Vad är den bästa golfbanan i Sverige?",
            //         PostedAt = new DateTime(2024, 03, 01, 12, 0, 0)
            //     }
            // );
        }
    }
}
