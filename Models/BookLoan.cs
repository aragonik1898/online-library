using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BookLoan
    {
        public int Id { get; set; }
        
        public DateTime LoanDate { get; set; } = DateTime.Now;
        
        public DateTime? ReturnDate { get; set; }
        
        public DateTime DueDate { get; set; }
        
        public bool IsReturned { get; set; } = false;
        
        [StringLength(500)]
        public string? Notes { get; set; }
        
        // Внешние ключи
        public int BookId { get; set; }
        public string UserId { get; set; } = string.Empty;
        
        // Навигационные свойства
        public virtual Book Book { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}

