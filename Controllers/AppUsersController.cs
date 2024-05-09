using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Galbaat.Data;
using Galbaat.Models;
using Galbaat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace Galbaat.Controllers
{
    [Authorize]
    public class AppUsersController : Controller
    {
        private readonly AppDbContext _context;

        public AppUsersController(AppDbContext context)
        {
            _context = context;
        }


        // GET: AppUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUser.FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }
            var userPosts = await _context.Post
                                        .Where(p => p.AppUserId == id) 
                                        .OrderByDescending(p=> p.Id)
                                        .ToListAsync();
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isFollowing = await _context.UserFollow
                                    .AnyAsync(uf => uf.FollowerId == currentUserId && uf.FollowedId == id);
            var followers = _context.UserFollow
                                    .Count(uf => uf.FollowedId == id);
            var following = _context.UserFollow
                                    .Count(uf=>uf.FollowerId == id);
            var posts = _context.Post.Count(p=> p.AppUserId == currentUserId);                        
            var viewModel = new UserDetailsViewModel
            {
                AppUser = appUser,
                Posts = userPosts
            };

            
            ViewData["CurrentUserId"] = id;
            ViewData["isFollowing"] = isFollowing;
            ViewData["following"] = following;
            ViewData["followers"] = followers;
            ViewData["posts"] = posts;
            return View(viewModel);
        }

       

        private bool AppUserExists(string id)
        {
            return _context.AppUser.Any(e => e.Id == id);
        }
    }
}
