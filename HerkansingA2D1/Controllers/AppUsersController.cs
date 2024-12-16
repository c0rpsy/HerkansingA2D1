using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HerkansingA2D1.Data;
using HerkansingA2D1.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

using HerkansingA2D1.Services;

namespace HerkansingA2D1.Controllers
{
    public class AppUsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly HerkansingA2D1Context _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AppUsersController> _logger;


        public AppUsersController(HerkansingA2D1Context context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOrderService orderService, ILogger<AppUsersController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _orderService = orderService;
            _logger = logger;
        }


        public async Task<IActionResult> Account()
        {
            var orders = await _context.Orders.ToListAsync();
            return View(orders);
        }

        // GET: AppUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: AppUsers/Details/5 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // GET: AppUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppUsers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUser model)
        {
            _logger.LogInformation($"Incoming UserName: '{model.UserName}', Password: '{model.Password}', Email: '{model.Email}'");

            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.UserName.Trim(),
                    NormalizedUserName = model.UserName.Trim().ToUpper(),
                    Email = model.Email.Trim(),
                    NormalizedEmail = model.Email.Trim().ToUpper()
                };

                _logger.LogInformation($"Normalized UserName: '{user.NormalizedUserName}', Normalized Email: '{user.NormalizedEmail}'");

                bool ownerExists = _context.Users.Any(u => u.Role == "Owner");
                user.Role = ownerExists ? "Customer" : "Owner";

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created successfully.");
                    return RedirectToAction("Account");
                }

                foreach (var error in result.Errors)
                {
                    _logger.LogError($"Error: {error.Code} - {error.Description}");
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                _logger.LogWarning("Model state is invalid.");
            }

            return View(model);
        }


        // GET: AppUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            return View(appUser);
        }

        // POST: AppUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,Email,Role")] AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserExists(appUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }

        // GET: AppUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appUser = await _context.Users.FindAsync(id);
            if (appUser != null)
            {
                _context.Users.Remove(appUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        // GET: AppUsers/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: AppUsers/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Email, string Password, bool RememberMe)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, Password, RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Account");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }




        // POST: AppUsers/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
