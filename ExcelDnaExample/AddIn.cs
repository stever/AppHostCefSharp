using System;
using System.IO;
using System.ServiceModel;
using System.Windows;
using System.Windows.Forms.Integration;
using AppHostCefSharp;
using ExcelDna.Integration;
using ExcelDnaExample.Services;

namespace ExcelDnaExample
{
    public class Addin : IExcelAddIn
    {
        private static readonly ServiceHost PipeHost;

        static Addin()
        {
            // Start pipe host
            PipeHost = new ServiceHost(typeof(AddinServiceHost), new Uri("net.pipe://localhost"));
            PipeHost.AddServiceEndpoint(typeof(IAddinServiceHost), new NetNamedPipeBinding(), "ExcelAddinServiceHost");
            PipeHost.Open();
        }

        public Addin()
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
            const string start = "file:///Page.html";
            var geometry = new GeometryPersistence("ExampleWindow", 800, 600);
            const string appData = Settings.AppDataFolder;
            const string assembly = "AppHostCefSharp.WebBrowser.dll";
            const string title = "AppHostCefSharp";
            var window = new BrowserWindow(start, geometry, appData, assembly) {Title = title};
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
