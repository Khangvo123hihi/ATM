
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM.Models
{
    [Table("VisaTransactionLogs")]
    public class VisaTransactionLog
    {
        [Key]
        public long VisaLogId { get; set; }

        [Required]
        public int VisaCardId { get; set; }

        public long? TransactionId { get; set; }

        [Required]
        [StringLength(50)]
        public string RequestType { get; set; } = string.Empty;

        [StringLength(100)]
        public string? RequestReference { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? RequestData { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? ResponseData { get; set; }

        [Required]
        [StringLength(20)]
        public string ResponseStatus { get; set; } = string.Empty;

        [StringLength(500)]
        public string? ErrorMessage { get; set; }

        public DateTime CreatedAt { get; set; }

        // Relationships
        [ForeignKey(nameof(VisaCardId))]
        public VisaCard? VisaCard { get; set; }

        [ForeignKey(nameof(TransactionId))]
        public Transaction? Transaction { get; set; }
    }
}
