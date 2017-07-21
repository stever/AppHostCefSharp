using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using CefSharp;
using AppHostCefSharp.Services;

namespace AppHostCefSharp.WebBrowser
{
    public partial class BrowserControl : IContextMenuHandler
    {
        private const string AppDataFolderCache = "Cache";
        private const string AppDataFolderUserData = "User Data";

        private readonly string appDataFolder;
        private readonly IBrowserService service;
        private readonly DispatcherTimer timer;

        public BrowserControl()
        {
            CefInit();
            InitializeComponent();
        }

        public BrowserControl(IBrowserService service)
        {
            appDataFolder = service.AppDataPath;

            CefInit();
            InitializeComponent();

            this.service = service;

            Browser.MenuHandler = this;

            var url = service.URL;
            if (url != null) Browser.Address = url;

            timer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 0, 500)};
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            while (service.MessageCount > 0)
            {
                var msg = service.GetMessage();
                //if (msg != null)
                //{
                //    System.Windows.MessageBox.Show(msg);
                //}

                if (msg == "Refresh")
                {
                    Browser.Reload(ignoreCache:true);
                    return;
                }

                if (msg == "Close")
                {
                    timer.Stop();

                    if (Cef.IsInitialized)
                    {
                        Cef.Shutdown();
                    }

					Environment.Exit(0);
                    return;
                }
            }
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
    }
}
