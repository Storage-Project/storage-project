using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storage_app.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; } = new();
        public DateTimeOffset Create_at { get; set; } = DateTimeOffset.Now;
    }
}
