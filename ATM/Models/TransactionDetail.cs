
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM.Models
{
    [Table("TransactionDetails")]
    public class TransactionDetail
    {
        [Key]
        public long TransactionDetailId { get; set; }

        [Required]
        public long TransactionId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Fee { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal? ExchangeRate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? ReferenceNumber { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? TerminalId { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        // Relationship
        [ForeignKey(nameof(TransactionId))]
        public Transaction? Transaction { get; set; }
    }
}
