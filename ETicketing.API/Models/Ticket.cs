using System.ComponentModel.DataAnnotations;

namespace ETicketing.API.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int TotalQuota { get; set; }
        public int RemainingQuota { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}