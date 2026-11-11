
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM.Models
{
    [Table("BankAccounts")]
    public class BankAccount
    {
        [Key]
        public int BankAccountId { get; set; }

        [Required]
        public int BankId { get; set; }

        [Required]
        [StringLength(64)]
        [Column(TypeName = "varchar(64)")]
        public string AccountNumberHash { get; set; } = string.Empty;

        [Required]
        [StringLength(4)]
        [Column(TypeName = "char(4)")]
        public string AccountNumberLast4 { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string AccountHolderName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        // Relationship
        [ForeignKey(nameof(BankId))]
        public Bank? Bank { get; set; }
    }
}
