using storage_app.Models;
using storage_app.Services;
using storage_app.Utils.Objects;
using storage_app.ViewModels.Actions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace storage_app.ViewModels
{
    internal class ItemDetailViewModel : ViewModelBase
    {
        private Product _product = new();
        public Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged(nameof(Product));
            }
        }

        private Category _selectedCategory = new();
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        private List<Category> _categories = new();
        public List<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        private readonly ICategoryService categoryService;

        public ItemDetailViewModel(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
            GetCategories();
        }

        public void GetCategories()
        {
            var task = Task.Run(async () => await categoryService.GetCategories());
            Categories = task.Result;
        }
    }
}
