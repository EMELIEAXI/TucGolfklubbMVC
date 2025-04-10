using Microsoft.AspNetCore.Identity;

namespace TucGolfklubb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? ProfileImagePath { get; set; }

        public ICollection<UserFollow> Followers { get; set; } = new List<UserFollow>();
        public ICollection<UserFollow> Following { get; set; } = new List<UserFollow>();
        public DateTime? LastActivityViewedAt { get; set; }
    }
}
