using System.ComponentModel.DataAnnotations;

namespace ETicketing.API.DTOs
{
    public class CheckoutRequestDto
    {
        [Required]
        public int TicketId { get; set; }

        [Range(1,10)]
        public int Quantity { get; set; }

        [Required]
        public string PaymentMethod { get; set; } = string.Empty; // CreditCard or QR
    }
}