using Galbaat.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Galbaat.Data;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace Galbaat.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
         _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.Post.Include(p => p.AppUser).OrderByDescending(p => p.Id);
        ViewData["AppUserId"] = new SelectList(_context.AppUser, "Id", "Id");
        return View(await appDbContext.ToListAsync());
    }

    public async Task<IActionResult> Following()
    {
        var appDbContext = _context.Post.Include(p => p.AppUser).OrderByDescending(p => p.Id);
        ViewData["AppUserId"] = new SelectList(_context.AppUser, "Id", "Id");
        return View(await appDbContext.ToListAsync());
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}