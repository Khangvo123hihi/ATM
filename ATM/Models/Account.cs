
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ATM.Models
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(30)]
        public string AccountNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string AccountType { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Required]
        [StringLength(3)]
        public string CurrencyCode { get; set; } = "VND";

        [Required]
        [StringLength(20)]
        public string AccountStatus { get; set; } = "Active";

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // SQL Server rowversion
        [Timestamp]
        public byte[]? RowVersion { get; set; }

        // Relationships
        [ForeignKey(nameof(CustomerId))]
        public Customer? Customer { get; set; }

        public ICollection<AtmCard> ATMCards { get; set; }
            = new List<AtmCard>();

        public ICollection<Transaction> Transactions { get; set; }
            = new List<Transaction>();

        public ICollection<TransferTransaction> IncomingTransfers { get; set; }
            = new List<TransferTransaction>();

        public ICollection<TransferTransaction> OutgoingTransfers { get; set; }
            = new List<TransferTransaction>();

        public ICollection<VisaCard> VisaCards { get; set; }
            = new List<VisaCard>();
    }
}
