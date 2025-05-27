using LifeDrop.Data;
using LifeDrop.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LifeDrop.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!model.DateOfBirth.HasValue)
            {
                ModelState.AddModelError(nameof(model.DateOfBirth), "Date of Birth is required.");
                return View(model);
            }
            DateOnly dob = DateOnly.FromDateTime(model.DateOfBirth.Value);

            if (!ModelState.IsValid)
                return View(model);

            // Check for duplicate username or email
            if (_context.users.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError(nameof(model.Username), "Username is already taken.");
                return View(model);
            }

            if (_context.users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError(nameof(model.Email), "Email is already registered.");
                return View(model);
            }

            // Hash password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = hashedPassword,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BloodType = model.BloodType,
                TelNumber = model.TelNumber,
                Gender = model.Gender,
                DateOfBirth = dob,
                Location = model.Location,
                LastDonationDate = model.LastDonationDate.HasValue
                    ? model.LastDonationDate.Value.ToDateTime(TimeOnly.MinValue)
                    : null
            };

            _context.users.Add(user);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Registration successful. You can now log in.";
            return RedirectToAction(nameof(Login));
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.users
                .FirstOrDefault(u => u.Username == model.UserNameOrEmail || u.Email == model.UserNameOrEmail);

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.Username),
                    new(ClaimTypes.Email, user.Email),
                    new("UserID", user.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(2)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    authProperties
                );

                TempData["SuccessMessage"] = $"Welcome back, {user.Username}!";
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid username/email or password.");
            return View(model);
        }

        // GET: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["SuccessMessage"] = "You have been logged out.";
            return RedirectToAction(nameof(Login));
        }
    }
}
