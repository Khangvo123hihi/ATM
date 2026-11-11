
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM.Models
{
    [Table("ATMCards")]
    public class AtmCard
    {
        [Key]
        public int CardId { get; set; }

        [Required]
        public int AccountId { get; set; }

        // Không lưu số thẻ ATM gốc
        // Chỉ lưu mã hash và 4 số cuối
        [Required]
        [StringLength(255)]
        public string CardNumberHash { get; set; } = string.Empty;

        [Required]
        [StringLength(4)]
        public string CardNumberLast4 { get; set; } = string.Empty;

        // PIN được mã hóa bằng BCrypt, không lưu PIN dạng chữ thường
        [Required]
        [StringLength(255)]
        public string PinHash { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string CardType { get; set; } = "Debit";

        public DateTime ExpiryDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsLocked { get; set; }

        public int FailedPinAttempts { get; set; }

        public DateTime IssuedAt { get; set; }

        public DateTime? LockedAt { get; set; }

        // Relationship
        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }

        public ICollection<CardPinAttempt> CardPinAttempts { get; set; }
            = new List<CardPinAttempt>();

        public ICollection<Transaction> Transactions { get; set; }
            = new List<Transaction>();
    }
}
