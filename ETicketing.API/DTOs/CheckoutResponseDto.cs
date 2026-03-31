namespace ETicketing.API.DTOs
{
    public class CheckoutResponseDto
    {
        public int OrderId { get; set; }
        public int TransactionId { get; set; }
        public string TransactionReference { get; set; } = string.Empty;
        public string OrderStatus { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}