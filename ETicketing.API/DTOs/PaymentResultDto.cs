namespace ETicketing.API.DTOs
{
    public class PaymentResultDto
    {
        public string Status { get; set; } = string.Empty; // Success / Failed / Pending
        public string Message { get; set; } = string.Empty;
        public DateTime? ConfirmedAt { get; set; }
    }
}