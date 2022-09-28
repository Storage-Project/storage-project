using storage_app.Models;
using storage_app.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace storage_app.Views
{
    /// <summary>
    /// Interação lógica para ItemDetailView.xam
    /// </summary>
    public partial class ItemDetailView : UserControl
    {
        public ItemDetailView()
        {
            InitializeComponent();
        }

        private void StackPanel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                ItemDetailViewModel viewModel = (ItemDetailViewModel)DataContext;
                viewModel
                    .StartEditionCommand
                    .Execute(null);
            }
        }
    }
}
