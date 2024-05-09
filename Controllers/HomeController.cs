using Galbaat.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Galbaat.Data;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Security.Claims;

namespace Galbaat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public HomeController(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Post.Include(p => p.AppUser).OrderByDescending(p => p.Id);
            return View(await appDbContext.ToListAsync());
        }

        public string GetCurrentUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;


            if (httpContext!=null &&  httpContext.User.Identity.IsAuthenticated)
            {
                return httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            return null; 
        }
        public async Task<IActionResult> Following()
        {
            var currentUserId = GetCurrentUserId(); 
            
            var followedUsers = _context.UserFollow.Where(uf => uf.FollowerId == currentUserId).Select(uf => uf.FollowedId);

            var postsFromFollowedUsers = _context.Post
                                            .Where(p => followedUsers.Contains(p.AppUserId))
                                            .Include(p => p.AppUser)
                                            .OrderByDescending(p => p.Id);

            return View(await postsFromFollowedUsers.ToListAsync());
        }

        public async Task<IActionResult> Liked()
        {
            var currentUserId = GetCurrentUserId(); 
            
            var likes = _context.Like.Where(l => l.AppUserId == currentUserId).Select(l => l.PostId);

            var postsFromLikes = _context.Post
                                            .Where(p => likes.Contains(p.Id))
                                            .Include(p => p.AppUser)
                                            .OrderByDescending(p => p.Id);

            return View(await postsFromLikes.ToListAsync());

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}