using System;
using System.Windows;
using System.Windows.Controls;
using RedGate.AppHost.Server;

namespace SteveRGB.AppHostCefSharp
{
    /// <summary>
    /// Interaction logic for BrowserWindow.xaml
    /// </summary>
    public partial class BrowserWindow : Window
    {
        public BrowserWindow() : this("chrome://version")
        { }

        public BrowserWindow(string url)
        {
            InitializeComponent();

            try
            {
                var safeAppHostChildHandle = new ChildProcessFactory()
                    .Create("AppHostCefSharp.WebBrowser.dll");

                var locator = new BrowserServiceLocator(url);
                Content = safeAppHostChildHandle.CreateElement(locator);
                Closing += (sender, args) => locator.Close();
            }
            catch (Exception e)
            {
                Content = new TextBlock {Text = e.ToString()};
            }
        }
    }
}
