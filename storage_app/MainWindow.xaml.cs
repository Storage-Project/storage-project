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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using storage_app.Models;
using storage_app.ViewModels;

using storage_app.Models;
using storage_app.Services;
using storage_app.ViewModel;

namespace storage_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public MainWindowViewModel MainWindowViewModel;

        public MainWindow()
        {
            //MainWindowViewModel = new MainWindowViewModel();

            InitializeComponent();
        }
    }
}
