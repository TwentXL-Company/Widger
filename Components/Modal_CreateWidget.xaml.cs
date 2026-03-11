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
    public partial class Modal_CreateWidget : UserControl
    {
        public Modal_CreateWidget()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(Content.Text) || string.IsNullOrWhiteSpace(Content.Text))
            {
                ContentError.Visibility = Visibility.Visible;
                return;
            }
            else
                ContentError.Visibility = Visibility.Collapsed;

            try
            {
                DateTime dateTime = DateTime.Now;
                Widget widget = new Widget();
                widget.Heading.Content = Heading.Text;
                widget.Content.Text = Content.Text;
                widget.WidgetDate.Content = dateTime.ToString("g");

                MainWindow.Instance?.WidgetsContent.Children.Add(widget);
                ModalService.Hide();
                ToastService.ShowToast("Widget was created", Brushes.Green);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Create widget error: " + ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ModalService.Hide();
        }
    }
}
