using System;

namespace storage_app.Models
{
    internal class Product
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; } = new();
        public DateTimeOffset Create_at { get; set; } = DateTimeOffset.Now;

    }
}
