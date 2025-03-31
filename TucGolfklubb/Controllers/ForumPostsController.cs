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

namespace TucGolfklubb.Controllers
{
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
                // If a forumId is passed, use it directly
                ViewData["ForumId"] = forumId.Value;
            }
            else
            {
                // Otherwise, provide a dropdown list
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
                // Redirect to the Forum details page after successful creation
                return RedirectToAction("Details", "Forum", new { id = forumPost.ForumId });
            }
            // If validation fails, check if ForumId is provided or need to show a dropdown
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,ForumId,Content,PostedAt")] ForumPost forumPost)
        {
            if (id != forumPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forumPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumPostExists(forumPost.Id))
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
            ViewData["ForumId"] = new SelectList(_context.Forums, "Id", "Id", forumPost.ForumId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", forumPost.UserId);
            return View(forumPost);
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
            if (forumPost != null)
            {
                _context.ForumPosts.Remove(forumPost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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