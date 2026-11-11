
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM.Models
{
    [Table("Receipts")]
    public class Receipt
    {
        [Key]
        public long ReceiptId { get; set; }

        [Required]
        public long TransactionId { get; set; }

        [Required]
        [StringLength(30)]
        public string ReceiptNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string ReceiptType { get; set; } = string.Empty;

        [Required]
        public DateTime IssuedAt { get; set; }

        [StringLength(255)]
        public string? ReceiptFilePath { get; set; }

        // Relationship
        [ForeignKey(nameof(TransactionId))]
        public Transaction? Transaction { get; set; }
    }
}
