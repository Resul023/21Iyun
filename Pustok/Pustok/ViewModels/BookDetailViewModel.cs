using Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.ViewModels
{
    public class BookDetailViewModel
    {
        public Books Book { get; set;}
        public List<Books> RelatedBooks { get; set; }
        public BookCommentViewModel BookComment { get; set; }
    }
}
