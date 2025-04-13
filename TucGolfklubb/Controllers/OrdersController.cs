using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TucGolfklubb.Data;
using TucGolfklubb.Models;

namespace TucGolfklubb.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


        // GET: Orders/OrderDetails/5
        public async Task<IActionResult> OrderDetails(int id)
        {
            // Retrieve the order along with its items
            var order = await _context.Orders
                .Include(o => o.OrderItems) // Include the order items to display the purchased products
                .ThenInclude(oi => oi.Product) // Include the product details for each item
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound(); // Return a 404 if the order is not found
            }

            return View(order); // Pass the order details to the view
        }


        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,OrderDate")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }


        //Flytta order från ShoppingCart till Order
        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
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

            if (cart == null || !cart.OrderItems.Any())
            {
                return RedirectToAction("Index", "ShoppingCart");
            }

            var viewModel = new ProductShopViewModel
            {
                OrderItems = cart.OrderItems.ToList() ?? new List<OrderItem>(),
                TotalPrice = cart.OrderItems.Sum(oi => oi.Price * oi.Quantity),
                PaymentMethods = new List<string> { "Kort", "Swish", "Faktura", "Klarna", "PayPal" } // Lägg till betalningsalternativ
            };
            if(cart == null || cart.OrderItems == null || !cart.OrderItems.Any())
            {
                Console.WriteLine("Cart är tom eller saknar OrderItems!");
                return RedirectToAction("Index", "ShoppingCart");
            }

                return View(viewModel);
            }

        // Gå igenom Order och Visa produkterna där i
        [HttpPost]
        public async Task<IActionResult> Receipt(
                string fullName,
                string address,
                string zipCode,
                string city,
                string selectedPaymentMethod)
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

            if (cart == null || !cart.OrderItems.Any())
            {
                return RedirectToAction("Index", "ShoppingCart");
            }

            // Skapa en order
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalPrice = cart.OrderItems.Sum(oi => oi.Price * oi.Quantity),
                OrderItems = cart.OrderItems.Select(oi => new OrderItem
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    Product = oi.Product,
                    ProductName = oi.Product?.Name,
                }).ToList(),
            };

            _context.Orders.Add(order);
            _context.ShoppingCart.Remove(cart);
            await _context.SaveChangesAsync();


            // Log Purchase Activity + Notify Followers
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                // Log activity
                var activity = new UserActivity
                {
                    UserId = user.Id,
                    Type = "Purchase",
                    Content = $"Köpte produkter (Order #{order.Id})",
                    OrderId = order.Id,
                    CreatedAt = DateTime.Now
                };
                _context.Activities.Add(activity);

                // Notify followers
                var followers = await _context.UserFollows
                    .Where(f => f.FolloweeId == user.Id)
                    .Select(f => f.FollowerId)
                    .ToListAsync();

                foreach (var followerId in followers)
                {
                    _context.Notifications.Add(new Notification
                    {
                        UserId = followerId!,
                        Message = $"{user.FullName ?? user.UserName} gjorde ett köp (Order #{order.Id}).",
                        CreatedAt = DateTime.Now,
                        IsRead = false
                    });
                }

                await _context.SaveChangesAsync(); // Save activity + notifications
            }


            // Bygg kvitto-ViewModel med extra info från formuläret
            var viewModel = new ReceiptViewModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems,
                TotalPrice = order.TotalPrice,
                FullName = fullName,
                Address = address,
                ZipCode = zipCode,
                City = city,
                SelectedPaymentMethod = selectedPaymentMethod
            };

            return View("Receipt", viewModel);

        }
    }
}
