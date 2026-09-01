using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widger.Models
{
    public class WidgetModel
    {
        public string? Heading { get; set; }
        public string? Content { get; set; }
        public string? Date { get; set; }
        public double CoordinateX { get; set; } = 500;
        public double CoordinateY { get; set; } = 200;
        public string? BackgroundColor { get; set; }
        public string? TextColor { get; set; }
        public bool IsDesktop { get; set; }
    }
}
