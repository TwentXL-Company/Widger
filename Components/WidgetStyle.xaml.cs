using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;
using Widger.Services;

namespace Widger.Components
{
    public partial class WidgetStyle : Window
    {
        private Widget Widget;
        public WidgetStyle(Widget widget)
        {
            InitializeComponent();
            Widget = widget;
            BgHexTextBox.Text = widget.MainBorder.Background.ToString();
            TextHexTextBox.Text = widget.Heading.Foreground.ToString();

            if (TryParseHex(BgHexTextBox.Text, out var bg))
                BgCanvas.SelectedColor = bg;
            if (TryParseHex(TextHexTextBox.Text, out var fg))
                TextCanvas.SelectedColor = fg;

            BgHexTextBox.LostFocus += (_, __) =>
            {
                if (TryParseHex(BgHexTextBox.Text, out var c))
                    BgCanvas.SelectedColor = c;
            };
            TextHexTextBox.LostFocus += (_, __) =>
            {
                if (TryParseHex(TextHexTextBox.Text, out var c))
                    TextCanvas.SelectedColor = c;
            };
        }

        private void WindowDrag_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Widget.MainBorder.Background = new SolidColorBrush(BgCanvas.SelectedColor ?? Colors.Transparent);
                Widget.Heading.Foreground = new SolidColorBrush(TextCanvas.SelectedColor ?? Colors.Transparent);
                Widget.Content.Foreground = new SolidColorBrush(TextCanvas.SelectedColor ?? Colors.Transparent);
                this.Close();
                ToastService.ShowToast("Saved", Brushes.Green);
            }
            catch
            {
                ToastService.ShowToast("Widget style error. Try again");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Canvas_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue is not Color c) return;

            if (ReferenceEquals(sender, BgCanvas))
            {
                BgHexTextBox.Text = ToHex(c);
            }
            else if (ReferenceEquals(sender, TextCanvas))
            {
                TextHexTextBox.Text = ToHex(c);
            }
        }

        private void BgOk_Click(object sender, RoutedEventArgs e) => BgPickButton.IsChecked = false;
        private void TextOk_Click(object sender, RoutedEventArgs e) => TextPickButton.IsChecked = false;

        private static string ToHex(Color c) => $"#{c.A:X2}{c.R:X2}{c.G:X2}{c.B:X2}";

        private static bool TryParseHex(string? text, out Color color)
        {
            color = default;
            if (string.IsNullOrWhiteSpace(text))
                return false;

            text = text.Trim();
            if (text.StartsWith("#"))
                text = text[1..];

            if (text.Length == 6)
                text = "FF" + text;

            if (text.Length != 8)
                return false;

            if (!uint.TryParse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var argb))
                return false;

            color = Color.FromArgb(
                (byte)((argb >> 24) & 0xFF),
                (byte)((argb >> 16) & 0xFF),
                (byte)((argb >> 8) & 0xFF),
                (byte)(argb & 0xFF));

            return true;
        }
    }
}
