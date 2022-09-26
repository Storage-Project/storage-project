using storage_app.ViewModels;
using System.Windows.Controls;

namespace storage_app.Views
{
    /// <summary>
    /// Interação lógica para StorageDataGridView.xam
    /// </summary>
    public partial class StorageDataGridView : UserControl
    {
        public StorageDataGridView()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            StorageDataGridViewModel viewModel = (StorageDataGridViewModel)DataContext;
            viewModel.SelectedProductActionViewModel
                .SelectedProductCommand
                .Execute(viewModel.SelectedProduct);
        }
    }
}
