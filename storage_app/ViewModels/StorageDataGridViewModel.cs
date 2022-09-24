using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using storage_app.Models;
using storage_app.Services;
using storage_app.Utils.Objects;

namespace storage_app.ViewModels
{
    internal class StorageDataGridViewModel : ViewModelBase
    {
        private List<Product> _products = new();
        public List<Product> Products
        {
            get { return _products; }
            set 
            { 
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        private readonly IProductService productService;
        public StorageDataGridViewModel(IProductService productService)
        {
            this.productService = productService;
            GetProducts();
        }

        public void GetProducts(Filter? filter = null)
        {
            Trace.WriteLine(filter?.Description + filter?.Category.Description);
            var task = Task.Run(async () => await productService.GetProducts());
            _products = task.Result;
        }
    }
}
