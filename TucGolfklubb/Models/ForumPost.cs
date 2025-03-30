namespace TucGolfklubb.Models
{

    public class ForumPost
    {
        public int Id { get; set; }
        public int ForumId { get; set; }
        public Forum? Forum { get; set; }

        public string? UserId { get; set; } // OBS: nullable nu!
        public ApplicationUser? User { get; set; }

        public string Content { get; set; } = "";
        public DateTime PostedAt { get; set; } = DateTime.Now;
    }
}
