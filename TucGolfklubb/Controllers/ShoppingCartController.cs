using AspNetCoreGeneratedDocument;
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
        public async Task<ShoppingCart> GetShoppingCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return null; // Hantera fallet om användaren inte är inloggad
            }

            var cart = await _context.ShoppingCart
                .Include(c => c.OrderItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId, OrderItems = new List<OrderItem>() };
                _context.ShoppingCart.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
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
                return NotFound();
            }

            // Kontrollera om användaren redan har en shopping cart
            await GetShoppingCart();
            var cart = await _context.ShoppingCart
                .Include(c => c.OrderItems)
                .ThenInclude(oi => oi.Product)
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

            // Skapa en uppdaterad modell att skicka till vy
            var updatedModel = new ProductShopViewModel
            {
                OrderItems = cart.OrderItems.Select(oi => new OrderItem
                {
                    ProductName = oi.Product?.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList() ?? new List<OrderItem>(),
                OrderTotalPrice = cart.OrderItems.Sum(oi => oi.Quantity * oi.Price)
            };

            return View("_OrderSummary", updatedModel); 
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = await _context.ShoppingCart
                .Include(c => c.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                var existingItem = cart.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);
                if (existingItem != null)
                {
                    existingItem.Quantity--;

                    if (existingItem.Quantity <= 0)
                        cart.OrderItems.Remove(existingItem);

                    await _context.SaveChangesAsync();
                }

                var updatedModel = new ProductShopViewModel
                {
                    OrderItems = cart.OrderItems.Select(oi => new OrderItem
                    {
                        ProductId = oi.ProductId,
                        ProductName = oi.Product?.Name,
                        Quantity = oi.Quantity,
                        Price = oi.Price
                    }).ToList(),
                    OrderTotalPrice = cart.OrderItems.Sum(oi => oi.Price * oi.Quantity)
                };

                return PartialView("_OrderSummary", updatedModel); 
            }

            return BadRequest(); // Eller något lämpligt fallback
        }

    }
    
}
//hej