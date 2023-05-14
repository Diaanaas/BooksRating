using BooksRating.Models;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace BooksRating.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly BookratingContext _context;

        public HomeController(ILogger<HomeController> logger, BookratingContext context)
        {
			_logger = logger;
            _context = context;
        }

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

        public IActionResult Query1()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Title", "Title");
            return View();
        }
        public IActionResult Query1Res(string[] selectedBook)
        {
            //using (var command = this._context.Database.GetDbConnection().CreateCommand())
            //{
            //    command.CommandText = "SELECT * FROM Book b inner join BookAuthor ba on ba.BookId=b.Id WHERE Title LIKE '%book%'";
            //    command.CommandType = CommandType.Text;

            //    this._context.Database.OpenConnection();

            //    using (var result = command.ExecuteReader())
            //    {
            //        while (result.Read())
            //        {
                        
            //         }
            //    }
            //}
            var name = "%" + selectedBook.FirstOrDefault()+ "%";
            var bookratingContext = _context.Ratings.Include(r => r.Book).Include(r => r.Reader).Include(r=>r.RatingString);
            var comps = _context.Database.SqlQueryRaw<Book>("SELECT title FROM Book b inner join BookAuthor ba on ba.BookId=b.Id WHERE Title LIKE {0}", name).ToList(); 
            return View(comps);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}