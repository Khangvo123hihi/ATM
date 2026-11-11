
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM.Models
{
    [Table("TransferTransactions")]
    public class TransferTransaction
    {
        [Key]
        public long TransferId { get; set; }

        [Required]
        public long TransactionId { get; set; }

        [Required]
        public int SourceAccountId { get; set; }

        [Required]
        public int DestinationBankId { get; set; }

        [Required]
        public int DestinationBankAccountId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TransferAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TransferFee { get; set; }

        [StringLength(255)]
        public string? TransferContent { get; set; }

        [StringLength(100)]
        public string? BeneficiaryName { get; set; }

        [Required]
        [StringLength(30)]
        public string TransferStatus { get; set; } = string.Empty;

        [StringLength(100)]
        public string? ExternalReference { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        // Relationships
        [ForeignKey(nameof(TransactionId))]
        public Transaction? Transaction { get; set; }

        [ForeignKey(nameof(SourceAccountId))]
        public Account? SourceAccount { get; set; }

        [ForeignKey(nameof(DestinationBankId))]
        public Bank? DestinationBank { get; set; }

        [ForeignKey(nameof(DestinationBankAccountId))]
        public BankAccount? DestinationBankAccount { get; set; }
    }
}
