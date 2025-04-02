using TucGolfklubb.Models;
using System.Collections.Generic;

namespace TucGolfklubb.Models
{
    public class ProductShopViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public int? SelectedCategoryId { get; set; }
    }
}
