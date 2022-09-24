using System.Collections.Generic;
using System.Threading.Tasks;

using storage_app.Models;
using storage_app.Services;

namespace storage_app.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private StorageDataGridViewModel _storageDataGridViewModel;
        public StorageDataGridViewModel StorageDataGridViewModel
        {
            get { return _storageDataGridViewModel; }
            set { 
                _storageDataGridViewModel = value;
                OnPropertyChanged(nameof(StorageDataGridViewModel));
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
        public MainViewModel(IProductService productService, ICategoryService categoryService)
        {
            _storageDataGridViewModel = new(productService);

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
