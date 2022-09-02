using System;
using System.ComponentModel.DataAnnotations;

using storage.Models;

namespace storage.ViewModels{
    public class CreateProduct{
        [Required]
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [Required]
        public Category Category { get; set; } = new();
    }
}