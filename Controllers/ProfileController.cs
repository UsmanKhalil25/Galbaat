using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Galbaat.Models;

namespace Galbaat.Controllers;

public class ProfileController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

}
