using Microsoft.AspNetCore.Mvc;
using LifeDrop.Data;
using LifeDrop.ViewModels;
using LifeDrop.Models;
using Microsoft.EntityFrameworkCore;

public class AdminDashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminDashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var totalUsers = _context.users.Count();
        var totalDonations = _context.donations.Count();
        var bloodGroups = _context.users
            .GroupBy(u => u.BloodGroup)
            .Select(g => new { BloodGroup = g.Key, Count = g.Count() })
            .ToList();

        var model = new AdminDashboardViewModel
        {
            TotalUsers = totalUsers,
            TotalDonations = totalDonations,
            BloodGroupStats = bloodGroups
                .ToDictionary(g => g.BloodGroup ?? "Unknown", g => g.Count)
        };

        return View(model);
    }

    // GET: /AdminDashboard/ManageCenters
    public IActionResult ManageCenters()
    {
        var centers = _context.donationcenters.ToList();
        return View(centers);
    }

    // POST: Add Center
    [HttpPost]
    public IActionResult AddCenter(DonationCenter model)
    {
        if (ModelState.IsValid)
        {
            _context.donationcenters.Add(model);
            _context.SaveChanges();
            return RedirectToAction("ManageCenters");
        }
        return View("ManageCenters", _context.donationcenters.ToList());
    }

    // POST: Delete Center
    [HttpPost]
    public IActionResult DeleteCenter(int id)
    {
        var center = _context.donationcenters.Find(id);
        if (center != null)
        {
            _context.donationcenters.Remove(center);
            _context.SaveChanges();
        }
        return RedirectToAction("ManageCenters");
    }

    // GET: /AdminDashboard/PendingDonations
    public IActionResult PendingDonations()
    {
        var pendingDonations = _context.donations
            .Where(d => !d.IsConfirmed)
            .Include(d => d.Donor)
            .ToList();

        return View(pendingDonations); // create a PendingDonations.cshtml view
    }

    // POST: /AdminDashboard/ConfirmDonation/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ConfirmDonation(int id)
    {
        var donation = _context.donations.FirstOrDefault(d => d.DonationId == id);
        if (donation == null)
            return NotFound();

        var user = _context.users.FirstOrDefault(u => u.Id == donation.DonorId);
        if (user == null)
            return NotFound();

        donation.IsConfirmed = true;
        user.DonationCount += 1;

        _context.SaveChanges();

        TempData["Success"] = "Donation confirmed.";
        return RedirectToAction("PendingDonations");
    }
    

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult HandleRestriction(int userId, string action)
    {
        var user = _context.users.FirstOrDefault(u => u.Id == userId);
        if (user == null) return NotFound();

        if (action == "restrict")
        {
            user.IsRestricted = true;
            TempData["Success"] = $"User {user.Username} has been restricted.";
        }
        else if (action == "unrestrict")
        {
            user.IsRestricted = false;
            TempData["Success"] = $"User {user.Username} has been unrestricted.";
        }

        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
