using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.DAL;
using Pustok.Models;
using Pustok.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Controllers
{
    public class BookDetailController : Controller
    {
        private readonly AppDbContext _context;

        public BookDetailController(AppDbContext context)
        {
            this._context = context;
        }

        public IActionResult Detail(int Id) 
        {
            Books book = _context.Book
                .Include(x=>x.BookImages)
                .Include(x=>x.Genre)
                .Include(x=>x.Author)
                .Include(x=>x.BookTags).ThenInclude(x=>x.Tags)
                .FirstOrDefault(x => x.Id == Id);
            BookDetailViewModel bookVW = new BookDetailViewModel
            {
                Book = book,
                RelatedBooks = _context.Book.Include(x => x.Author)
                .Include(x => x.Genre)
                .Include(x => x.BookImages)
                .Where(x => x.GenreId == book.GenreId).Take(6).ToList(),
                BookComment = new BookCommentViewModel {BookId = Id },
            
            };
            return View(bookVW);

        }

        public IActionResult GetBookModal(int id)
        {
            Books book = _context.Book.Include(x => x.Author).Include(x => x.Genre).Include(x => x.BookImages).FirstOrDefault(x=>x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return PartialView("_BookModalPartial",book);
        }

        [HttpPost]
        public async Task<IActionResult> Comment(BookCommentViewModel commentVM) 
        {
            if (!ModelState.IsValid)
            {

            }
        
        }
    }
}
