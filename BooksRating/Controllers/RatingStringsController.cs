using BooksRating.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksRating.Controllers
{
    public class RatingStringsController : Controller
    {
        private readonly BookratingContext _context;

        public RatingStringsController(BookratingContext context)
        {
            _context = context;
        }

        // GET: Readers
        public async Task<IActionResult> Index()
        {
            return _context.RatingStrings != null ?
                        View(await _context.RatingStrings.ToListAsync()) :
                        Problem("Entity set 'BookratingContext.RatingStrings'  is null.");
        }

        // GET: Readers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RatingStrings == null)
            {
                return NotFound();
            }

            var reader = await _context.RatingStrings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        // GET: Readers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Readers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rating")] RatingString ratingString)
        {
            var ratingtrings = _context.RatingStrings.ToArray();
            foreach (var b in ratingtrings)
            {
                if (b.Rating == ratingString.Rating)
                    ModelState.AddModelError("Name", "Читач з таким іменем вже існує");
            }
            if (ModelState.IsValid)
            {
                _context.Add(ratingString);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ratingString);
        }

        // GET: Readers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RatingStrings == null)
            {
                return NotFound();
            }

            var ratingString = await _context.RatingStrings.FindAsync(id);
            if (ratingString == null)
            {
                return NotFound();
            }
            return View(ratingString);
        }

        // POST: Readers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Rating")] RatingString ratingString)
        {
            if (id != ratingString.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ratingString);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingStringExists(ratingString.Id))
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
            return View(ratingString);
        }

        // GET: Readers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RatingStrings == null)
            {
                return NotFound();
            }
            //var ratings = _context.Ratings.ToArray();
            //foreach (var b in ratings)
            //{
            //    if (id == b.RatingId)
            //        return Problem("Рейтинг використовується");
            //}
            var ratingString = await _context.RatingStrings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ratingString == null)
            {
                return NotFound();
            }

            return View(ratingString);
        }

        // POST: Readers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RatingStrings == null)
            {
                return Problem("Entity set 'BookratingContext.Readers'  is null.");
            }
            var ratingString = await _context.RatingStrings.FindAsync(id);
            if (ratingString != null)
            {
                _context.RatingStrings.Remove(ratingString);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingStringExists(int id)
        {
            return (_context.RatingStrings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
