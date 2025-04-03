using Microsoft.AspNetCore.Mvc;
using TucGolfklubb.Models;

namespace TucGolfklubb.ViewModels
{
    public class ReceiptViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemViewModel>? OrderItems { get; set; }
    }
}
