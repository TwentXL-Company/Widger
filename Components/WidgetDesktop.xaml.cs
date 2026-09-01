using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Widger.Models;

namespace Widger.Components
{
    public partial class WidgetDesktop : Window
    {
        private readonly WidgetModel _model;

        public WidgetDesktop(WidgetModel model)
        {
            InitializeComponent();

            _model = model;

            this.Heading.Content = model.Heading;
            this.Content.Text = model.Content;
            this.WidgetDate.Content = model.Date;

            this.WidgetRoot.Background = new BrushConverter().ConvertFrom(model.BackgroundColor) as Brush;
            this.Heading.Foreground = new BrushConverter().ConvertFrom(model.TextColor) as Brush;
            this.Content.Foreground = new BrushConverter().ConvertFrom(model.TextColor) as Brush;

            LocationChanged += WidgetDesktop_LocationChanged;
            Left = model.CoordinateX;
            Top = model.CoordinateY;
        }

        private void WidgetDesktop_LocationChanged(object? sender, EventArgs e)
        {
            _model.CoordinateX = (int)Left;
            _model.CoordinateY = (int)Top;
        }

        private void WidgetDrag_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void MoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (MoreButton.ContextMenu != null)
            {
                MoreButton.ContextMenu.PlacementTarget = MoreButton;
                MoreButton.ContextMenu.IsOpen = true;
            }
        }

        private void DeleteFromDesktop_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
