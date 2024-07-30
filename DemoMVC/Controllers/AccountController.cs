using DemoMVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using YourNamespace.Models;
using Microsoft.AspNetCore.Authorization;

namespace DemoMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountsContext _context;

        // Constructor injection of AccountsContext
        public AccountController(AccountsContext context)
        {
            _context = context;
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    LoginName = model.LoginName,
                    Password = model.Password // Hash password
                    //UserType = model.UserType
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                // Redirect to profile update page
                return RedirectToAction("Login", new { userId = user.UserID });
            }
            return View(model);
        }

        /*public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {

        }*/

        // GET: Account/Login

        public IActionResult Login()
        {
            return View();
        }
        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid) 
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.LoginName == model.LoginName && u.Password == model.Password);

                if(user != null) 
                {
                    var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, user.LoginName),
                    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString())
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    // return RedirectToAction("Index", "Home");
                    return RedirectToAction("Profile");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Profile
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.UserProfile)
                .FirstOrDefaultAsync(u => u.UserID == int.Parse(userId));

            if (user == null)
            {
                return NotFound();
            }

            var model = new UserProfileViewModel
            {
                UserID = user.UserID,
                LoginName = user.LoginName,
                //Name = user.UserProfile?.Name,
               // UserType = user.UserType // Ensure this is populated

            };

            return View(model);
        }

        /*
        [HttpPost]
        public async Task<IActionResult> UploadPhoto(IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Login", "Account"); // Redirect if not logged in
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == int.Parse(userId));
                if (user == null)
                {
                    return NotFound();
                }

                // Ensure the user is an Artist
                if (user.UserType != "Artist")
                {
                    return Forbid(); // Or redirect to an error page
                }

                // Generate a unique file name
                var fileName = Path.GetFileName(photo.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }
                // Update the user's photo path
                user.Photo = $"/uploads/{fileName}";
                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Profile"); // Redirect back to the profile page
            }

            return BadRequest(); // Handle the case where no file is uploaded
        */

    }
}
