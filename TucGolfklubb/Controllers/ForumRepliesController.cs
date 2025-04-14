using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TucGolfklubb.Data;
using TucGolfklubb.Models;

namespace TucGolfklubb.Controllers
{
    [Authorize]
    public class ForumRepliesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ForumRepliesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST: ForumReplies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ForumPostId,ParentReplyId,Content")] ForumReply reply)
        {
            if (ModelState.IsValid)
            {
                reply.UserId = _userManager.GetUserId(User);
                reply.PostedAt = DateTime.Now;

                _context.Replies.Add(reply);
                await _context.SaveChangesAsync();

                // Log activity (only once)
                var activity = new UserActivity
                {
                    UserId = reply.UserId,
                    Type = reply.ParentReplyId == null ? "Comment" : "Reply",  // Differentiate!
                    Content = reply.Content.Length > 100 ? reply.Content.Substring(0, 100) + "..." : reply.Content,
                    ForumPostId = reply.ForumPostId,
                    ForumReplyId = reply.Id,
                    CreatedAt = DateTime.Now
                };
                _context.Activities.Add(activity);

                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "ForumPosts", new { id = reply.ForumPostId }, fragment: $"reply-{reply.Id}");
            }


            // If validation fails, go back to the forum post page
            return RedirectToAction("Details", "ForumPosts", new { id = reply.ForumPostId });
        }

        // GET: ForumReplies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var reply = await _context.Replies.FindAsync(id);
            if (reply == null || reply.UserId != _userManager.GetUserId(User))
                return Forbid();

            return View(reply);
        }

        // POST: ForumReplies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content")] ForumReply reply)
        {
            var existing = await _context.Replies.FindAsync(id);
            if (existing == null || existing.UserId != _userManager.GetUserId(User))
                return Forbid();

            existing.Content = reply.Content;
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "ForumPosts", new { id = existing.ForumPostId });
        }

        // GET: ForumReplies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var reply = await _context.Replies
                .Include(r => r.ForumPost)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reply == null || reply.UserId != _userManager.GetUserId(User))
                return Forbid();

            return View(reply);
        }

        // POST: ForumReplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reply = await _context.Replies.FindAsync(id);
            if (reply == null || reply.UserId != _userManager.GetUserId(User))
                return Forbid();

            reply.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "ForumPosts", new { id = reply.ForumPostId });
        }
    }
}
