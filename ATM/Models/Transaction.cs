
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        public long TransactionId { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string TransactionCode { get; set; } = string.Empty;

        [Required]
        public int AccountId { get; set; }

        public int? CardId { get; set; }

        [Required]
        [StringLength(30)]
        public string TransactionType { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BalanceBefore { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BalanceAfter { get; set; }

        [Required]
        [StringLength(3)]
        [Column(TypeName = "char(3)")]
        public string CurrencyCode { get; set; } = "VND";

        [Required]
        [StringLength(20)]
        public string TransactionStatus { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        // Relationships
        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }

        [ForeignKey(nameof(CardId))]
        public AtmCard? AtmCard { get; set; }

        public TransactionDetail? TransactionDetail { get; set; }
    }
}
