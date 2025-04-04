using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TucGolfklubb.Data;
using TucGolfklubb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TucGolfklubb.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview(ProductShopViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("ProductShop", "Shop", new { categoryId = model.SelectedProduct?.CategoryId });

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var review = new Review
            {
                ProductId = model.SelectedProduct.Id,
                UserId = user.Id,
                Comment = model.NewReview.Comment,
                Rating = model.NewReview.Rating,
                Date = DateTime.Now
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("ProductShop", "Shop", new { categoryId = model.SelectedProduct.CategoryId });
        }
    }
}