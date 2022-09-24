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

        private FilterViewModel _filterViewModel;
        public FilterViewModel FilterViewModel
        {
            get { return _filterViewModel; }
            set
            {
                _filterViewModel = value;
                OnPropertyChanged(nameof(FilterViewModel));
            }
        }

        public MainViewModel(IProductService productService, ICategoryService categoryService)
        {
            _storageDataGridViewModel =
                new StorageDataGridViewModel(productService);

            _filterViewModel =
                new FilterViewModel(categoryService);
        }


    }
}
