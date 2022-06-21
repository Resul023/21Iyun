using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Areas.Manage.ViewModel;
using Pustok.DAL;
using Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Manage.Controllers
{   
    [Authorize(Roles ="SuperAdmin")]
    [Area("manage")]
    public class CreateAdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CreateAdminController(AppDbContext context,UserManager<AppUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            /*var admins = _userManager.GetUsersInRoleAsync("Admin").Result;*/
            var admin = _userManager.Users.Where(x => x.IsAdmin);
            return View(admin);
        }
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminRegisterViewModel adminVW) 
        {
            AppUser admin = new AppUser 
            {
                UserName = adminVW.UserName,
                FullName = adminVW.FullName,
                IsAdmin = true
            };
            var result = await _userManager.CreateAsync(admin, adminVW.Password);
            if (!result.Succeeded)
            {
                foreach (var eror in result.Errors)
                {
                    ModelState.AddModelError("", eror.Description);

                }
                return View();
            }
            await _userManager.AddToRoleAsync(admin, "Admin");
            return RedirectToAction("index");

        }
    }
}
