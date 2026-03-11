using Hardcodet.Wpf.TaskbarNotification;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Widger
{
    public partial class App : Application
    {
        private TaskbarIcon Tray => (TaskbarIcon)Resources["TrayIcon"];

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var tray = (TaskbarIcon)FindResource("TrayIcon");
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
