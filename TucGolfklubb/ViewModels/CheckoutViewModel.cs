using Microsoft.AspNetCore.Mvc;
using TucGolfklubb.Models;

namespace TucGolfklubb.ViewModels
{
    public class CheckoutViewModel
    {
        public int OrderId { get; set; }  // Lägg till detta
        public DateTime OrderDate { get; set; }  // Lägg till detta
        public List<OrderItemViewModel>? OrderItems { get; set; }
        public decimal CheckoutTotalPrice { get; set; }
    }
}
