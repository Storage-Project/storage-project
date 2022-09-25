using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using storage_app.Services;
using storage_app.ViewModels;

namespace storage_app
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider serviceProvider;

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
            MainViewModel mainViewModel = BuildMainViewModel();
            WindowStartup(mainViewModel);
        }

        private MainViewModel BuildMainViewModel()
        {
            var _providerService = serviceProvider.GetService<IProductService>();
            var _categoryService = serviceProvider.GetService<ICategoryService>();

            if (_providerService == null) throw new Exception("Missing ProviderService");
            if (_categoryService == null) throw new Exception("Missing CategoryService");

            return new MainViewModel(_providerService, _categoryService);
        }

        private void WindowStartup(MainViewModel mainViewModel)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            if (mainWindow == null) throw new Exception("Missing MainWindow");
            mainWindow.DataContext = mainViewModel;
            mainWindow.Show();
        }

    }
}
