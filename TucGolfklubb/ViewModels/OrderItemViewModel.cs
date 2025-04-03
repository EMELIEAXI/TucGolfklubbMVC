using Microsoft.AspNetCore.Mvc;

namespace TucGolfklubb.ViewModels
{
    public class OrderItemViewModel
    {
        public decimal OrderTotalPrice { get; set; }
        public List<OrderItemViewModel>? OrderItems { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
