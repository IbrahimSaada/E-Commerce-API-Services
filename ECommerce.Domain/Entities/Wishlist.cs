namespace ECommerce.Domain.Entities
{
    public class Wishlist
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public User? User { get; set; }

        public long ProductId { get; set; }
        public Product? Product { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
