using Microsoft.AspNetCore.Identity;  // For UserManager
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TucGolfklubb.Data;
using TucGolfklubb.Models;
using Microsoft.AspNetCore.Authorization;

namespace TucGolfklubb.Controllers
{
    [Authorize]
    public class ForumPostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;  // Add this line to declare the UserManager

        public ForumPostsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;  // Initialize UserManager
        }

        // GET: ForumPosts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ForumPosts.Include(f => f.Forum).Include(f => f.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ForumPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumPost = await _context.ForumPosts
                .Include(f => f.Forum)
                .Include(f => f.User)
                .Include(fp => fp.Replies)           // Include the Replies collection
                    .ThenInclude(r => r.User)         // Optionally, include the User for each reply
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumPost == null)
            {
                return NotFound();
            }

            return View(forumPost);
        }

        // GET: ForumPosts/Create
        public IActionResult Create(int? forumId)
        {
            if (forumId.HasValue)
            {
                var forum = _context.Forums.FirstOrDefault(f => f.Id == forumId.Value);
                if (forum == null) return NotFound();

                ViewData["ForumId"] = forumId.Value;
                ViewData["ForumTitle"] = forum.Title; // This enables display in the form
            }
            else
            {
                ViewData["ForumId"] = new SelectList(_context.Forums, "Id", "Title");
            }

            return View();
        }

        // POST: ForumPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ForumId,Content,PostedAt")] ForumPost forumPost)
        {
            if (ModelState.IsValid)
            {
                forumPost.UserId = _userManager.GetUserId(User);
                _context.Add(forumPost);
                await _context.SaveChangesAsync();

                // Step 1: Create main activity log for the user
                var activity = new UserActivity
                {
                    UserId = forumPost.UserId,
                    Type = "Post",
                    Content = forumPost.Content.Length > 100 ? forumPost.Content.Substring(0, 100) + "..." : forumPost.Content,
                    ForumPostId = forumPost.Id,
                    CreatedAt = DateTime.Now
                };
                _context.Activities.Add(activity);

                // Step 2: Notify all followers of this user
                var followers = await _context.UserFollows
                    .Where(f => f.FolloweeId == forumPost.UserId)
                    .Select(f => f.FollowerId)
                    .ToListAsync();

                foreach (var followerId in followers)
                {
                    _context.Activities.Add(new UserActivity
                    {
                        UserId = forumPost.UserId,
                        Type = "Post",
                        Content = forumPost.Content.Length > 100 ? forumPost.Content.Substring(0, 100) + "..." : forumPost.Content,
                        ForumPostId = forumPost.Id,
                        CreatedAt = DateTime.Now
                    });
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Forum", new { id = forumPost.ForumId });
            }

            if (forumPost.ForumId != 0)
            {
                ViewData["ForumId"] = forumPost.ForumId;
            }
            else
            {
                ViewData["ForumId"] = new SelectList(_context.Forums, "Id", "Title", forumPost.ForumId);
            }

            return View(forumPost);
        }

        // GET: ForumPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumPost = await _context.ForumPosts.FindAsync(id);
            if (forumPost == null)
            {
                return NotFound();
            }
            ViewData["ForumId"] = new SelectList(_context.Forums, "Id", "Id", forumPost.ForumId);

            return View(forumPost);
        }

        // POST: ForumPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ForumId,Content")] ForumPost formModel)
        {
            if (id != formModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var postToUpdate = await _context.ForumPosts.FindAsync(id);
                if (postToUpdate == null)
                {
                    return NotFound();
                }

                try
                {
                    postToUpdate.Content = formModel.Content;
                    postToUpdate.PostedAt = DateTime.Now;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Just rethrow — we're not doing custom concurrency handling
                    throw;
                }

                // ✅ Go back to the forum page
                return RedirectToAction("Details", "Forum", new { id = formModel.ForumId });
            }

            return View(formModel);
        }

        // GET: ForumPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumPost = await _context.ForumPosts
                .Include(f => f.Forum)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumPost == null)
            {
                return NotFound();
            }

            return View(forumPost);
        }

        // POST: ForumPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var forumPost = await _context.ForumPosts.FindAsync(id);
            if (forumPost == null)
            {
                return NotFound();
            }

            int forumId = forumPost.ForumId; // Save this before deleting

            _context.ForumPosts.Remove(forumPost);
            await _context.SaveChangesAsync();

            // ✅ Redirect back to the inlägg list for that forum
            return RedirectToAction("Details", "Forum", new { id = forumId });
        }

        private bool ForumPostExists(int id)
        {
            return _context.ForumPosts.Any(e => e.Id == id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReply(int forumPostId, string replyContent)
        {
            if (string.IsNullOrWhiteSpace(replyContent))
            {
                // If the reply content is empty, redirect back to the details page.
                return RedirectToAction("Details", new { id = forumPostId });
            }

            var reply = new ForumReply
            {
                ForumPostId = forumPostId,
                Content = replyContent,
                UserId = _userManager.GetUserId(User),
                PostedAt = DateTime.Now
            };

            _context.Replies.Add(reply);
            await _context.SaveChangesAsync();

            // Redirect back to the details page so the new reply will be displayed.
            return RedirectToAction("Details", new { id = forumPostId });
        }

    }
}