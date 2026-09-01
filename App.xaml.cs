using Hardcodet.Wpf.TaskbarNotification;
using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Widger.Services.Interfaces;
using Widger.Services;
using Widger.Components;

namespace Widger
{
    public partial class App : Application
    {
        private TaskbarIcon Tray => (TaskbarIcon)Resources["TrayIcon"];

        private ServiceProvider _serviceProvider;
        public static IServiceProvider Services = ((App)Current)._serviceProvider;

        public App()
        {
            ServiceCollection service = new ServiceCollection();
            RegisterServices(service);

            _serviceProvider = service.BuildServiceProvider();
        }

        private void RegisterServices(ServiceCollection service)
        {
            service.AddSingleton<ISaveWidgetService, SaveWidgetService>();

            service.AddTransient<MainWindow>();
            service.AddTransient<Modal_CreateWidget>();
            service.AddTransient<Widget>();
            service.AddTransient<Func<Widget, Modal_DeleteWidget>>(sp => widget =>
                new Modal_DeleteWidget(widget, sp.GetRequiredService<ISaveWidgetService>())
            );
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var tray = (TaskbarIcon)FindResource("TrayIcon");

            if (_serviceProvider != null)
            {
                Window window = _serviceProvider.GetRequiredService<MainWindow>();
                window.Show();
            }
            else
                throw new Exception("Set provider error");
        }

        private void Tray_Open(object sender, RoutedEventArgs e)
        {
            var w = Current.MainWindow;
            if (w == null) return;
            w.ShowInTaskbar = true;
            w.Show();
            w.WindowState = WindowState.Normal;
            w.Activate();
        }

        private void Tray_Exit(object sender, RoutedEventArgs e)
        {
            Tray.Dispose();
            Shutdown();
        }
    }
}
