using System.Collections.Generic;
using System.Threading.Tasks;

using storage_app.Models;
using storage_app.Services;

namespace storage_app.ViewModels
{
    internal class StorageDataGridViewModel : ViewModelBase
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

        private readonly IProductService productService;
        public StorageDataGridViewModel(IProductService productService)
        {
            this.productService = productService;
            GetProducts();
        }

        public void GetProducts()
        {
            var task = Task.Run(async () => await productService.GetProducts());
            _products = task.Result;
        }
    }
}
