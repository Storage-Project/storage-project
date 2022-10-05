using storage_app.Models;
using storage_app.Services;
using storage_app.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace storage_app.ViewModels
{
    internal class SellViewModel : ViewModelBase
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

        private Product _selectedProduct = new();
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        private readonly IProductService productService;
        public SellViewModel(IProductService productService)
        {
            _startSelling = new((_) => SellProduct());
            this.productService = productService;
            GetProducts();
        }

        private void GetProducts()
        {
            var task = Task.Run(async () => await productService.GetProducts());
            Products = task.Result;
        }

        private void SellProduct()
        {
            var task = Task.Run(
                async () =>
                await productService.SellProduct(
                    SelectedProduct.Id,
                    0,
                    SelectedProduct)
                );
        }

        private SellProduct? _startSelling;
        public SellProduct StartSelling
        {
            get
            {
                if (_startSelling == null)
                    _startSelling = new SellProduct(_ => { });
                return _startSelling;
            }
            set
            {
                _startSelling = value;
            }
        }
    }

    internal class SellProduct : CommandExecutor
    {
        public SellProduct(Action<object?> action) : base(action) { }
    }
}
