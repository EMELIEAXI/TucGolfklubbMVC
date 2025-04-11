using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
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
                return new ShoppingCart { UserId = "anonymous", OrderItems = new List<OrderItem>() };
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
        [HttpGet]
        [Route("ShoppingCart/Index")]
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

            var model = new ProductShopViewModel
            {
                OrderItems = cart?.OrderItems.Select(oi => new OrderItem
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList() ?? new List<OrderItem>(),
                OrderTotalPrice = cart?.OrderItems.Sum(oi => oi.Quantity * oi.Price) ?? 0
            };

            return View("_OrderSummary", model);
        }

        [HttpPost]
        [Route("ShoppingCart/AddToCart")]
        [Authorize]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                return RedirectToAction("Index", "Shop");

            await GetShoppingCart();
            var cart = await _context.ShoppingCart
                .Include(c => c.OrderItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            var existingItem = cart.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.OrderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = quantity,
                    Price = product.Price
                });
            }

            await _context.SaveChangesAsync();

            var redirectUrl = Url.Action("Index", "Shop", new { categoryId = product.CategoryId, productId = product.Id });
            return Redirect(redirectUrl + $"#product-{product.Id}");
        }


        [HttpGet]
        [Route("ShoppingCart/ItemCount")]
        public async Task<IActionResult> ItemCount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Json(0);

            var cart = await _context.ShoppingCart
                .Include(c => c.OrderItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            int count = cart?.OrderItems.Sum(oi => oi.Quantity) ?? 0;
            return Json(count);
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

            return BadRequest();
        }
    }

}