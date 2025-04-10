using System.ComponentModel.DataAnnotations.Schema;

namespace TucGolfklubb.Models
{
    public class UserFollow
    {
        public int Id { get; set; }

        public string? FollowerId { get; set; }
        public string? FolloweeId { get; set; }

        [ForeignKey("FollowerId")]
        public ApplicationUser? Follower { get; set; }

        [ForeignKey("FolloweeId")]
        public ApplicationUser? Followee { get; set; }
    }
}
