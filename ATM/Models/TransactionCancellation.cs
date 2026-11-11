
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM.Models
{
    [Table("TransactionCancellations")]
    public class TransactionCancellation
    {
        [Key]
        public long CancellationId { get; set; }

        [Required]
        public long TransactionId { get; set; }

        [Required]
        [StringLength(255)]
        public string CancellationReason { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal RefundAmount { get; set; }

        [Required]
        [StringLength(20)]
        public string CancellationStatus { get; set; } = string.Empty;

        public DateTime CancelledAt { get; set; }

        // Relationship
        [ForeignKey(nameof(TransactionId))]
        public Transaction? Transaction { get; set; }
    }
}
