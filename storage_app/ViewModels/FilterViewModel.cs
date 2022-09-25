using storage_app.Models;
using storage_app.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace storage_app.ViewModels
{
    internal class FilterViewModel : ViewModelBase
    {
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

        public FilterViewModel(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
            GetCategories();
        }

        public void GetCategories()
        {
            var task = Task.Run(async () => await categoryService.GetCategories());
            _categories = task.Result;
        }
    }
}
