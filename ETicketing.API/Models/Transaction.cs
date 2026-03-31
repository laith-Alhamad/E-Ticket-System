namespace ETicketing.API.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public string TransactionReference { get; set; } = Guid.NewGuid().ToString();
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending"; // Pending, Success, Failed
        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ConfirmedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? FailureReason { get; set; }

        public ICollection<LedgerEntry> LedgerEntries { get; set; } = new List<LedgerEntry>();

        
    }
}
