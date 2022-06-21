using Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.ViewModels
{
    public class HomeViewModel
    {
        public List<Featured> featureds { get; set; }
        public List<Promotions> promotions { get; set; }
        public List<Slider> sliders { get; set; }
        public List<Books> DiscountedBooks { get; set; }
        public List<Books> NewBooks { get; set; }
        public List<Books> FeaturedBooks { get; set; }
        
    }
}
