using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Galbaat.Data;
using Galbaat.Models;
using System.Security.Claims;

namespace Galbaat.Controllers
{
    public class UserFollowsController : Controller
    {
        private readonly AppDbContext _context;

        public UserFollowsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserFollows
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserFollow.Include(u => u.Followed).Include(u => u.Follower);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserFollows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFollow = await _context.UserFollow
                .Include(u => u.Followed)
                .Include(u => u.Follower)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userFollow == null)
            {
                return NotFound();
            }

            return View(userFollow);
        }

        // GET: UserFollows/Create
        public IActionResult Create()
        {
            ViewData["FollowedId"] = new SelectList(_context.AppUser, "Id", "Id");
            ViewData["FollowerId"] = new SelectList(_context.AppUser, "Id", "Id");
            return View();
        }

        // POST: UserFollows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FollowerId,FollowedId")] UserFollow userFollow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userFollow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }



            ViewData["FollowedId"] = new SelectList(_context.AppUser, "Id", "Id", userFollow.FollowedId);
            ViewData["FollowerId"] = new SelectList(_context.AppUser, "Id", "Id", userFollow.FollowerId);
            return View(userFollow);
        }

        // GET: UserFollows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFollow = await _context.UserFollow.FindAsync(id);
            if (userFollow == null)
            {
                return NotFound();
            }
            ViewData["FollowedId"] = new SelectList(_context.AppUser, "Id", "Id", userFollow.FollowedId);
            ViewData["FollowerId"] = new SelectList(_context.AppUser, "Id", "Id", userFollow.FollowerId);
            return View(userFollow);
        }

        // POST: UserFollows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FollowerId,FollowedId")] UserFollow userFollow)
        {
            if (id != userFollow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userFollow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserFollowExists(userFollow.Id))
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
            ViewData["FollowedId"] = new SelectList(_context.AppUser, "Id", "Id", userFollow.FollowedId);
            ViewData["FollowerId"] = new SelectList(_context.AppUser, "Id", "Id", userFollow.FollowerId);
            return View(userFollow);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unfollow(string followingId)
        {
            if (followingId == null)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            // Find the follow relationship to remove
            var followRelationship = await _context.UserFollow
                                                .FirstOrDefaultAsync(uf => uf.FollowerId == currentUserId && uf.FollowedId == followingId);
            
            if (followRelationship == null)
            {
                return NotFound(); // Relationship doesn't exist, return appropriate response
            }

            // Remove the follow relationship
            _context.UserFollow.Remove(followRelationship);
            await _context.SaveChangesAsync();

            // Redirect to a page or action method after successful unfollow
            return RedirectToAction("Details", "AppUsers", new { id = followingId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Follow(string followingId)
        {
            if (followingId == null)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var existingFollowRelationship = await _context.UserFollow
                                                            .FirstOrDefaultAsync(uf => uf.FollowerId == currentUserId && uf.FollowedId == followingId);
            
            if (existingFollowRelationship != null)
            {

                return RedirectToAction("Index", "Home"); 
            }

            // If the follow relationship does not exist, create a new one
            if(currentUserId!=null) 
            {
                var newFollowRelationship = new UserFollow
                {
                    FollowerId = currentUserId,
                    FollowedId = followingId
                };
                _context.UserFollow.Add(newFollowRelationship);
                await _context.SaveChangesAsync();
            }


            return RedirectToAction("Details", "AppUsers", new { id = followingId });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFollow = await _context.UserFollow
                .Include(u => u.Followed)
                .Include(u => u.Follower)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userFollow == null)
            {
                return NotFound();
            }

            return View(userFollow);
        }

        // POST: UserFollows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userFollow = await _context.UserFollow.FindAsync(id);
            if (userFollow != null)
            {
                _context.UserFollow.Remove(userFollow);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserFollowExists(int id)
        {
            return _context.UserFollow.Any(e => e.Id == id);
        }
    }
}
