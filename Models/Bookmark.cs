using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Bookmark
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        public int? PageNumber { get; set; }
        
        [StringLength(500)]
        public string? Quote { get; set; } // Цитата из книги
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedDate { get; set; }
        
        // Внешние ключи
        public int BookId { get; set; }
        public string UserId { get; set; } = string.Empty;
        
        // Навигационные свойства
        public virtual Book Book { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}



