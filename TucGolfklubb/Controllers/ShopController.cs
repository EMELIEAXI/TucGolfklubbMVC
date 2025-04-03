using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TucGolfklubb.Data;
using TucGolfklubb.Models;

namespace TucGolfklubb.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProductShop(int? categoryId)
        {
            var categories = await _context.Categories.ToListAsync();

            //Hämta produkter baserat på kategori-Id
            var products = categoryId.HasValue
                ? await _context.Products.Where(p => p.CategoryId == categoryId.Value).ToListAsync()
                : await _context.Products.ToListAsync();

            var viewModel = new ProductShopViewModel
            {
                Categories = categories,
                Products = products,
                SelectedCategoryId = categoryId
            };

            return View(viewModel);
        }


    }
}
