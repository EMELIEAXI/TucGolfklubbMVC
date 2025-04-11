using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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

        public async Task<IActionResult> Index(int? categoryId, int? productId)
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

            var cart = await _context.ShoppingCart
                .Include(c => c.OrderItems)
                .FirstOrDefaultAsync(c => c.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));

            ViewBag.CartItemCount = cart?.OrderItems.Sum(i => i.Quantity) ?? 0;

            return View(viewModel);
        }
    }
}