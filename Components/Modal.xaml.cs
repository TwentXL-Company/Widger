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

namespace Widger.Components
{
    public partial class Modal : UserControl
    {
        public static Modal? Instance;
        public Modal(UIElement element)
        {
            InitializeComponent();
            Instance = this;
            ModalContent.Children?.Add(element);
        }
    }
}
