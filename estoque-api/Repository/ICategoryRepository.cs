using System;
using System.Collections.Generic;
using storage.Models;

namespace storage.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryByID(int categoryId);
        void InsertCategory(Category category);
        Task<bool> DeleteCategory(int categoryID);
        void UpdateCategory(Category category);
        
    }
}