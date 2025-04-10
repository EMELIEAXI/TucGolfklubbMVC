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

        public async Task<IActionResult> ProductShop(int? categoryId, int? productId)
        {
            var categories = await _context.Categories.ToListAsync();
            var productsQuery = _context.Products
                .Where(p => !categoryId.HasValue || p.CategoryId == categoryId.Value)
                .Include(p => p.Reviews)
                .ThenInclude(r => r.User);

            var products = await productsQuery.ToListAsync();

            Product? selectedProduct = null;
            if (productId.HasValue)
            {
                selectedProduct = products.FirstOrDefault(p => p.Id == productId.Value);
            }

            double? averageRating = null;
            if (selectedProduct?.Reviews != null && selectedProduct.Reviews.Any())
            {
                averageRating = selectedProduct.Reviews.Average(r => r.Rating);
            }

            var viewModel = new ProductShopViewModel
            {
                Categories = categories,
                Products = products,
                SelectedCategoryId = categoryId,
                SelectedProduct = selectedProduct,
                Reviews = selectedProduct?.Reviews.ToList() ?? new List<Review>(),
                AverageRating = averageRating
            };

            return View(viewModel);
        }
    }
}