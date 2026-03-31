namespace ETicketing.API.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Pending"; // Pending, Paid, Failed, Cancelled
        public string PaymentMethod { get; set; } = string.Empty; // CreditCard, QR

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
        public string? Notes { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        
    }
}
