namespace TucGolfklubb.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public string Comment { get; set; } = "";
        public int Rating { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public ApplicationUser? User { get; set; }
    }
}
