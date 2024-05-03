using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Galbaat.Models;
using Galbaat.Data;
using Microsoft.EntityFrameworkCore;

namespace Galbaat.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly GalbaatContext _context;
    public HomeController(ILogger<HomeController> logger, GalbaatContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Post.ToListAsync());     
    }

    
    public IActionResult Following()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
