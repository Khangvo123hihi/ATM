
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM.Models
{
    [Table("Banks")]
    public class Bank
    {
        [Key]
        public int BankId { get; set; }

        [Required]
        [StringLength(20)]
        public string BankCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string BankName { get; set; } = string.Empty;

        [StringLength(255)]
        public string? BankLogo { get; set; }

        [StringLength(255)]
        public string? ApiEndpoint { get; set; }

        [Required]
        [StringLength(20)]
        public string BankStatus { get; set; } = "Active";

        public DateTime CreatedAt { get; set; }

        // Relationships
        public ICollection<BankAccount> BankAccounts { get; set; }
            = new List<BankAccount>();

        public ICollection<BankIntegrationLog> BankIntegrationLogs { get; set; }
            = new List<BankIntegrationLog>();
    }
}
