using BooksRating.Models;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
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
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Name", "Name");
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Name", "Name");
            ViewData["ReaderEmailId"] = new SelectList(_context.Readers, "Email", "Email");
            return View();
        }
        public IActionResult Query1Res(string[] selectedBook1)
        {

            var name = selectedBook1.FirstOrDefault();
            ViewBag.Name = name;
            var result = _context.Database.SqlQueryRaw<string>("SELECT a.Name FROM (Book b inner join BookAuthor ba on ba.BookId=b.Id) inner join Author a  on a.Id=ba.AuthorId  WHERE Title = {0}", name).ToList(); 
            return View(result);
        }

        public IActionResult Query2Res(string[] selectedBook2)
        {

            var name = selectedBook2.FirstOrDefault();
            ViewBag.Name = name;
            var result = _context.Database.SqlQueryRaw<string>("SELECT c.Name FROM Book b inner join BookAuthor ba on ba.BookId=b.Id inner join Author a  on a.Id=ba.AuthorId  inner join Country c  on c.Id=a.CountryId WHERE Title = {0}", name).ToList();
            return View(result);
        }

        public IActionResult Query3Res(string[] selectedAuthor1)
        {

            var name = selectedAuthor1.FirstOrDefault();
            ViewBag.Name = name;
            var result = _context.Database.SqlQueryRaw<int>("SELECT count(*) FROM Book b inner join BookAuthor ba on ba.BookId=b.Id inner join Author a  on a.Id=ba.AuthorId WHERE a.Name = {0}", name).ToList();
            return View(result);
        }

        public IActionResult Query4Res(string[] selectedAuthor2)
        {

            var name = selectedAuthor2.FirstOrDefault();
            ViewBag.Name = name;
            var result = _context.Database.SqlQueryRaw<string>("SELECT distinct g.Name FROM Book b inner join BookAuthor ba on ba.BookId=b.Id inner join Author a  on a.Id=ba.AuthorId inner join BookGenre bg on bg.BookId=b.Id inner join Genre g on bg.GenreId=g.Id WHERE a.Name = {0}", name).ToList();
            return View(result);
        }

        public IActionResult Query5Res(string[] selectedReader)
        {

            var name = selectedReader.FirstOrDefault();
            ViewBag.Name = name;
            var result = _context.Database.SqlQueryRaw<string>("SELECT distinct rs.Rating FROM Reader rd inner join Rating r on rd.Id=r.ReaderId inner join RatingString rs  on r.RatingId=rs.Id WHERE rd.Name = {0}", name).ToList();
            return View(result);
        }
        public IActionResult Query6Res(string[] selectedAuthor3)
        {
            var name = selectedAuthor3.FirstOrDefault();
            ViewBag.Name = name;
            var result = _context.Database.SqlQueryRaw<string>("SELECT Author.Name FROM Author where Author.Name = {0} AND  Author.Id in (Select BookAuthor.AuthorId from BookAuthor group by BookAuthor.AuthorId having count(BookAuthor.BookId)= (select count(id) from Book))", name).ToList();
            return View(result);
        }

        public IActionResult Query7Res(string[] selectedReaderEmail)
        {
            var name = selectedReaderEmail.FirstOrDefault();
            ViewBag.Name = name;
            var result = _context.Database.SqlQueryRaw<string>("SELECT r.Name FROM Reader r WHERE r.Email != {0} AND NOT EXISTS ((SELECT Rating.RatingId   FROM Rating   WHERE Rating.ReaderId = r.Id)  EXCEPT  (SELECT Rating.RatingId   FROM Rating   WHERE  Rating.ReaderId IN  (SELECT Reader.Id  FROM Reader  WHERE Reader.Email = {0}))) AND NOT EXISTS ((SELECT Rating.RatingId   FROM Rating   WHERE  Rating.ReaderId IN   (SELECT Reader.Id  FROM Reader  WHERE Reader.Email = {0}))  EXCEPT  (SELECT Rating.RatingId   FROM Rating   WHERE Rating.ReaderId = r.Id))", name).ToList();
            return View(result);
        }

        public IActionResult Query8Res(string[] selectedReader2)
        {
            var name = selectedReader2.FirstOrDefault();
            var result = _context.Database.SqlQueryRaw<string>("SELECT r.Name FROM Reader r WHERE r.Name = {0} AND NOT EXISTS ((SELECT Rating.RatingId FROM Rating WHERE Rating.ReaderId = r.Id) EXCEPT (SELECT RatingString.Id FROM RatingString)) AND NOT EXISTS ((SELECT RatingString.Id FROM RatingString) EXCEPT (SELECT Rating.RatingId FROM Rating WHERE Rating.ReaderId = r.Id))", name).ToList();
            return View(result);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}