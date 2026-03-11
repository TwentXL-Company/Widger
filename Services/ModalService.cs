using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Widger.Components;

namespace Widger.Services
{
    public static class ModalService
    {
        public static void Show(UIElement content)
        {
            Modal modal = new Modal(content);
            MainWindow.Instance?.Modal_Content.Children?.Add(modal);
        }

        public static void Hide()
        {
            Modal.Instance?.ModalContent.Children?.Clear();
            MainWindow.Instance?.Modal_Content.Children?.Clear();
        }
    }
}
