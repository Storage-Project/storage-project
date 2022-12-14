using System;
using System.ComponentModel.DataAnnotations;

using storage.Models;

namespace storage.Dto{
    public class UpdateProduct{
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; } = new();
        public int SellingCount { get; set; }
    }
}