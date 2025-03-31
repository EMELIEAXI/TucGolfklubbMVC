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
                new Category { Id = 2, Name = "Bollar" },
                new Category { Id = 3, Name = "Kläder" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Callaway Järnset", Description = "Komplett set...", Price = 5999.00m, CategoryId = 1 },
                new Product { Id = 2, Name = "Titleist Pro V1", Description = "Tourklassad golfboll...", Price = 549.00m, CategoryId = 2 },
                new Product { Id = 3, Name = "Puma Golf Pikétröja", Description = "Andningsaktiv piké...", Price = 399.00m, CategoryId = 3 }
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
