using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pustok.DAL;
using Pustok.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel homeVW = new HomeViewModel
            {
                promotions = _context.Promotion.ToList(),
                featureds = _context.Featured.ToList(),
                sliders = _context.Slider.ToList(),
                DiscountedBooks = _context.Book.Include(x=>x.Author).Include(x=>x.BookImages).Where(x=>x.DiscountPercent>0).Take(20).ToList(),
                NewBooks = _context.Book.Include(x => x.Author).Include(x => x.BookImages).Where(x => x.IsNew).Take(20).ToList(),
                FeaturedBooks = _context.Book.Include(x => x.Author).Include(x => x.BookImages).Where(x => x.IsFeatured).Take(20).ToList(),

            };

            
            return View(homeVW);
        }


    } 
}
