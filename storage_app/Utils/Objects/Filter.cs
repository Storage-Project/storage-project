using storage_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storage_app.Utils.Objects
{
    internal class Filter
    {
        public Category Category { get; set; } = new();
        public string Description { get; set; } = string.Empty;
    }
}
