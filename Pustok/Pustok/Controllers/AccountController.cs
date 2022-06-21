using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok.Models;
using Pustok.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser>userManager,SignInManager<AppUser>signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }   
        public IActionResult Index()
        {
            AccountIndexViewModel vm = new AccountIndexViewModel
            {
                LoginVW = new MemberLoginViewModel(),
                RegisterVW = new MemberRegisterViewModel()
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterViewModel memberVW) 
        {
            if (!ModelState.IsValid)
            {
                return View("index", new AccountIndexViewModel { RegisterVW = memberVW});
            }

            AppUser newUser = new AppUser 
            { 
                UserName = memberVW.RegisterUserName,
                FullName = memberVW.FullName,
                Email = memberVW.Email,
                
                IsAdmin = false,
            };

            var result = await _userManager.CreateAsync(newUser, memberVW.RegisterPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                   
                }
                return View("index");
            }
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            string url = Url.Action("ConfirmEmail", "Account", new { email = newUser.Email, token = token }, Request.Scheme);
            await _userManager.AddToRoleAsync(newUser, "Member");

            return Ok(new { Url = url });

            return RedirectToAction("index");
        }
        public async Task<IActionResult> ConfirmEmail(string email,string token) 
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Content("This user is not exists");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return RedirectToAction("index");
            }
            else
            {
                return Content("There are problem in process");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(MemberLoginViewModel memberVW)
        {
            if (!ModelState.IsValid)
            {
                return View("index", new AccountIndexViewModel { LoginVW = memberVW , RegisterVW=new MemberRegisterViewModel () });
            }
            AppUser member =  _userManager.Users.FirstOrDefault(x=>!x.IsAdmin&&x.UserName == memberVW.LoginUserName);
            if (member == null)
            {
                ModelState.AddModelError("","UserName or Password is incorrect");
                return View("index", new AccountIndexViewModel { LoginVW = memberVW, RegisterVW = new MemberRegisterViewModel() });
            }
            if (!member.EmailConfirmed)
            {
                ModelState.AddModelError("", "Confirm your email account");
                return View("index", new AccountIndexViewModel { LoginVW = memberVW, RegisterVW = new MemberRegisterViewModel() });
            }
            var result = await _signInManager.PasswordSignInAsync(member, memberVW.LoginPassword,false,false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is incorrect");
                return View("index", new AccountIndexViewModel { LoginVW = memberVW, RegisterVW = new MemberRegisterViewModel() });
            }
            return RedirectToAction("index","home");
        }

        public async Task<IActionResult> Logout() 
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home"); 
        }

        public IActionResult Forgot() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Forgot(MemberForgetPasswordViewModel modelVM) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByEmailAsync(modelVM.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "This email is not exists");
                return View();
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string url = Url.Action("ResetPassword", "Account", new { email = user.Email, token = token }, Request.Scheme);
            return Ok(new { Url = url });
        }

        public async Task<IActionResult> ResetPassword(string email,string token) 
        {
            AppUser isExitsUser = _userManager.Users.FirstOrDefault(x => !x.IsAdmin && x.NormalizedEmail == email.ToUpper());
            if (isExitsUser == null)
            {
                return Content("This user is not exits");
            }
            if (!await _userManager.VerifyUserTokenAsync(isExitsUser,_userManager.Options.Tokens.PasswordResetTokenProvider,"ResetPassword",token))
            {
                return Content("There are problem in process");
            }
            MemberResetPasswordViewModel vm = new MemberResetPasswordViewModel 
            { 
            Email = email,
            Token = token
            };
            return View(vm);
        
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(MemberResetPasswordViewModel resetVM) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user =  _userManager.Users.FirstOrDefault(x => !x.IsAdmin && x.NormalizedEmail == resetVM.Email.ToUpper());
            if (user == null)
            {
                return Content("This user is not exists");
            }
            var result = await _userManager.ResetPasswordAsync(user, resetVM.Token, resetVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            return RedirectToAction("index");
        }
    }
}
