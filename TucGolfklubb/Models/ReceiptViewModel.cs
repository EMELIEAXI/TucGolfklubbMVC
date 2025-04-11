namespace TucGolfklubb.Models
{
    public class ReceiptViewModel
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? SelectedPaymentMethod { get; set; }

        // Plus eventuell orderinfo:
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public decimal TotalPrice { get; set; }
    }
}
