using System.Diagnostics;
using Library.Models;
using Library.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Получаем последние добавленные книги
            var recentBooks = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Where(b => b.IsAvailable)
                .OrderByDescending(b => b.CreatedDate)
                .Take(6)
                .ToListAsync();

            // Получаем популярные жанры
            var popularGenres = await _context.Genres
                .Include(g => g.Books)
                .OrderByDescending(g => g.Books.Count)
                .Take(5)
                .ToListAsync();

            ViewBag.RecentBooks = recentBooks;
            ViewBag.PopularGenres = popularGenres;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
