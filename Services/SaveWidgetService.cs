using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Widger.Components;
using Widger.Models;

namespace Widger.Services
{
    public static class SaveWidgetService
    {
        private static readonly string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static readonly string dirPath = Path.Combine(localPath, "Widger");
        public static readonly string widgetsPath = Path.Combine(dirPath, "widgets.json");

        public static void Save()
        {
            try
            {
                List<WidgetModel> widgetList = new List<WidgetModel>();
                UIElementCollection widgetsBlock = MainWindow.Instance?.WidgetsContent.Children;
                foreach (var item in widgetsBlock)
                {
                    if (item is Widget widget)
                    {
                        bool isDesktop = widget.WidgetIsDesktop.Visibility == Visibility.Visible;

                        widgetList.Add(new WidgetModel
                        {
                            Heading = widget.Heading.Content.ToString(),
                            Content = widget.Content.Text,
                            Date = widget.WidgetDate.Content.ToString(),
                            BackgroundColor = widget.MainBorder.Background.ToString(),
                            TextColor = widget.Heading.Foreground.ToString(),
                            IsDesktop = isDesktop
                        });
                    }
                }

                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(widgetList, options);
                File.WriteAllText(widgetsPath, json);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Save data error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void Load()
        {
            try
            {
                MainWindow.Instance?.WidgetsContent.Children.Clear();

                string directory = Path.GetDirectoryName(widgetsPath)!;
                Directory.CreateDirectory(directory);

                if (!File.Exists(widgetsPath))
                    File.Create(widgetsPath).Dispose();

                string file = File.ReadAllText(widgetsPath);
                if (string.IsNullOrWhiteSpace(file) || file.Trim() == "[]")
                    return;

                var widgetsList = JsonSerializer.Deserialize<List<WidgetModel>>(file);
                if (widgetsList == null || widgetsList.Count == 0)
                    return;

                if (widgetsList != null && widgetsList.Count > 0)
                {
                    foreach (var item in widgetsList)
                    {
                        string heading = item.Heading;
                        string content = item.Content;
                        string date = item.Date;
                        string background = item.BackgroundColor;
                        string textColor = item.TextColor;
                        bool isDesktop = item.IsDesktop;

                        Widget widget = new Widget();
                        widget.Heading.Content = heading;
                        widget.Content.Text = content;
                        widget.WidgetDate.Content = date;

                        widget.MainBorder.Background = new BrushConverter().ConvertFrom(background) as Brush;
                        widget.Heading.Foreground = new BrushConverter().ConvertFrom(textColor) as Brush;
                        widget.Content.Foreground = new BrushConverter().ConvertFrom(textColor) as Brush;

                        MainWindow.Instance?.WidgetsContent.Children.Add(widget);

                        if(isDesktop)
                            widget.AddWidgetToDesktop(heading, content, date, background, textColor);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Load data error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
