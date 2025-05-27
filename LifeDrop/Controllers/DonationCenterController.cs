using LifeDrop.Data;
using Microsoft.AspNetCore.Mvc;

public class DonationCenterController : Controller
{
    private readonly ApplicationDbContext _context;

    public DonationCenterController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var centers = _context.donationcenters.ToList();
        return View(centers);
    }
}
