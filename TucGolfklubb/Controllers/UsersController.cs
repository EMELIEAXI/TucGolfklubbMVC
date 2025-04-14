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

            var referer = Request.Headers["Referer"].ToString();

            var viewModel = new UserProfileViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                ProfileImagePath = user.ProfileImagePath,
                IsFollowedByCurrentUser = isFollowing,
                RecentActivities = await _context.Activities
                    .Where(a => a.UserId == user.Id)
                    .OrderByDescending(a => a.CreatedAt)
                    .Take(10)
                    .ToListAsync(),
                    PreviousPageUrl = referer
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ActivityFeed()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var currentUserId = currentUser?.Id;

            var followedUserIds = await _context.UserFollows
                .Where(f => f.FollowerId == currentUserId)
                .Select(f => f.FolloweeId)
                .ToListAsync();

            var activities = await _context.Activities
                .Where(a => followedUserIds.Contains(a.UserId!) && a.UserId != currentUserId)
                .Include(a => a.User)
                .OrderByDescending(a => a.CreatedAt)
                .Take(50)
                .ToListAsync();

            // Capture the referer URL (previous page URL)
            var referer = Request.Headers["Referer"].ToString();

            // Pass the referer URL to the view
            ViewData["PreviousPageUrl"] = referer;

            // Null-safe update of LastActivityViewedAt
            if (currentUser != null)
            {
                currentUser.LastActivityViewedAt = DateTime.Now;
                await _userManager.UpdateAsync(currentUser);
            }

            return View("ActivityFeed", activities); // No need to modify activity content here
        }

        // Added Index() below
        public async Task<IActionResult> Index()
        {
            var currentUserId = _userManager.GetUserId(User);

            var users = await _userManager.Users.ToListAsync();

            var followedIds = await _context.UserFollows
                .Where(f => f.FollowerId == currentUserId)
                .Select(f => f.FolloweeId)
                .ToListAsync();

            var viewModels = new List<UserProfileViewModel>();

            foreach (var user in users)
            {
                var isFollowed = followedIds.Contains(user.Id);

                var activities = await _context.Activities
                    .Where(a => a.UserId == user.Id)
                    .OrderByDescending(a => a.CreatedAt)
                    .Take(5)
                    .ToListAsync();

                viewModels.Add(new UserProfileViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    Email = user.Email,
                    ProfileImagePath = user.ProfileImagePath,
                    IsFollowedByCurrentUser = isFollowed,
                    RecentActivities = activities
                });
            }

            return View(viewModels); // now returns List<UserProfileViewModel>
        }
    }
}
