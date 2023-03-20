using BooksRating.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace BooksRating.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly BookratingContext _context;
        public ChartController(BookratingContext context) 
        { 
            _context = context;
        }

        [HttpGet("JsonDataGenres")]
        public JsonResult JsonDataGenres()
        {
            var genres = _context.Genres.Include(r=>r.BookGenres).ToList();
            List<object> genBook = new List<object>();
            genBook.Add(new[] { "Жанр", "Кількість книжок" });
            foreach (var c in genres)
            {
                genBook.Add (new object[] { c.Name, c.BookGenres.Count() });
            }
            return new JsonResult(genBook);
        }

        [HttpGet("JsonDataAuthors")]
        public JsonResult JsonDataAuthors()
        {
            var genres = _context.Authors.Include(r => r.BookAuthors).ToList();
            List<object> genBook = new List<object>();
            genBook.Add(new[] { "Автор", "Кількість книжок" });
            foreach (var c in genres)
            {
                genBook.Add(new object[] { c.Name, c.BookAuthors.Count() });
            }
            return new JsonResult(genBook);
        }

        [HttpGet("JsonDataCountrys")]
        public JsonResult JsonDataCountrys()
        {
            var genres = _context.Countries.Include(r => r.Authors).ToList();
            List<object> genBook = new List<object>();
            genBook.Add(new[] { "Країна", "Кількість авторів" });
            foreach (var c in genres)
            {
                genBook.Add(new object[] { c.Name, c.Authors.Count() });
            }
            return new JsonResult(genBook);
        }
    }
}
