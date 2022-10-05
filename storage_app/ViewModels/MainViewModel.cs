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

        private ItemDetailViewModel _itemDetailViewModel;
        public ItemDetailViewModel ItemDetailViewModel
        {
            get { return _itemDetailViewModel; }
            set
            {
                _itemDetailViewModel = value;
                OnPropertyChanged(nameof(ItemDetailViewModel));
            }
        }

        private SellViewModel _sellViewModel;
        public SellViewModel SellViewModel
        {
            get { return _sellViewModel; }
            set
            {
                _sellViewModel = value;
                OnPropertyChanged(nameof(_sellViewModel));
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

        public MainViewModel(IProductService productService, ICategoryService categoryService)
        {
            _sellViewModel =
                new SellViewModel(productService);

            _itemDetailViewModel =
                new ItemDetailViewModel(categoryService, productService);

            _selectedProductActionViewModel =
                new SelectedProductActionViewModel();
            BuildSelectedProductAction();
            
            _storageDataGridViewModel =
                new StorageDataGridViewModel(productService, SelectedProductActionViewModel);

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
                        if (_storageDataGridViewModel != null)
                            StorageDataGridViewModel.GetProducts(f as Filter); 
                    });
        }

        public void BuildSelectedProductAction()
        {
            SelectedProductActionViewModel.SelectedProductCommand =
                new SelectedProduct(
                    p =>
                    {
                        if (_itemDetailViewModel != null)
                            ItemDetailViewModel.UpdateSelectedProduct(p as Product);
                    });
        }
    }
}
