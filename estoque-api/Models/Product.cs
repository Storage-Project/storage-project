using System;
using System.ComponentModel.DataAnnotations;

namespace storage.Models{
    public class Product{
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [Required]
        public Category Category { get; set; } = new();
        public DateTimeOffset Create_at { get; set; } = DateTimeOffset.Now;
        public int SellingCount { get; set; }
       
    }
}