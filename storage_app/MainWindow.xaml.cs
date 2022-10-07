using System.Windows;
using storage_app.Utils;

namespace storage_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Sell_Button_Click(object sender, RoutedEventArgs e)
        {
            SellPopupView.SellPopup.IsOpen = true;
        }
    }
}
