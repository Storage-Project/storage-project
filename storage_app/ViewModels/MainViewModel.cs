using System.Collections.Generic;
using System.Threading.Tasks;

using storage_app.Models;
using storage_app.Services;

namespace storage_app.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private List<Product> _products = new();
        public List<Product> products
        {
            get { return _products; }
            set 
            { 
                _products = value;
                OnPropertyChanged(nameof(products));
            }
        }

        private List<Category> _categories = new();
        public List<Category> categories
        {
            get { return _categories; }
            set 
            { 
                _categories = value; 
                OnPropertyChanged(nameof(categories));
            }
        }

        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        public MainViewModel(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            GetProducts();
            GetCategories();
        }

        public void GetProducts()
        {
            var task = Task.Run(async () => await productService.GetProducts());
            _products = task.Result;
        }
        public void GetCategories()
        {
            var task = Task.Run(async () => await categoryService.GetCategories());
            _categories = task.Result;
        }
    }
}
