﻿namespace ECommerce.Domain.Entities
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
