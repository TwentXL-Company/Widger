using Microsoft.Extensions.DependencyInjection;
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
using Widger.Services.Interfaces;

namespace Widger.Components
{
    public partial class Modal_CreateWidget : UserControl
    {
        private ISaveWidgetService _saveWidget;

        public Modal_CreateWidget(ISaveWidgetService saveWidget)
        {
            InitializeComponent();

            _saveWidget = saveWidget;
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
                Widget widget = App.Services.GetRequiredService<Widget>();

                widget.Heading.Content = Heading.Text;
                widget.Content.Text = Content.Text;
                widget.WidgetDate.Content = dateTime.ToString("g");

                MainWindow.Instance?.WidgetsContent.Children.Add(widget);
                _saveWidget.Save();

                ModalService.Hide();
                ToastService.ShowToast("Widget was created", Brushes.Green);
                MainWindow.Instance?.UpdateWidgetMessage();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Create widget error: " + ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
            => ModalService.Hide();
    }
}
