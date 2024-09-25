using CadwiseAutomaticTellerMachine.MVVM.Navigation;
using CadwiseAutomaticTellerMachine.MVVM.ViewModels;
using CadwiseAutomaticTellerMachine.MVVM.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CadwiseAutomaticTellerMachine
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWondow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWondow.Show();
            base.OnStartup(e);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>(_serviceProvider => new MainWindow
            {
                DataContext = _serviceProvider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<HelpViewModel>();
            services.AddSingleton<AboutViewModel>();
            services.AddSingleton<AuthViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<Func<Type, ViewModelBase>>(_serviceProvider => viewModelType =>
            {
                return (ViewModelBase)_serviceProvider.GetRequiredService(viewModelType);
            });

        }
    }

}
