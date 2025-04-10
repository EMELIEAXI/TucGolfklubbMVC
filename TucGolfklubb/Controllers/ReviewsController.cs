using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TucGolfklubb.Data;
using TucGolfklubb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis;

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
        public async Task<IActionResult> AddReview(int productId, string comment, int rating)
        {
            if (string.IsNullOrWhiteSpace(comment) || rating < 1 || rating > 5)
            {
                ModelState.AddModelError("", "Kommentar och betyg måste vara giltiga.");
                return RedirectToAction("Index", "Shop", new { categoryId = productId });
            }

            var selectedProduct = await _context.Products.Include(p => p.Category)
                                                         .FirstOrDefaultAsync(p => p.Id == productId);

            if (selectedProduct == null)
            {
                return RedirectToAction("Index", "Shop", new { categoryId = 1 });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var review = new Review
            {
                ProductId = productId,
                UserId = user.Id,
                Comment = comment,
                Rating = rating,
                Date = DateTime.Now
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            var redirectUrl = Url.Action("Index", "Shop", new { categoryId = selectedProduct.CategoryId });

            return Redirect(redirectUrl + $"#product-{productId}");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var review = await _context.Reviews.Include(r => r.User)
                                               .Include(r => r.Product)
                                               .ThenInclude(p => p.Category)
                                               .FirstOrDefaultAsync(r => r.Id == reviewId);

            if (review == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            if (review.UserId != user?.Id)
            {
                return Unauthorized();
            }

            int productId = review.ProductId;
            int? categoryId = review.Product.CategoryId;

            if (!categoryId.HasValue)
            {
                categoryId = 1;
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            var redirectUrl = Url.Action("Index", "Shop", new { categoryId = categoryId.Value });

            return Redirect(redirectUrl + $"#product-{productId}");
        }
    }
}