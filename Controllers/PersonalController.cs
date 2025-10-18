using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Authorization;

namespace Library.Controllers
{
    [Authorize]
    public class PersonalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonalController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Personal/Bookmarks
        public IActionResult Bookmarks(int page = 1, int pageSize = 6)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) 
            {
                TempData["ErrorMessage"] = "Пользователь не найден";
                return RedirectToAction("Login", "Account");
            }

            var bookmarks = _context.Bookmarks
                .Include(bm => bm.Book)
                    .ThenInclude(b => b.Author)
                .Include(bm => bm.Book)
                    .ThenInclude(b => b.Genre)
                .Where(bm => bm.UserId == userId)
                .OrderByDescending(bm => bm.CreatedDate)
                .AsQueryable();

            var pagedResult = PaginationService.GetPagedResult(bookmarks, page, pageSize);

            return View(pagedResult);
        }


        // POST: Personal/AddBookmark
        [HttpPost]
        public async Task<IActionResult> AddBookmark(int bookId, string title, string? description = null, int? pageNumber = null, string? quote = null)
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (userId == null) return Json(new { success = false, message = "Пользователь не найден" });

                // Проверяем, существует ли книга
                var book = await _context.Books.FindAsync(bookId);
                if (book == null) return Json(new { success = false, message = "Книга не найдена" });

                var bookmark = new Bookmark
                {
                    BookId = bookId,
                    UserId = userId,
                    Title = title,
                    Description = description,
                    PageNumber = pageNumber,
                    Quote = quote
                };

                _context.Bookmarks.Add(bookmark);
                await _context.SaveChangesAsync();

                return Json(new { success = true, bookmarkId = bookmark.Id });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Ошибка: {ex.Message}" });
            }
        }

        // POST: Personal/DeleteBookmark
        [HttpPost]
        public async Task<IActionResult> DeleteBookmark(int bookmarkId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Json(new { success = false, message = "Пользователь не найден" });

            var bookmark = await _context.Bookmarks
                .FirstOrDefaultAsync(bm => bm.Id == bookmarkId && bm.UserId == userId);

            if (bookmark != null)
            {
                _context.Bookmarks.Remove(bookmark);
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true });
        }

        // GET: Personal/EditBookmark/5
        public async Task<IActionResult> EditBookmark(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return NotFound();

            var bookmark = await _context.Bookmarks
                .Include(bm => bm.Book)
                .FirstOrDefaultAsync(bm => bm.Id == id && bm.UserId == userId);

            if (bookmark == null) return NotFound();

            return View(bookmark);
        }

        // POST: Personal/EditBookmark/5
        [HttpPost]
        public async Task<IActionResult> EditBookmark(int id, [Bind("Id,Title,Description,PageNumber,Quote")] Bookmark bookmark)
        {
            if (id != bookmark.Id) return NotFound();

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return NotFound();

            var existingBookmark = await _context.Bookmarks
                .FirstOrDefaultAsync(bm => bm.Id == id && bm.UserId == userId);

            if (existingBookmark == null) return NotFound();

            if (ModelState.IsValid)
            {
                existingBookmark.Title = bookmark.Title;
                existingBookmark.Description = bookmark.Description;
                existingBookmark.PageNumber = bookmark.PageNumber;
                existingBookmark.Quote = bookmark.Quote;
                existingBookmark.UpdatedDate = DateTime.Now;

                _context.Update(existingBookmark);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Закладка успешно обновлена.";
                return RedirectToAction("Bookmarks");
            }

            return View(bookmark);
        }
    }
}
