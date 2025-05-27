using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LifeDrop.ViewModels;


[Authorize] 
public class UserController : Controller
{
  public IActionResult Profile()
  {
    return View();
  }

  public IActionResult Donate()
  {
    return View(new DonationViewModel());
  }
    
    [HttpPost]
    public IActionResult Donate(DonationViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Save donation info to the database or send confirmation
            TempData["Success"] = "Thank you for your donation!";
            return RedirectToAction("Donate");
        }

        return View(model);
    }
}
