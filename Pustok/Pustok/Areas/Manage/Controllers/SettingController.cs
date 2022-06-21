using Microsoft.AspNetCore.Mvc;
using Pustok.DAL;
using Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var setting = _context.Settings.ToList();
            
            return View(setting);
        }
        public IActionResult Edit(int Id) 
        {
            var setting = _context.Settings.FirstOrDefault(x=>x.Id == Id);
            if (setting==null)
            {
                return NotFound();
            }
           
            return View(setting);
        
        }

        [HttpPost]
        public IActionResult Edit(Setting setting) 
        {
           /* if (!ModelState.IsValid)
            {
                return View();
            }*/

            var isExists = _context.Settings.FirstOrDefault(x => x.Id == setting.Id);
            if (isExists == null)
            {
                return NotFound();
            }
            isExists.Value = setting.Value;
            _context.SaveChanges();
            return RedirectToAction("index");

        }
    }
}
