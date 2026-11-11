
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ATM.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(20)]
        public string CustomerCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [Required]
        [StringLength(20)]
        public string IdentityNumber { get; set; } = string.Empty;

        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }

        // Relationships
        public ICollection<Account> Accounts { get; set; }
            = new List<Account>();

        public ICollection<VisaCard> VisaCards { get; set; }
            = new List<VisaCard>();
    }
}
