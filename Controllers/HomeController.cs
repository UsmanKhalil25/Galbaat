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

namespace Galbaat.Controllers;

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
        ViewData["AppUserId"] = new SelectList(_context.AppUser, "Id", "Id");
        return View(await appDbContext.ToListAsync());
    }

     public string GetCurrentUserId()
    {
        // Get the current HttpContext
        var httpContext = _httpContextAccessor.HttpContext;


        // Check if user is authenticated
        if (httpContext!=null &&  httpContext.User.Identity.IsAuthenticated)
        {
            // Get the user's ID
            return httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        // User is not authenticated or ID is not found
        return null; // Or throw an exception or handle the case based on your application's requirement
    }
    public async Task<IActionResult> Following()
    {
        // Get the current user's ID
        var currentUserId = GetCurrentUserId(); // You need to implement a method to get the current user's ID
        
        // Find the users that the current user follows
        var followedUsers = _context.UserFollow.Where(uf => uf.FollowerId == currentUserId).Select(uf => uf.FollowedId);

        // Retrieve posts from followed users
        var postsFromFollowedUsers = _context.Post
                                        .Where(p => followedUsers.Contains(p.AppUserId))
                                        .Include(p => p.AppUser)
                                        .OrderByDescending(p => p.Id);

        ViewData["AppUserId"] = new SelectList(_context.AppUser, "Id", "Id");
        return View(await postsFromFollowedUsers.ToListAsync());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}