using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Genre
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string? Description { get; set; }
        
        // Навигационные свойства
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}

