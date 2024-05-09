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

    public class LikesController : Controller
    {
        private readonly AppDbContext _context;

        public LikesController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> LikePost(int postId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var existingLike = await _context.Like
                                            .FirstOrDefaultAsync(like => like.AppUserId == currentUserId && like.PostId == postId);
            
            if (existingLike != null)
            {
                _context.Remove(existingLike);
                await _context.SaveChangesAsync();
                return Json(new { liked = false }); 
            }

            if (currentUserId != null) 
            {
                var newLike = new Like
                {
                    AppUserId = currentUserId,
                    PostId = postId
                };
                _context.Like.Add(newLike);
                await _context.SaveChangesAsync();
                return Json(new { liked = true }); 
            }
            return Json(new { liked = false }); 
        }


        private bool LikeExists(int id)
        {
            return _context.Like.Any(e => e.Id == id);
        }
    }
}
