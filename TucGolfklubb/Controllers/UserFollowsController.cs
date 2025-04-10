using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TucGolfklubb.Data;
using TucGolfklubb.Models;

namespace TucGolfklubb.Controllers
{
    [Authorize]
    public class UserFollowsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserFollowsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Follow(string id)
        {
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId == id)
                return BadRequest(); // Can't follow yourself

            var alreadyFollowing = await _context.UserFollows
                .AnyAsync(f => f.FollowerId == currentUserId && f.FolloweeId == id);

            if (!alreadyFollowing)
            {
                var follow = new UserFollow
                {
                    FollowerId = currentUserId,
                    FolloweeId = id
                };

                _context.UserFollows.Add(follow);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Profile", "Users", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Unfollow(string id)
        {
            var currentUserId = _userManager.GetUserId(User);
            var follow = await _context.UserFollows
                .FirstOrDefaultAsync(f => f.FollowerId == currentUserId && f.FolloweeId == id);

            if (follow != null)
            {
                _context.UserFollows.Remove(follow);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Profile", "Users", new { id });
        }
    }
}
