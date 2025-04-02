namespace TucGolfklubb.Models
{
    public class Forum
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; } 
        public ICollection<ForumPost> Posts { get; set; } = new List<ForumPost>();
    }
}
