using System.IO;
using System.Windows;
using System.Windows.Forms.Integration;
using ExcelDna.Integration;
using SteveRGB.AppHostCefSharp;

namespace SteveRGB.ExcelDnaExample
{
    public class AddIn : IExcelAddIn
    {
        public AddIn()
        {
            // Prepare AppData
            if (!Directory.Exists(Settings.AppDataPath))
            {
                Directory.CreateDirectory(Settings.AppDataPath);
            }
            else
            {
                // Delete the browser cache on startup.
                var cachePath = Path.Combine(Settings.AppDataPath, "Cache", "Cache");
                if (!Directory.Exists(cachePath)) return;
                foreach (var filename in Directory.GetFiles(cachePath)) File.Delete(filename);
                Directory.Delete(cachePath);
            }
        }

        void IExcelAddIn.AutoOpen()
        { }

        void IExcelAddIn.AutoClose()
        { }

        public static void ShowExampleForm()
        {
            var geometry = new GeometryPersistence("ExampleWindow", 800, 600);
            var start = "https://www.google.com";
            var window = new BrowserWindow(start, geometry, Settings.AppDataFolder)
            {
                Title = "AppHostCefSharp"
            };

            Show(window);
        }

        private static void Show(Window window)
        {
            ExcelAsyncUtil.QueueAsMacro(() =>
            {
                if (Application.Current == null)
                {
                    new Application().ShutdownMode = ShutdownMode.OnExplicitShutdown;
                }

                ElementHost.EnableModelessKeyboardInterop(window);

                if (Application.Current != null)
                {
                    Application.Current.MainWindow = window;
                }

                window.Show();
            });
        }
    }
}
