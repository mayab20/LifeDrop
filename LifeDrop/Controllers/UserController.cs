using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LifeDrop.ViewModels;
using LifeDrop.Data;
using LifeDrop.Models;


[Authorize] 
public class UserController : Controller
{
  private readonly ApplicationDbContext _context;

  public UserController(ApplicationDbContext context)
  {
    _context = context;
  }
  
 [Authorize]
  public IActionResult Profile()
  {
      var userId = User.FindFirst("UserID")?.Value;

      if (userId == null)
          return RedirectToAction("Login", "Account");

      var user = _context.users.FirstOrDefault(u => u.Id == int.Parse(userId));
      if (user == null)
          return NotFound();

      var model = new ProfileViewModel
      {
        IsAdmin=user.IsAdmin,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Username = user.Username,
        Email = user.Email,
        TelNumber = user.TelNumber,
        Gender = user.Gender,
        DateOfBirth = user.DateOfBirth == default
            ? null
            : user.DateOfBirth.ToDateTime(TimeOnly.MinValue),

        BloodGroup = user.BloodGroup,
        Location = user.Location,
        LastDonationDate = user.LastDonationDate,
        DonationCount=user.DonationCount
      };

      return View(model);
  }

  [Authorize]
  [HttpGet]
  public IActionResult Donate()
  {
      var userIdClaim = User.FindFirst("UserID");
      if (userIdClaim == null)
          return RedirectToAction("Login");

      int userId = int.Parse(userIdClaim.Value);

      var user = _context.users.FirstOrDefault(u => u.Id == userId);
      if (user == null)
          return NotFound();

      var viewModel = new DonationViewModel
      {
          FirstName = user.FirstName,
          LastName = user.LastName,
          Email = user.Email,
          BloodGroup = user.BloodGroup,
          Location = user.Location 
      };

      return View(viewModel);
  }


[Authorize]
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Donate(DonationViewModel model)
{
    if (!ModelState.IsValid)
        return View(model);

    var userIdClaim = User.FindFirst("UserID");
    if (userIdClaim == null)
        return RedirectToAction("Login");

    int donorId = int.Parse(userIdClaim.Value);

    var user = _context.users.FirstOrDefault(u => u.Id == donorId);
    if (user == null)
        return NotFound();

    var donation = new Donation
    {
        DonorId = donorId,
        DonationDate = DateOnly.FromDateTime(model.DonationDate),
        DonationLocation = model.Location,
        RecipientId = 1, // Placeholder until recipient logic is added
        IsConfirmed = false
    };

    _context.donations.Add(donation);

    _context.SaveChanges();

    TempData["Success"] = "Thank you for your donation!";
    return RedirectToAction("Donate");
}

}
