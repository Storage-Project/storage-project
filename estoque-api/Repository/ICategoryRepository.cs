using System;
using System.Collections.Generic;
using storage.Models;

namespace storage.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category?> GetCategoryByID(int categoryId);
        Task<Category> InsertCategory(Category category);
        Task<bool> DeleteCategory(int categoryID);
        Task<Category> UpdateCategory(Category category, int id);
        
    }
}