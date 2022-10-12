using storage_app.ViewModels;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using storage_app.Utils;

namespace storage_app.Views
{
    /// <summary>
    /// Interação lógica para SellPopupView.xam
    /// </summary>
    public partial class SellPopupView : UserControl
    {
        public SellPopupView()
        {
            InitializeComponent();
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            SellPopup.IsOpen = false;
        }

        private void Sell_Button_Click(object sender, RoutedEventArgs e)
        {
            SellViewModel viewModel = (SellViewModel)DataContext;

            if (!viewModel.StartSelling.IsFunc) return;

            dynamic result = viewModel
                                .StartSelling
                                .ExecuteFunc(null);

            if(result is bool && result == false)
            {
                ShowMessage.ErrorMessage("This product cannot be sold in this quantity");
                viewModel.QuantityToSell = 0;
            }
            else
            {
                ShowMessage.DefaultMessage("Product sold!");
                SellPopup.IsOpen = false;
            }
            
        }

        private void ComboBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (sender is not ComboBox comboBox ||
                DataContext is not SellViewModel viewModel) 
                return;
            comboBox.IsDropDownOpen = true;
            viewModel.FilterProductsByDescription(comboBox.Text.ToLower().Trim());
        }

        private void ComboBox_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is not ComboBox comboBox) return;
            comboBox.IsDropDownOpen = true;
        }

        private void ComboBox_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is not ComboBox comboBox) return;
            comboBox.IsDropDownOpen = false;
        }
    }
}
