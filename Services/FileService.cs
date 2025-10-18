using System.IO;

namespace Library.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string folder);
        Task<bool> DeleteFileAsync(string filePath);
        string GetFileUrl(string fileName);
        string GetFilePath(string fileName);
        bool FileExists(string fileName);
        string GetContentType(string fileName);
    }

    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string _uploadsFolder = "uploads/books";

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            var uploadsPath = Path.Combine(_environment.WebRootPath, _uploadsFolder, folder);
            Directory.CreateDirectory(uploadsPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine(_uploadsFolder, folder, fileName).Replace("\\", "/");
        }

        public Task<bool> DeleteFileAsync(string filePath)
        {
            try
            {
                var fullPath = Path.Combine(_environment.WebRootPath, filePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public string GetFileUrl(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;

            return $"/{_uploadsFolder}/books/{fileName}";
        }

        public string GetFilePath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;

            var filePath = Path.Combine(_environment.WebRootPath, _uploadsFolder, "books", fileName);
            return filePath;
        }

        public bool FileExists(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            var filePath = GetFilePath(fileName);
            return File.Exists(filePath);
        }

        public string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".epub" => "application/epub+zip",
                ".txt" => "text/plain",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".rtf" => "application/rtf",
                _ => "application/octet-stream"
            };
        }
    }
}
