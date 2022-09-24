using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using storage_app.Models;
using storage_app.Services;

namespace storage_app.ViewModel
{
    internal class MainViewModel
    {
        private List<Product> _products;
        public List<Product> products
        {
            get { return _products; }
            set { _products = value; }
        }

        private List<Category> _categories;
        public List<Category> categories
        {
            get { return _categories; }
            set { _categories = value; }
        }

        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        public MainViewModel(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.GetProducts();
            this.GetCategories();
        }

        public void GetProducts()
        {
            var task = Task.Run(async () => await productService.GetProducts());
            this._products = task.Result;
        }
        public void GetCategories()
        {
            var task = Task.Run(async () => await categoryService.GetCategories());
            this._categories = task.Result;
        }
    }
}
