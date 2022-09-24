using storage_app;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using storage_app.Services;
using storage_app.ViewModel;
using storage_app;

namespace storage_app
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new();

            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<MainWindow>();
            serviceProvider = services.BuildServiceProvider();
        }

        void OnStartup(object s, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            MainViewModel VM = new MainViewModel(serviceProvider.GetService<IProductService>(), serviceProvider.GetService<ICategoryService>());
            mainWindow.DataContext = VM;
            mainWindow.Show();

        }



    }
}
