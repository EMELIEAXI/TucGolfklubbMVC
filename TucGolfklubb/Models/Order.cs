using Microsoft.Build.Framework;

namespace TucGolfklubb.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public ICollection<OrderItem>? OrderItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
