using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LifeDrop.Models;
using LifeDrop.Data;

namespace LifeDrop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var centers = _context.donationcenters.Take(13).ToList();
        return View(centers);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About() => View();
    public IActionResult Contact() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
