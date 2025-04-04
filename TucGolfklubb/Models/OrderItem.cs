namespace TucGolfklubb.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int? ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; internal set; }
        public string? ProductName { get; set; }
    }
}
