using Microsoft.EntityFrameworkCore;

using storage.Models;
using storage.Repository;
using storage.Exceptions;

namespace storage.Repository
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


        public async Task<Category?> GetCategoryByID(int categoryId)
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

        public async Task<Category?> InsertCategory(Category category)
        {
            var _categories = _context.Categories;
            if (_categories == null)
                throw new InternalServerError();

            try
            {
                await _categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception)
            {
                throw new InternalServerError();
            }
        }

        public async Task<Category?> UpdateCategory(Category category, int id)
        {
            var _categories = _context.Categories;
            if (_categories == null)
                throw new InternalServerError();

            var cat = await _categories.FirstOrDefaultAsync(x => x.Id == id);
            if (cat == null)
                return null;

            try
            {
                cat.Description = category.Description;
                _categories.Update(cat);
                await _context.SaveChangesAsync();
                return cat;
            }
            catch
            {
                throw new InternalServerError();
            }
        }
    }
}