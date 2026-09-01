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
using Widger.Models;
using Widger.Services;
using Widger.Services.Interfaces;

namespace Widger.Components
{
    public partial class Widget : UserControl
    {
        public WidgetDesktop? DesktopWindow { get; set; }
        private ISaveWidgetService _saveWidget;

        public Widget(ISaveWidgetService saveWidget)
        {
            InitializeComponent();

            _saveWidget = saveWidget;

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
            WidgetModel model = new WidgetModel();

            model.Heading = this.Heading.Content.ToString();
            model.Content = this.Content.Text.ToString();
            model.Date = this.WidgetDate.Content.ToString();
            model.CoordinateX = 500;
            model.CoordinateY = 200;
            model.BackgroundColor = this.MainBorder.Background.ToString();
            model.TextColor = this.Heading.Foreground.ToString();

            AddWidgetToDesktop(model);
        }

        private void DeleteWidget_Click(object sedner, RoutedEventArgs e)
        {
            var createDialog = App.Services.GetRequiredService<Func<Widget, Modal_DeleteWidget>>();
            var deleteDialog = createDialog(this);
            ModalService.Show(deleteDialog);
        }

        private void IsEdit(bool isEdit)
        {
            if (isEdit)
            {
                MoreButton.Visibility = Heading.Visibility = ScrollContent.Visibility = Content.Visibility = Visibility.Collapsed;
                SaveChangesButton.Visibility = HeadingEdited.Visibility = ScrollContentEdited.Visibility = ContentEdited.Visibility = Visibility.Visible;

                HeadingEdited.Text = Heading.Content.ToString();
                ContentEdited.Text = Content.Text;

                _saveWidget.Save();
            }
            else
            {
                MoreButton.Visibility = Heading.Visibility = ScrollContent.Visibility = Content.Visibility = Visibility.Visible;
                SaveChangesButton.Visibility = HeadingEdited.Visibility = ScrollContentEdited.Visibility = ContentEdited.Visibility = Visibility.Collapsed;
            }
        }

        public void AddWidgetToDesktop(WidgetModel model)
        {
            if(WidgetIsDesktop.Visibility == Visibility.Visible)
            {
                ToastService.ShowToast("This widget is already use", Brushes.Red);
                return;
            }
            WidgetDesktop widgetDesktop = new WidgetDesktop(model);
            
            widgetDesktop.Closed += (_, __) =>
                WidgetIsDesktop.Visibility = Visibility.Collapsed;

            widgetDesktop.Show();
            this.DesktopWindow = widgetDesktop;
            WidgetIsDesktop.Visibility = Visibility.Visible;

            _saveWidget.Save();
        }
    }
}
