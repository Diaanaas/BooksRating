using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BooksRating.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BooksRating.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookratingContext _context;

        public BooksController(BookratingContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var bookratingContext = _context.Books.Include(r => r.BookAuthors).ThenInclude(r => r.Author).Include(r => r.BookGenres).ThenInclude(r => r.Genre);
            return View(await bookratingContext.ToListAsync());
            //return _context.Books != null ?
            //              View(await _context.Books.ToListAsync()) :
            //              Problem("Entity set 'BookratingContext.Books'  is null.");
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            var book = new Book();
            book.BookAuthors = new List<BookAuthor>();
            book.BookGenres = new List<BookGenre>();
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,PublishedYear,Description,Cover")] Book book, string[] selectedAuthor, string[] selectedGenre)
        {
            var books =  _context.Books.ToArray();
            foreach (var b in books)
            {
                if(b.Title == book.Title)
                ModelState.AddModelError("Title", "Книга з такою назвою вже існує");
            }
            DateTime date1 = new DateTime(1900, 1, 1);
            if (book.PublishedYear > DateTime.Now || book.PublishedYear < date1)
            {
                ModelState.AddModelError("PublishedYear", "Рік має бути між 1900 та 2023");
            }
            if (selectedAuthor != null)
            {
                book.BookAuthors = new List<BookAuthor>();
                foreach (var author in selectedAuthor)
                {
                    var authorToAdd = new BookAuthor { BookId = book.Id, AuthorId = int.Parse(author) };
                    book.BookAuthors.Add(authorToAdd);
                }
            }
            if (selectedGenre != null)
            {
                book.BookGenres = new List<BookGenre>();
                foreach (var genre in selectedGenre)
                {
                    var genreToAdd = new BookGenre { BookId = book.Id, GenreId = int.Parse(genre) };
                    book.BookGenres.Add(genreToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,PublishedYear,Description,Cover")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }
            DateTime date1 = new DateTime(1900, 1, 1);
            if (book.PublishedYear > DateTime.Now || book.PublishedYear < date1)
            {
                ModelState.AddModelError("PublishedYear", "Рік має бути між 01.01.1900 та поточною датою");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }
            //var ratings = _context.Ratings.ToArray();
            //foreach (var b in ratings)
            //{
            //    if (id == b.BookId)
            //        return Problem("Книга використовується");
            //}
            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'BookratingContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
