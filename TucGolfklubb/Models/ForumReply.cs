using System;
using System.ComponentModel.DataAnnotations;

namespace TucGolfklubb.Models
{
    public class ForumReply
    {
        public int Id { get; set; }

        [Required]
        public int ForumPostId { get; set; }

        [Required]
        public string Content { get; set; } = "";

        // Store the replying user's ID
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public DateTime PostedAt { get; set; } = DateTime.Now;

        // Navigation property back to the ForumPost
        public ForumPost? ForumPost { get; set; }
    }
}