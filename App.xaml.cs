using CadwiseAutomaticTellerMachine.Business.Interfaces;
using CadwiseAutomaticTellerMachine.Business.Services;
using CadwiseAutomaticTellerMachine.Infrastructure.Data;
using CadwiseAutomaticTellerMachine.Infrastructure.Repositories;
using CadwiseAutomaticTellerMachine.MVVM.Interfaces;
using CadwiseAutomaticTellerMachine.MVVM.Navigation;
using CadwiseAutomaticTellerMachine.MVVM.ViewModels;
using CadwiseAutomaticTellerMachine.MVVM.Views;
using CadwiseAutomaticTellerMachine.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CadwiseAutomaticTellerMachine
{
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
            services.AddSingleton<ServiceViewModel>();
            services.AddSingleton<BalanceViewModel>();
            services.AddSingleton<WithdrawMoneyViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<Func<Type, ViewModelBase>>(_serviceProvider => viewModelType =>
            {
                return (ViewModelBase)_serviceProvider.GetRequiredService(viewModelType);
            });
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql("Server=localhost;Port=5432;Database=atm;Username=admin;Password=admin");
            });
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
