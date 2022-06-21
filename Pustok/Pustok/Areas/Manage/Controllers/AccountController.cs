using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok.Areas.Manage.ViewModel;
using Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }
        public async Task<IActionResult> CreateAdmin()
        {
            AppUser admin = new AppUser
            {
                FullName = "Super Admin",
                UserName = "SPAdmin",
                IsAdmin = true,

            };
            var result = await _userManager.CreateAsync(admin, "Hello123");
            if (!result.Succeeded)
            {
                return Ok(result.Errors);
            }
            await _userManager.AddToRoleAsync(admin,"SuperAdmin");
            return View();
        }
        public async Task<IActionResult> CreateRoles() 
        {
            await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("Member"));
            return Ok();

        }
        public IActionResult Login() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel admin) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = _userManager.Users.FirstOrDefault(x=>x.IsAdmin && x.UserName == admin.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Password or UserName is not correct");
                return View();
            }
           
            var result = await _signInManager.PasswordSignInAsync(user,admin.Password,false,false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Password or UserName is not correct");
                return View();
            }
            return RedirectToAction("index", "Home");

        }
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");

        } 
        public async Task<IActionResult> GetUser() 
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                return Content(user.FullName);
            }
            else
            {
                return Content("You have to login");
            }
        
        }
       
    }
}
