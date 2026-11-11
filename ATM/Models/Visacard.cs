
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM.Models
{
    [Table("VisaCards")]
    public class VisaCard
    {
        [Key]
        public int VisaCardId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        [StringLength(255)]
        public string CardNumberHash { get; set; } = string.Empty;

        [Required]
        [StringLength(4)]
        public string CardNumberLast4 { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string CardType { get; set; } = string.Empty;

        public DateTime ExpiryDate { get; set; }

        [Required]
        [StringLength(20)]
        public string CardStatus { get; set; } = string.Empty;

        public DateTime IssuedAt { get; set; }

        // Relationships
        [ForeignKey(nameof(CustomerId))]
        public Customer? Customer { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }

        public ICollection<VisaTransactionLog> VisaTransactionLogs { get; set; }
            = new List<VisaTransactionLog>();
    }
}
