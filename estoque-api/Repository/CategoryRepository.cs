using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using storage.Models;
using storage.Repository;
using storage.Exceptions;

namespace storage.Controllers
{
    public class CategoryRepository : ICategoryRepository
    {
        private AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<bool> DeleteCategory(int categoryID)
        {
            var _categories = _context.Categories;
            if (_categories == null)
                throw new InternalServerError();
            var prod = await _categories.FirstOrDefaultAsync(x => x.Id == categoryID);
            if (prod == null)
            {
                return false;
            }
            try
            {
                _categories.Remove(prod);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new InternalServerError();
            }
        }


        public async Task<Category> GetCategoryByID(int categoryId)
        {
            var _categories = _context.Categories;
            if (_categories != null)
            {
                return await _categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == categoryId);
            }
            else
            {
                throw new InternalServerError();
            }
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var _categories = _context.Categories;
            if (_categories != null)
            {
                return await _categories.AsNoTracking().ToListAsync();
            }
            else
            {
                throw new InternalServerError();
            }
        }

        public Task<Category> InsertCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateCategory(Category category, int id)
        {
            throw new NotImplementedException();
        }
    }
}