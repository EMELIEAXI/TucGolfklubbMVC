using TucGolfklubb.Models;
using System.Collections.Generic;

namespace TucGolfklubb.Models
{
    public class ProductShopViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public int? SelectedCategoryId { get; set; }
        public Product? SelectedProduct { get; set; }
        public List<Review>? Reviews { get; set; }
        public Review NewReview { get; set; } = new Review();

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItem>? OrderItems { get; set; }

        public decimal OrderTotalPrice { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public List<string>? PaymentMethods { get; set; }

    }

}
