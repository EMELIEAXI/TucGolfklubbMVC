using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TucGolfklubb.Data;
using TucGolfklubb.Models;

namespace TucGolfklubb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            return View(order);
        }


        // Generate Receipt (Kvitto)
        public async Task<IActionResult> GenerateReceipt(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User) // include customer
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product) // include product info
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            // Logic to generate a receipt, display it as a view, or generate a PDF
            return View(order); // Show the order details as a receipt
        }

        // Delete Order
        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders
        .Include(o => o.OrderItems)  // Ensure related OrderItems are loaded
        .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Log or handle the exception for better debugging
                return BadRequest($"Error deleting order: {ex.InnerException?.Message}");
            }

            return RedirectToAction(nameof(Index)); // Redirect back to the orders list
        }


        // Edit Order Item
        [HttpPost]
        public async Task<IActionResult> EditOrderItem(int itemId, int quantity)
        {
            // Find the order item by its Id
            var orderItem = await _context.OrderItems
                .Include(oi => oi.Order) // Get the order associated with this item
                .FirstOrDefaultAsync(oi => oi.Id == itemId);

            if (orderItem == null)
            {
                return NotFound();
            }

            // Update the quantity of the order item (do not manually set TotalPrice)
            orderItem.Quantity = quantity;

            // Recalculate the order total
            if (orderItem.Order != null)
            {
                // Load all OrderItems for the order so recalculation is correct
                await _context.Entry(orderItem.Order)
                    .Collection(o => o.OrderItems)
                    .LoadAsync();

                orderItem.Order.RecalculateTotalPrice();
            }

            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Redirect back to the order details page
            return RedirectToAction("Details", new { id = orderItem.Order?.Id });
        }

        // Delete Order Item
        [HttpPost]
        public async Task<IActionResult> DeleteOrderItem(int itemId)
        {
            // 1. Get the order item
            var orderItem = await _context.OrderItems
                .FirstOrDefaultAsync(oi => oi.Id == itemId);

            if (orderItem == null)
            {
                return NotFound();
            }

            // 2. Load the parent order (non-nullable) with OrderItems
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderItem.OrderId);

            if (order == null)
            {
                return NotFound();
            }

            // 3. Remove the order item and save the change
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync(); // Save first so the item is removed from DB

            // 4. Recalculate and save new total
            order.RecalculateTotalPrice();
            await _context.SaveChangesAsync(); // Second save to persist updated total


            // Redirect back to the order details page
            return RedirectToAction("Details", new { id = order.Id });
        }
    }
}
