using Microsoft.AspNetCore.Identity;
using TucGolfklubb.Models;

namespace TucGolfklubb.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";

        //Gene: add user to recognized
        public ApplicationUser? User { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        //Gene: Make order items non-nullable
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        
        //Gene: Add total amount as calculated property
        public decimal TotalAmount => OrderItems.Sum(item => item.Quantity * item.UnitPrice);
    }
}
