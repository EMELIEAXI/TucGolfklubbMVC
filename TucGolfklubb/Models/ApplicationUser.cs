using Microsoft.AspNetCore.Identity;

namespace TucGolfklubb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? ProfileImagePath { get; set; }
    }
}
