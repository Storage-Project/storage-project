using storage_app.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storage_app.ViewModels.Actions
{
    internal class SelectedProductActionViewModel
    {
        private SelectedProduct? _selectedProductCommand;
        public SelectedProduct SelectedProductCommand
        {
            get
            {
                if (_selectedProductCommand == null)
                    _selectedProductCommand = new SelectedProduct(_ => { });
                return _selectedProductCommand;
            }
            set
            {
                _selectedProductCommand = value;
            }
        }
    }   

    internal class SelectedProduct : CommandExecutor
    {
        public SelectedProduct(Action<object?> action) : base(action) { }
    }
}
