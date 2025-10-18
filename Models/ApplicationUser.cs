using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        public string? FirstName { get; set; }
        
        [StringLength(50)]
        public string? LastName { get; set; }
        
        public DateTime? BirthDate { get; set; }
        
        [StringLength(200)]
        public string? Address { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public bool IsActive { get; set; } = true;
        
        public string FullName => $"{FirstName} {LastName}".Trim();
        
        // Навигационные свойства
        public virtual ICollection<BookLoan> Loans { get; set; } = new List<BookLoan>();
    }
}
