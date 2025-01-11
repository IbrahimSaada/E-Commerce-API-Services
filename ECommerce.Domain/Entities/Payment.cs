namespace ECommerce.Domain.Entities
{
    public class Payment
    {
        public long Id { get; set; }

        public long OrderId { get; set; }
        public Order? Order { get; set; }

        public decimal Amount { get; set; }
        public string Method { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
