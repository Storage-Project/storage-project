using storage_app.Models;

namespace storage_app.Utils.Objects
{
    internal class Filter
    {
        public Category Category { get; set; } = new();
        public string Description { get; set; } = string.Empty;
    }
}
