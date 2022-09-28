using storage_app.Models;
using storage_app.Services;
using storage_app.Utils;
using storage_app.Utils.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace storage_app.ViewModels
{
    internal class ItemDetailViewModel : ViewModelBase
    {
        private EditingConditions _editing = new();
        public EditingConditions Editing
        {
            get { return _editing; }
            set
            {
                _editing = value;
                OnPropertyChanged(nameof(Editing));
            }
        }

        private Product _product = new();
        private Product _originalProduct = new();
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
        private readonly IProductService productService;

        public ItemDetailViewModel(
            ICategoryService categoryService,
            IProductService productService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            GetCategories();

            _startEditionCommand = new((_) => StartEdition());
            _startInsertionCommand = new((_) => StartInsertion());
            _addCommand = new((_) => EndInsertWithAdd());
            _saveCommand = new((_) => EndEditWithSave());
            _cancelCommand = new((_) => EndWithCancel());
        }

        public void GetCategories()
        {
            var task = Task.Run(async () => await categoryService.GetCategories());
            Categories = task.Result;
        }

        public void UpdateSelectedProduct(Product? product)
        {
            if (product != null)
            {
                SelectedCategory = product.Category;
                Product = product;
                _originalProduct = new() {
                    Id = product.Id,
                    Description = product.Description,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    Category = product.Category,
                    Create_at = product.Create_at,
                };
            }
        }

        private StartInsertion? _startInsertionCommand;
        public StartInsertion StartInsertionCommand
        {
            get
            {
                if (_startInsertionCommand == null)
                    _startInsertionCommand = new StartInsertion(_ => { });
                return _startInsertionCommand;
            }
            set
            {
                _startInsertionCommand = value;
            }
        }
        private void StartInsertion()
        {
            Product = new();
            Editing.IsEditing = true;
            Editing.IsAdding = true;
            OnPropertyChanged(nameof(Editing));
        }
        private StartEdition? _startEditionCommand;
        public StartEdition StartEditionCommand
        {
            get
            {
                if (_startEditionCommand == null)
                    _startEditionCommand = new StartEdition(_ => { });
                return _startEditionCommand;
            }
            set
            {
                _startEditionCommand = value;
            }
        }
        private void StartEdition()
        {
            if (Product.Id == 0) return; 
            Editing.IsEditing = true;
            OnPropertyChanged(nameof(Editing));
        }
        private AddCommand? _addCommand;
        public AddCommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                    _addCommand = new AddCommand(_ => { });
                return _addCommand;
            }
            set
            {
                _addCommand = value;
            }
        }
        private void EndInsertWithAdd()
        {
            // DO THING IN BACKEND
            _originalProduct = Product;
            EndEdition();
        }
        private SaveCommand? _saveCommand;
        public SaveCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new SaveCommand(_ => { });
                return _saveCommand;
            }
            set
            {
                _saveCommand = value;
            }
        }

        private void EndEditWithSave()
        {
            Product.Category = SelectedCategory;
            Trace.WriteLine(Product.Category.Description);
            Trace.WriteLine(Product.Description);

            _originalProduct = Product;
            EndEdition();
        }
        private CancelCommand? _cancelCommand;
        public CancelCommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                    _cancelCommand = new CancelCommand(_ => { });
                return _cancelCommand;
            }
            set
            {
                _cancelCommand = value;
            }
        }

        private void EndWithCancel()
        {
            SelectedCategory = _originalProduct.Category;
            Product = _originalProduct;
            EndEdition();
        }

        private void EndEdition()
        {
            Editing.IsEditing = false;
            Editing.IsAdding = false;
            OnPropertyChanged(nameof(Editing));
        }
    }

    internal class StartEdition : CommandExecutor
    {
        public StartEdition(Action<object?> action) : base(action) { }
    }

    internal class StartInsertion : CommandExecutor
    {
        public StartInsertion(Action<object?> action) : base(action) { }
    }

    internal class AddCommand : CommandExecutor
    {
        public AddCommand(Action<object?> action) : base(action) { }
    }

    internal class SaveCommand : CommandExecutor
    {
        public SaveCommand(Action<object?> action) : base(action) { }
    }

    internal class CancelCommand : CommandExecutor
    {
        public CancelCommand(Action<object?> action) : base(action) { }
    }
}
