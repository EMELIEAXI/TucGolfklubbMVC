using Microsoft.AspNetCore.Identity;
using TucGolfklubb.Models;

using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace TucGolfklubb.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public ApplicationUser? User { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        
        private decimal? _totalPrice;
        public decimal TotalPrice
        {
            get => _totalPrice ?? OrderItems.Sum(item => item.Quantity * item.Price);
            set => _totalPrice = value;
        }

        // Method to explicitly recalculate the total price
        public void RecalculateTotalPrice()
        {
            _totalPrice = OrderItems.Sum(item => item.Quantity * item.Price);
        }
    }
}
