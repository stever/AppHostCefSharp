using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using CefSharp;
using AppHostCefSharp.Services;
using ExcelDnaExample.BoundProxy;

namespace AppHostCefSharp.WebBrowser
{
    public partial class BrowserControl : IContextMenuHandler
    {
        private const string AppDataFolderCache = "Cache";
        private const string AppDataFolderUserData = "User Data";

        private readonly string appDataFolder;
        private readonly IBrowserService service;
        private readonly DispatcherTimer dispatcherTimer;

        public BrowserControl(IBrowserService service)
        {
            appDataFolder = service.AppDataPath;

            CefInit();
            InitializeComponent();

            this.service = service;

            Browser.MenuHandler = this;
            Browser.RegisterAsyncJsObject("bound", new AddinAsyncBoundObject());

            var url = service.URL;
            if (url != null) Browser.Address = url;

            // Timer to check if the browser window has been closed.
            dispatcherTimer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 1)};
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (!service.Closed)
            {
                return;
            }

            // Stop timer and shut down CEF when browser window closed.
            dispatcherTimer.Stop();

            if (Cef.IsInitialized)
            {
                Cef.Shutdown();
            }

            Environment.Exit(0);
        }

        private void CefInit()
        {
            if (Cef.IsInitialized)
            {
                return;
            }

            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("disable-gpu", "1");

            if (appDataFolder != null)
            {
                var appDataRoot = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var path = Path.Combine(appDataRoot, appDataFolder);
                settings.CachePath = Path.Combine(path, AppDataFolderCache);
                settings.UserDataPath = Path.Combine(path, AppDataFolderUserData);
                settings.IgnoreCertificateErrors = true;
            }

            Cef.Initialize(settings);
        }

        #region IContextMenuHandler

        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            model.Clear();
        }

        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            return false;
        }

        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        { }

        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            return false;
        }

        #endregion

        #region Debug menu

        private void MenuItem_Click_ShowDevTools(object sender, RoutedEventArgs e)
        {
            Browser.ShowDevTools();
        }

        private void MenuItem_Click_CloseDevTools(object sender, RoutedEventArgs e)
        {
            Browser.CloseDevTools();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
#if !DEBUG
            var menubar = (Menu)FindName("MenuBar");
            if (menubar != null) menubar.Visibility = Visibility.Collapsed;
#endif
        }

        private void MenuItem_Click_PageReload(object sender, RoutedEventArgs e)
        {
            Browser.Reload();
        }

        private void MenuItem_Click_NavigateBack(object sender, RoutedEventArgs e)
        {
            Browser.Back();
        }

        private void MenuItem_Click_NavigateForward(object sender, RoutedEventArgs e)
        {
            Browser.Forward();
        }

        #endregion
    }
}
