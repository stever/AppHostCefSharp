using System.Windows;
using System.Windows.Forms.Integration;
using ExcelDna.Integration;
using SteveRGB.AppHostCefSharp;

namespace SteveRGB.ExcelDnaExample
{
    public class AddIn : IExcelAddIn
    {
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
