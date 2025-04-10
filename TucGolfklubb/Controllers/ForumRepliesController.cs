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

        // This GET is no longer needed because reply form is inline
        // GET: ForumReplies/Create
        /*
        public IActionResult Create(int forumPostId, int? parentReplyId)
        {
            var reply = new ForumReply
            {
                ForumPostId = forumPostId,
                ParentReplyId = parentReplyId
            };

            return View(reply);
        }
        */

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

                // Log activity
                var activity = new UserActivity
                {
                    UserId = reply.UserId,
                    Type = "Reply",
                    Content = reply.Content.Length > 100 ? reply.Content.Substring(0, 100) + "..." : reply.Content,
                    ForumPostId = reply.ForumPostId,
                    CreatedAt = DateTime.Now
                };

                _context.Activities.Add(activity);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "ForumPosts", new { id = reply.ForumPostId }, fragment: $"reply-{reply.Id}");
            }

            //ViewBag.ForumPostId = reply.ForumPostId;
            //ViewBag.ParentReplyId = reply.ParentReplyId;
            //return View(reply);

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

            _context.Replies.Remove(reply);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "ForumPosts", new { id = reply.ForumPostId });
        }
    }
}
