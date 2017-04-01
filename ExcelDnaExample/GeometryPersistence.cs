using System;
using System.Windows;
using AppHostCefSharp;

namespace ExcelDnaExample
{
    public class GeometryPersistence : IPersistGeometry
    {
        private readonly string id;
        private readonly double? defaultHeight;
        private readonly double? defaultWidth;

        public GeometryPersistence(string id)
        {
            this.id = id;
            defaultWidth = 800;
            defaultHeight = 600;
        }

        public GeometryPersistence(string id, double defaultWidth, double defaultHeight)
        {
            this.id = id;
            this.defaultWidth = defaultWidth;
            this.defaultHeight = defaultHeight;
        }

        public void Persist(Window window)
        {
            Update(new Geometry
            {
                Top = window.Top,
                Left = window.Left,
                Width = window.Width,
                Height = window.Height,
                State = window.WindowState
            });
        }

        public void Restore(Window window)
        {
            var geometry = Get();

            // Size to fit.
            if (geometry.Height > SystemParameters.VirtualScreenHeight)
                geometry.Height = SystemParameters.VirtualScreenHeight;
            if (geometry.Width > SystemParameters.VirtualScreenWidth)
                geometry.Width = SystemParameters.VirtualScreenWidth;

            // Move into view.
            if (geometry.Top + geometry.Height / 2 > SystemParameters.VirtualScreenHeight)
                geometry.Top = SystemParameters.VirtualScreenHeight - geometry.Height;
            if (geometry.Top < 0) geometry.Top = 0;
            if (geometry.Left + geometry.Width / 2 > SystemParameters.VirtualScreenWidth)
                geometry.Left = SystemParameters.VirtualScreenWidth - geometry.Width;
            if (geometry.Left < 0) geometry.Left = 0;

            // Apply geometry to window.
            if (geometry.Top != null) window.Top = geometry.Top.Value;
            if (geometry.Left != null) window.Left = geometry.Left.Value;
            if (geometry.Width != null) window.Width = geometry.Width.Value;
            if (geometry.Height != null) window.Height = geometry.Height.Value;
            window.WindowState = geometry.State;
            window.WindowStartupLocation = geometry.Top != null
                ? WindowStartupLocation.Manual // Manually position or center.
                : WindowStartupLocation.CenterScreen;
        }

        private void Update(Geometry geometry)
        {
            var x = geometry.Left;
            var y = geometry.Top;
            var w = geometry.Width;
            var h = geometry.Height;

            string state;
            switch (geometry.State)
            {
                case WindowState.Normal:
                case WindowState.Minimized:
                    state = "Normal";
                    break;
                case WindowState.Maximized:
                    state = "Maximized";
                    break;
                default:
                    throw new Exception();
            }

            Settings.Default[$"Geometry_{id}"] = $"{x}|{y}|{w}|{h}|{state}";
            Settings.Default.Save();
        }

        private Geometry Get()
        {
            var key = $"Geometry_{id}";
            if (!Settings.Default.ContainsKey(key) || string.IsNullOrEmpty(Settings.Default[key]))
            {
                return new Geometry
                {
                    Width = defaultWidth,
                    Height = defaultHeight
                };
            }

            var element = Settings.Default[key].Split('|');

            WindowState state;
            switch (element[4])
            {
                case "Normal":
                case "Minimised":
                    state = WindowState.Normal;
                    break;
                case "Maximized":
                    state = WindowState.Maximized;
                    break;
                default:
                    throw new Exception();
            }

            return new Geometry
            {
                Left = double.Parse(element[0]),
                Top = double.Parse(element[1]),
                Width = double.Parse(element[2]),
                Height = double.Parse(element[3]),
                State = state
            };
        }
    }
}
