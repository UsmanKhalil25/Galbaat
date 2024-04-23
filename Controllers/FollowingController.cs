using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Galbaat.Models;

namespace Galbaat.Controllers;

public class FollowingController : Controller
{
    private readonly ILogger<FollowingController> _logger;

    public FollowingController(ILogger<FollowingController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
