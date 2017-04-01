using System.Windows;

namespace AppHostCefSharp
{
    public class Geometry
    {
        public double? Top { get; set; }
        public double? Left { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public WindowState State { get; set; } = WindowState.Normal;
    }
}
