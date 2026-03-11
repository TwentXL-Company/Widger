using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Widger.Components
{
    public partial class WidgetDesktop : Window
    {
        public WidgetDesktop(string? heading, string content, string? date, string background, string textColor)
        {
            InitializeComponent();
            this.Heading.Content = heading;
            this.Content.Text = content;
            this.WidgetDate.Content = date;

            this.WidgetRoot.Background = new BrushConverter().ConvertFrom(background) as Brush;
            this.Heading.Foreground = new BrushConverter().ConvertFrom(textColor) as Brush;
            this.Content.Foreground = new BrushConverter().ConvertFrom(textColor) as Brush;
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
