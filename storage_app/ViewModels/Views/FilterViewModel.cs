using storage_app.Models;
using storage_app.Services;
using storage_app.Utils.Objects;
using storage_app.ViewModels.Actions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace storage_app.ViewModels
{
    internal class FilterViewModel : ViewModelBase
    {
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
        private Filter _filter = new();
        public Filter Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                OnPropertyChanged(nameof(Filter));
            }
        }

        private Category _selectedCategory = new();
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                _filter.Category = _selectedCategory;
                OnPropertyChanged(nameof(SelectedCategory));
                OnPropertyChanged(nameof(Filter));
            }
        }

        private string _filterDescription = string.Empty;
        public string FilterDescription
        {
            get { return _filterDescription; }
            set
            {
                _filterDescription = value;
                _filter.Id = Convert.ToInt32(_filterDescription);
                _filter.Description = _filterDescription;
                OnPropertyChanged(nameof(FilterDescription));
                OnPropertyChanged(nameof(Filter));
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

        public FilterViewModel(
            ICategoryService categoryService,
            SearchActionViewModel searchActionViewModel)
        {
            _searchActionViewModel = searchActionViewModel;
            this.categoryService = categoryService;
            GetCategories();
        }

        public void GetCategories()
        {
            var task = Task.Run(async () => await categoryService.GetCategories());
            _categories = task.Result;
            _categories.Insert(0, new Category());
            Categories = _categories;
        }
    }
}
