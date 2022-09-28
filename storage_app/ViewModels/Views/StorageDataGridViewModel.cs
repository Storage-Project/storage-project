using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using storage_app.Models;
using storage_app.Services;
using storage_app.Utils.Objects;
using storage_app.ViewModels.Actions;

namespace storage_app.ViewModels
{
    internal class StorageDataGridViewModel : ViewModelBase
    {
        private SelectedProductActionViewModel _selectedProductActionViewModel;
        public SelectedProductActionViewModel SelectedProductActionViewModel
        {
            get { return _selectedProductActionViewModel; }
            set
            {
                _selectedProductActionViewModel = value;
                OnPropertyChanged(nameof(SelectedProductActionViewModel));
            }
        }

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
        public StorageDataGridViewModel(
            IProductService productService,
            SelectedProductActionViewModel selectedProductActionViewModel)
        {
            _selectedProductActionViewModel = selectedProductActionViewModel;
            this.productService = productService;
            GetProducts();
        }

        public void GetProducts(Filter? filter = null)
        {
            if (filter == null)
            {
                var task = Task.Run(async () => await productService.GetProducts());
                Products = task.Result;
            }
            else
            {
                var task = Task.Run(
                    async () =>
                    await productService
                    .GetProductsFiltered(
                        description: filter.Description,
                        category: filter.Category.Description
                        )
                    );
                Products = task.Result;
            }

            if (Products.Count > 0) SelectedProduct = Products[0];
            else SelectedProduct = new Product();
            
            SelectedProductActionViewModel.SelectedProductCommand.Execute(SelectedProduct);
        }
    }
}
