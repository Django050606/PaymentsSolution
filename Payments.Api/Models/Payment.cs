using System.ComponentModel.DataAnnotations;

namespace Payments.Api.Models
{
    public enum PaymentStatus
    {
        Created,
        Rejected
    }

    public class Payment
    {
        public Guid Id { get; set; } // [cite: 77]

        [Required]
        public string WalletNumber { get; set; } = string.Empty; // [cite: 78]

        [Required]
        public string Account { get; set; } = string.Empty; // [cite: 79]

        [Required]
        public decimal Amount { get; set; } // [cite: 82]

        [Required]
        public string Currency { get; set; } = "RUB"; // [cite: 83]

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty; // [cite: 80]

        [Phone]
        public string? Phone { get; set; } // [cite: 81]

        public string? Comment { get; set; } // [cite: 83]

        public PaymentStatus Status { get; set; } // [cite: 84]

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // [cite: 85]
    }
}