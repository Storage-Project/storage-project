using FileHelpers;
using System;
using System.Collections.Generic;

namespace storage_app.Models
{
    [DelimitedRecord(";")]
    internal class Product
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [FieldConverter(typeof(CategoryConverter))]
        public Category Category { get; set; } = new();
        [FieldConverter(typeof(DateTimeConverter))]
        public DateTimeOffset Create_at { get; set; } = DateTimeOffset.Now;

        internal class CategoryConverter : ConverterBase
        {
            public override object StringToField(string from)
            {
                return new Category() { Description = from };
            }
            public override string FieldToString(object from)
            {

                return from.ToString();

            }
        }
        public class DateTimeConverter : ConverterBase
        {
            public override object StringToField(string from)
            {
                return Convert.ToDateTime(from);
            }

            public override string FieldToString(object from)
            {
                return from.ToString();
            }

        }

    }
}
