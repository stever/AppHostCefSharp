using System;
using System.Windows.Controls;
using RedGate.AppHost.Server;

namespace AppHostCefSharp
{
    public partial class BrowserWindow
    {
        private readonly string url;
        private readonly string appDataPath;
        private readonly IPersistGeometry geometry;

        private IChildProcessHandle safeAppHostChildHandle;
        private BrowserServiceLocator locator;

        public BrowserWindow()
            : this("chrome://version", null, null, "AppHostCefSharp.WebBrowser.dll")
        { }

        public BrowserWindow(string url, IPersistGeometry geometry, string appDataPath, string childAssemblyName)
        {
            InitializeComponent();

            this.url = url;
            this.appDataPath = appDataPath;
            this.geometry = geometry;

            try
            {
                geometry?.Restore(this);
                safeAppHostChildHandle = new ChildProcessFactory().Create(childAssemblyName);
                locator = new BrowserServiceLocator(url, appDataPath);
                Content = safeAppHostChildHandle.CreateElement(locator);
                Closing += (sender, args) =>
                {
                    locator.Close();
                    geometry?.Persist(this);
                };
            }
            catch (Exception ex)
            {
                Content = new TextBlock { Text = ex.ToString() };
            }
        }

        public void Refresh()
        {
            locator.Close();
            geometry?.Persist(this);
            safeAppHostChildHandle = new ChildProcessFactory()
                .Create("AppHostCefSharp.WebBrowser.dll");
            locator = new BrowserServiceLocator(url, appDataPath);
            Content = safeAppHostChildHandle.CreateElement(locator);
        }
    }
}
