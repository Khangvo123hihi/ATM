
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM.Models
{
    [Table("CardPINAttempts")]
    public class CardPinAttempt
    {
        [Key]
        public int AttemptId { get; set; }

        [Required]
        public int CardId { get; set; }

        public bool IsSuccessful { get; set; }

        public DateTime AttemptedAt { get; set; }

        [StringLength(45)]
        public string? IpAddress { get; set; }

        [StringLength(255)]
        public string? FailureReason { get; set; }

        // Relationship
        [ForeignKey(nameof(CardId))]
        public AtmCard? AtmCard { get; set; }
    }
}
