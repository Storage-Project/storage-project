using System.Collections.Generic;
using System.Threading.Tasks;

using storage_app.Models;

namespace storage_app.Services
{
    internal class CategoryService : ServiceBase, ICategoryService
    {
        public async Task<List<Category>> GetCategories()
        {
            List<Category> categories = new();

            var _categories = await GetValueAsync<List<Category>>("v1/categories");

            if (_categories != null)
                categories = _categories;

            return categories;
        }
    }
}
