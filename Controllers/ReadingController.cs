using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Authorization;

namespace Library.Controllers
{
    public class ReadingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public ReadingController(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        // GET: Reading/Read/5
        [Authorize]
        public async Task<IActionResult> Read(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var book = await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genre)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (book == null)
                {
                    return NotFound();
                }

                if (string.IsNullOrEmpty(book.FileName))
                {
                    TempData["ErrorMessage"] = "Файл книги не загружен.";
                    return RedirectToAction("Details", "Books", new { id = book.Id });
                }

                var filePath = _fileService.GetFilePath(book.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    TempData["ErrorMessage"] = $"Файл книги не найден: {filePath}";
                    return RedirectToAction("Details", "Books", new { id = book.Id });
                }

                ViewBag.Book = book;
                ViewBag.FileUrl = _fileService.GetFileUrl(book.FileName);
                ViewBag.ContentType = _fileService.GetContentType(book.FileName);

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ошибка при загрузке страницы чтения: {ex.Message}";
                return RedirectToAction("Details", "Books", new { id = id });
            }
        }

        // GET: Reading/Download/5
        [Authorize]
        public async Task<IActionResult> Download(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null || string.IsNullOrEmpty(book.FileName))
            {
                return NotFound();
            }

            var filePath = _fileService.GetFilePath(book.FileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var contentType = _fileService.GetContentType(book.FileName);
            var fileName = $"{book.Title}.{Path.GetExtension(book.FileName).TrimStart('.')}";

            return File(fileBytes, contentType, fileName);
        }

        // GET: Reading/Upload/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upload(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);
                
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Reading/Upload/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upload(int id, IFormFile file)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);
                
            if (book == null)
            {
                return NotFound();
            }

            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Пожалуйста, выберите файл для загрузки.");
                return View(book);
            }

            // Проверяем тип файла
            var allowedExtensions = new[] { ".pdf", ".epub", ".txt", ".doc", ".docx", ".rtf" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            
            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("", "Поддерживаются только файлы: PDF, EPUB, TXT, DOC, DOCX, RTF");
                return View(book);
            }

            // Проверяем размер файла (максимум 50MB)
            if (file.Length > 50 * 1024 * 1024)
            {
                ModelState.AddModelError("", "Размер файла не должен превышать 50MB.");
                return View(book);
            }

            try
            {
                // Удаляем старый файл если есть
                if (!string.IsNullOrEmpty(book.FileName))
                {
                    await _fileService.DeleteFileAsync(book.FileName);
                }

                // Сохраняем новый файл
                var savedFileName = await _fileService.SaveFileAsync(file, "books");
                
                book.FileName = Path.GetFileName(savedFileName);
                book.FileType = fileExtension.TrimStart('.');
                book.FileSize = file.Length;
                book.FileUrl = _fileService.GetFileUrl(book.FileName);
                book.UpdatedDate = DateTime.Now;

                _context.Update(book);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Файл книги успешно загружен.";
                return RedirectToAction("Details", "Books", new { id = book.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Ошибка при загрузке файла: {ex.Message}");
                return View(book);
            }
        }

        // POST: Reading/DeleteFile/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(book.FileName))
            {
                await _fileService.DeleteFileAsync(book.FileName);
                
                book.FileName = null;
                book.FileType = null;
                book.FileSize = null;
                book.FileUrl = null;
                book.UpdatedDate = DateTime.Now;

                _context.Update(book);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Файл книги удален.";
            }

            return RedirectToAction("Details", "Books", new { id = book.Id });
        }
    }
}
