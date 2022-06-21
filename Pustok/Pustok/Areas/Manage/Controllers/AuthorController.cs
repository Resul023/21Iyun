using Microsoft.AspNetCore.Mvc;
using Pustok.Areas.Manage.ViewModel;
using Pustok.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pustok.Models;
using Microsoft.EntityFrameworkCore;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AuthorController : Controller
    {
        private readonly AppDbContext _context;

        public AuthorController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(_context.Authors.Include(x => x.books).Where(x => !x.IsDeleted).Count() / 2d);
            AuthorViewModel authorVW = new AuthorViewModel
            {
                Authors = _context.Authors.Include(x=>x.books).Where(x=>!x.IsDeleted).Skip((page-1)*2).Take(2).ToList(),
            };
            return View(authorVW);
        }
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Authors author)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Authors.Add(author);
            _context.SaveChanges();
            author.CreatedAt = DateTime.UtcNow.AddHours(4);
            author.ModifiedAt = DateTime.UtcNow.AddHours(4);
            AppUser admin = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            author.ModifiedBy = admin.Id;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id) 
        {
            
            Authors authors = _context.Authors.FirstOrDefault(x => x.Id == id );
            if (authors == null)
            {
                return RedirectToAction("error","home");
            }
            return View(authors);
        }

        [HttpPost]
        public IActionResult Edit(Authors authors)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Authors isExist = _context.Authors.FirstOrDefault(x => x.Id == authors.Id );
            if (isExist == null)
            {
                return RedirectToAction("error", "home");
            }

            isExist.ModifiedAt = DateTime.UtcNow.AddHours(4);
            AppUser admin = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            isExist.ModifiedBy = admin.Id;

            isExist.FullName = authors.FullName;
            isExist.Age = authors.Age;
            _context.SaveChanges();
            

            return RedirectToAction("index");

        }

        public IActionResult Delete(int id) 
        {

            Authors authors = _context.Authors.Include(x=>x.books).Where(x=>!x.IsDeleted).FirstOrDefault(x => x.Id == id);
            if (authors == null)
            {
                return RedirectToAction("error", "home");
            }
            authors.ModifiedAt = DateTime.UtcNow.AddHours(4);
            AppUser admin = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            authors.ModifiedBy = admin.Id;
            return View(authors);
        }

        [HttpPost]
        public IActionResult Delete(Authors author) 
        {
            Authors authors = _context.Authors.Include(x => x.books).FirstOrDefault(x => x.Id == author.Id && !x.IsDeleted);
            if (authors == null)
            {
                return RedirectToAction("error", "home");
            }
            _context.Remove(authors);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
