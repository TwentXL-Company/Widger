using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Widger.Components;
using Widger.Services;

namespace Widger
{
    public partial class MainWindow : Window
    {
        public static MainWindow? Instance;
        public MainWindow()
        {
            InitializeComponent();
            ToastService.Initialize(MyToast);
            Instance = this;
            SaveWidgetService.Load();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
            ShowInTaskbar = false;
        }

        protected override void OnClosed(EventArgs e)
        {
            SaveWidgetService.Save();
            base.OnClosed(e);
        }

        private void Titlebar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        private void BurgerMenu_Click(object sender, RoutedEventArgs e)
        {
            if (BurgerMenu.ContextMenu != null)
            {
                BurgerMenu.ContextMenu.PlacementTarget = BurgerMenu;
                BurgerMenu.ContextMenu.Placement = PlacementMode.Left;
                BurgerMenu.ContextMenu.IsOpen = true;
            }
        }

        private void AboutApp_Click(object sender, RoutedEventArgs e)
        {
            Modal_AboutApp aboutApp = new Modal_AboutApp();
            ModalService.Show(aboutApp);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Modal_Settings settings = new Modal_Settings();
            ModalService.Show(settings);
        }

        private void CreateWidget_Click(object sender, RoutedEventArgs e)
        {
            Modal_CreateWidget createWidget = new Modal_CreateWidget();
            ModalService.Show(createWidget);
        }
    }
}