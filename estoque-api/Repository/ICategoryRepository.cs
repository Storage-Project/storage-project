using System;
using System.Collections.Generic;
using storage.Models;

namespace storage.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategorys();
        Task<Category> GetCategoryByID(int categoryId);
        void InsertCategory(Category category);
        void DeleteCategory(int categoryID);
        void UpdateCategory(Category category);
        
    }
}