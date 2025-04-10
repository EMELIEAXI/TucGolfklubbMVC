using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TucGolfklubb.Data;
using TucGolfklubb.Models;

namespace TucGolfklubb.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Profile(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();

            var currentUserId = _userManager.GetUserId(User);
            var isFollowing = await _context.UserFollows
                .AnyAsync(f => f.FollowerId == currentUserId && f.FolloweeId == id);

            var viewModel = new UserProfileViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                IsFollowedByCurrentUser = isFollowing
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ActivityFeed()
        {
            var currentUserId = _userManager.GetUserId(User);

            var followedUserIds = await _context.UserFollows
                .Where(f => f.FollowerId == currentUserId)
                .Select(f => f.FolloweeId)
                .ToListAsync();

            var activities = await _context.Activities
                .Where(a => followedUserIds.Contains(a.UserId!))
                .Include(a => a.User)
                .OrderByDescending(a => a.CreatedAt)
                .Take(50)
                .ToListAsync();

            return View("ActivityFeed", activities);
        }

        // Added Index() below
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
    }
}
