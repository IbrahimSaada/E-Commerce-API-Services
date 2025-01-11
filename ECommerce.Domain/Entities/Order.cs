﻿namespace ECommerce.Domain.Entities
{
    public class Order
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User? User { get; set; }

        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
