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
using Widger.Services;

namespace Widger.Components
{
    public partial class Modal_DeleteWidget : UserControl
    {
        private Widget Widget;

        public Modal_DeleteWidget(Widget widget)
        {
            InitializeComponent();
            Widget = widget;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance?.WidgetsContent.Children?.Remove(Widget);
            ModalService.Hide();
            ToastService.ShowToast("The widget was deleted", Brushes.Red);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ModalService.Hide();
        }
    }
}
