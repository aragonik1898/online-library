using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        [Required]
        [StringLength(50)]
        public string ISBN { get; set; } = string.Empty;
        
        public int PublicationYear { get; set; }
        
        [StringLength(100)]
        public string? Publisher { get; set; }
        
        public int PageCount { get; set; }
        
        [StringLength(200)]
        public string? CoverImageUrl { get; set; }
        
        [StringLength(500)]
        public string? FileUrl { get; set; }
        
        [StringLength(100)]
        public string? FileName { get; set; }
        
        [StringLength(50)]
        public string? FileType { get; set; }
        
        public long? FileSize { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedDate { get; set; }
        
        public bool IsAvailable { get; set; } = true;
        
        // Внешние ключи
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        
        // Навигационные свойства
        public virtual Author Author { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
        public virtual ICollection<BookLoan> Loans { get; set; } = new List<BookLoan>();
    }
}
