using Microsoft.AspNetCore.Mvc;
using Pustok.DAL;
using Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Service
{
    public class LayoutService : Controller
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            this._context = context;
        }

        public List<Genres> GetGenres() 
        {

            return _context.Genres.ToList();
        
        }
        public Dictionary<string, string> GetSetting() 
        {

            return _context.Settings.ToDictionary(x=>x.Key,y=>y.Value);
        
        }
    }
}
