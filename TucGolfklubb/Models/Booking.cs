namespace TucGolfklubb.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public ApplicationUser? User { get; set; }
        public DateTime BookingDate { get; set; }
        public string? Details { get; set; }
    }
}
