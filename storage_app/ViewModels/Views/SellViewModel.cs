using storage_app.Models;
using storage_app.Services;
using storage_app.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace storage_app.ViewModels
{
    internal class SellViewModel : ViewModelBase
    {
        private List<Product> OriginalProducts = new();
        private List<Product> _filteredProducts = new();
        public List<Product> FilteredProducts
        {
            get { return _filteredProducts; }
            set
            {
                _filteredProducts = value;
                OnPropertyChanged(nameof(FilteredProducts));
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
                Trace.WriteLine(SelectedProduct);
            }
        }

        private int _quantityToSell = 0;
        public int QuantityToSell
        {
            get { return _quantityToSell; }
            set
            {
                _quantityToSell = value;
                OnPropertyChanged(nameof(QuantityToSell));
                CanSell = _quantityToSell > 0;
            }
        }
        private bool _canSell = false;
        public bool CanSell
        {
            get { return _canSell; }
            set
            {
                _canSell = value;
                OnPropertyChanged(nameof(CanSell));
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
            OriginalProducts = task.Result;
            FilterProductsByDescription(string.Empty);
        }

        public void FilterProductsByDescription(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                FilteredProducts = OriginalProducts;
                return;
            }

            FilteredProducts =
                OriginalProducts
                .FindAll(
                    p => 
                    p.Description
                    .ToLower().Trim()
                    .Contains(text, StringComparison.CurrentCultureIgnoreCase)
                    );
        }

        private bool SellProduct()
        {
            var task = Task.Run(
                async () =>
                await productService.SellProduct(
                    SelectedProduct.Id,
                    QuantityToSell,
                    SelectedProduct)
                );

            if (task.Result == null) return false;
            else return true;
        }

        private SellProduct? _startSelling;
        public SellProduct StartSelling
        {
            get
            {
                if (_startSelling == null)
                    _startSelling = new SellProduct((_) => { return false; });
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
        public SellProduct(Func<object?, dynamic> func) : base(func) { }
    }
}
