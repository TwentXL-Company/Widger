using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Widger.Components;

namespace Widger.Services
{
    public static class ToastService
    {
        public static Toast _toast;

        public static void Initialize(Toast toast)
        {
            _toast = toast;
        }

        public static void ShowToast(string message, Brush brush = null)
        {
            _toast.ShowToast(message, brush);
        }
    }
}
