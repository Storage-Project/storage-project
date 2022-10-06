using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void ComboBox_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
