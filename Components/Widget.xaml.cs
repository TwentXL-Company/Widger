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
    public partial class Widget : UserControl
    {
        public Widget()
        {
            InitializeComponent();
            IsEdit(false);
        }

        private void MoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (MoreButton.ContextMenu != null)
            {
                MoreButton.ContextMenu.PlacementTarget = MoreButton;
                MoreButton.ContextMenu.IsOpen = true;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            IsEdit(true);
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(ContentEdited.Text) || string.IsNullOrWhiteSpace(ContentEdited.Text))
            {
                ToastService.ShowToast("Field 'content' is required", Brushes.Red);
                return;
            }

            DateTime dateTime = DateTime.Now;
            Heading.Content = HeadingEdited.Text;
            Content.Text = ContentEdited.Text;
            WidgetDate.Content = dateTime.ToString("g");
            ToastService.ShowToast("Saved", Brushes.Green);

            IsEdit(false);
        }

        private void Style_Click(object sender, RoutedEventArgs e)
        {
            WidgetStyle widgetStyle = new WidgetStyle(this);
            widgetStyle.Show();
        }

        private void AddToDesktop_Click(object sender, RoutedEventArgs e)
        {
            AddWidgetToDesktop(this.Heading.Content.ToString(), this.Content.Text.ToString(), this.WidgetDate.Content.ToString(), this.MainBorder.Background.ToString(), this.Heading.Foreground.ToString());
        }

        private void DeleteWidget_Click(object sedner, RoutedEventArgs e)
        {
            Modal_DeleteWidget deleteWidget = new Modal_DeleteWidget(this);
            ModalService.Show(deleteWidget);
        }

        private void IsEdit(bool isEdit)
        {
            if (isEdit)
            {
                MoreButton.Visibility = Visibility.Collapsed;
                Heading.Visibility = Visibility.Collapsed;
                ScrollContent.Visibility = Visibility.Collapsed;
                Content.Visibility = Visibility.Collapsed;

                SaveChangesButton.Visibility = Visibility.Visible;
                HeadingEdited.Visibility = Visibility.Visible;
                ScrollContentEdited.Visibility = Visibility.Visible;
                ContentEdited.Visibility = Visibility.Visible;

                HeadingEdited.Text = Heading.Content.ToString();
                ContentEdited.Text = Content.Text;
            }
            else
            {
                MoreButton.Visibility = Visibility.Visible;
                Heading.Visibility = Visibility.Visible;
                ScrollContent.Visibility = Visibility.Visible;
                Content.Visibility = Visibility.Visible;

                SaveChangesButton.Visibility = Visibility.Collapsed;
                HeadingEdited.Visibility = Visibility.Collapsed;
                ScrollContentEdited.Visibility = Visibility.Collapsed;
                ContentEdited.Visibility = Visibility.Collapsed;
            }
        }

        public void AddWidgetToDesktop(string? heading, string content, string? date, string background, string textColor)
        {
            if(WidgetIsDesktop.Visibility == Visibility.Visible)
            {
                ToastService.ShowToast("This widget is already use", Brushes.Red);
                return;
            }
            WidgetDesktop widgetDesktop = new WidgetDesktop(heading, content, date, background, textColor);

            widgetDesktop.Closed += (_, __) =>
                WidgetIsDesktop.Visibility = Visibility.Collapsed;

            widgetDesktop.Show();
            WidgetIsDesktop.Visibility = Visibility.Visible;
            ToastService.ShowToast("Widget was added to desktop", Brushes.Green);
        }
    }
}
