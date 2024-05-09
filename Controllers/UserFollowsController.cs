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
using Microsoft.AspNetCore.Authorization;

namespace Galbaat.Controllers
{
    [Authorize]
    public class UserFollowsController : Controller
    {
        private readonly AppDbContext _context;

        public UserFollowsController(AppDbContext context)
        {
            _context = context;
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
            
            var followRelationship = await _context.UserFollow
                                                .FirstOrDefaultAsync(uf => uf.FollowerId == currentUserId && uf.FollowedId == followingId);
            
            if (followRelationship == null)
            {
                return NotFound(); 
            }

            _context.UserFollow.Remove(followRelationship);
            await _context.SaveChangesAsync();

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


        private bool UserFollowExists(int id)
        {
            return _context.UserFollow.Any(e => e.Id == id);
        }
    }
}
