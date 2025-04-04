using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TucGolfklubb.Data;
using TucGolfklubb.Models;

namespace TucGolfklubb.Controllers
{
    [Route("ShoppingCart")]
    public class ShoppingCartController : Controller
    {
            private readonly ApplicationDbContext _context;
            public ShoppingCartController(ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IActionResult> Index()
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var cart = await _context.ShoppingCart
                    .Include(c => c.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                return View(cart); // <---- Skickar varukorgen till vyn!
            }

            [HttpPost]
        [Route("ShoppingCart/AddToCart")]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account"); // Om användaren inte är inloggad, skicka till login
            }

            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound(); // Om produkten inte finns, visa 404
            }

            // Kontrollera om användaren redan har en shopping cart
            var cart = await _context.ShoppingCart
                .Include(c => c.OrderItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    OrderItems = new List<OrderItem>()
                };
                _context.ShoppingCart.Add(cart);
            }

            // Kolla om produkten redan finns i varukorgen
            var existingItem = cart.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity++; // Öka antal om produkten redan finns i varukorgen
            }
            else
            {
                cart.OrderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = 1,
                    Price = product.Price
                });
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(); // Skicka användaren till varukorgen
        }
    }
}
//hej