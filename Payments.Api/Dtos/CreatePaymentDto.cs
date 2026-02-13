using System.ComponentModel.DataAnnotations;

namespace Payments.Api.Dtos
{
    // [cite: 10-17]
    public class CreatePaymentDto
    {
        [Required]
        public string WalletNumber { get; set; } = string.Empty;

        [Required]
        public string Account { get; set; } = string.Empty;

        [Required, Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; } // [cite: 19]

        [Required]
        public string Currency { get; set; } = "RUB";

        
        [Required, EmailAddress] // [cite: 19]
        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }
        public string? Comment { get; set; }
    }
}