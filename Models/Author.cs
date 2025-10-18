using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Author
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Biography { get; set; }
        
        public DateTime? BirthDate { get; set; }
        
        public DateTime? DeathDate { get; set; }
        
        [StringLength(200)]
        public string? Country { get; set; }
        
        public string FullName => $"{FirstName} {LastName}";
        
        // Навигационные свойства
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}

