namespace TucGolfklubb.Models
{
    public class Forum
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public ICollection<ForumPost> Posts { get; set; } = new List<ForumPost>();
    }
}
