using System;
using System.Windows.Controls;
using System.Windows.Threading;
using RedGate.AppHost.Server;

namespace AppHostCefSharp
{
    public partial class BrowserWindow
    {
        private readonly BrowserServiceLocator locator;
        private readonly DispatcherTimer timer;

        public BrowserWindow()
            : this("chrome://version", null, null)
        { }

        public BrowserWindow(string url, IPersistGeometry geometry)
            : this(url, geometry, null)
        { }

        public BrowserWindow(string url, IPersistGeometry geometry, string appDataPath)
        {
            InitializeComponent();
            try
            {
                geometry?.Restore(this);

                var safeAppHostChildHandle = new ChildProcessFactory()
                    .Create("AppHostCefSharp.WebBrowser.dll");

                locator = new BrowserServiceLocator(url, appDataPath);
                Content = safeAppHostChildHandle.CreateElement(locator);

                Closing += (sender, args) =>
                {
                    locator.Send("Close");
                    geometry?.Persist(this);
                };

                timer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 0, 500)};
                timer.Tick += TimerTick;
                timer.Start();
            }
            catch (Exception ex)
            {
                Content = new TextBlock { Text = ex.ToString() };
            }
        }

        public void Send(string msg)
        {
            locator.Send(msg);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            while (locator.ReturnMessageCount > 0)
            {
                var msg = locator.GetReturnMessage();
                if (msg != null)
                {
                    System.Windows.MessageBox.Show(msg);
                }
            }
        }
    }
}
