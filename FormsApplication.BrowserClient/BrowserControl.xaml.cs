using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using CefSharp;
using Example.FormsApplication.Services;

namespace Example.FormsApplication.BrowserClient
{
    public partial class BrowserControl : UserControl
    {
        private readonly IBrowserService service;
        private readonly DispatcherTimer dispatcherTimer;

        public BrowserControl()
        {
            if (!Cef.IsInitialized)
            {
                var settings = new CefSettings();
                settings.CefCommandLineArgs.Add("disable-gpu", "1");
                Cef.Initialize(settings);
            }

            InitializeComponent();
        }

        public BrowserControl(IBrowserService service) : this()
        {
            this.service = service;

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
        }
    }
}
