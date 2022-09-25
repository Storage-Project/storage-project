using System.Collections.Generic;
using System.Threading.Tasks;

using storage_app.Models;
using storage_app.Services;
using storage_app.Utils.Objects;
using storage_app.ViewModels.Actions;

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

        private SearchActionViewModel _searchActionViewModel;
        public SearchActionViewModel SearchActionViewModel
        {
            get { return _searchActionViewModel; }
            set
            {
                _searchActionViewModel = value;
                OnPropertyChanged(nameof(SearchActionViewModel));
            }
        }

        public MainViewModel(IProductService productService, ICategoryService categoryService)
        {

            _storageDataGridViewModel =
                new StorageDataGridViewModel(productService);

            _searchActionViewModel =
                new SearchActionViewModel();
            BuildSearchAction();

            _filterViewModel =
                new FilterViewModel(categoryService, SearchActionViewModel);
        }

        public void BuildSearchAction()
        {
            SearchActionViewModel.FilteredSerchCommand =
                new Searcher(
                    f => 
                    { 
                        StorageDataGridViewModel.GetProducts(f as Filter); 
                    });
        }


    }
}
