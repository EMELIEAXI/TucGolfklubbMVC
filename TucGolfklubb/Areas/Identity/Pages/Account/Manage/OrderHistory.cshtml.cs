using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TucGolfklubb.Models;
using TucGolfklubb.Data;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace TucGolfklubb.Areas.Identity.Pages.Account.Manage
{
    public class OrderHistoryModel(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context) : PageModel
    {
        
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ApplicationDbContext _context = context;

        public List<Order>? Orders { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                Orders = await _context.Orders
                    .Include(o => o.OrderItems)
                    .Where(o => o.UserId == user.Id)
                    .ToListAsync();

                if (Orders == null)
                {
                    Orders = new List<Order>();
                }
            }
        }

        
        
    }
}
