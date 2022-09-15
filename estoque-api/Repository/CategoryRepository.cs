using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using storage.Models;
using storage.Repository;

namespace storage.Controllers
{
    public class CategoryRepository : ICategoryRepository
    {
        private AppDbContext _context;
        public CategoryRepository(AppDbContext context){
            this._context = context;
        }
        public void DeleteCategory(int categoryID)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryByID(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategorys()
        {
            throw new NotImplementedException();
        }

        public void InsertCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}