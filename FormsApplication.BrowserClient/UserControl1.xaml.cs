using System;
using System.Windows.Controls;
using CefSharp;

namespace Example.FormsApplication.BrowserClient
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1(string textToDisplay)
        {
            InitializeComponent();

            if (!Cef.IsInitialized)
            {
                var settings = new CefSettings();
                settings.EnableInternalPdfViewerOffScreen();

                // Disable GPU in WPF and Offscreen examples (Windows 7)
                // until #1634 has been resolved.
                var osVersion = Environment.OSVersion;
                if (osVersion.Version.Major == 6 && osVersion.Version.Minor == 1)
                {
                    settings.CefCommandLineArgs.Add("disable-gpu", "1");
                }

                Cef.Initialize(settings, shutdownOnProcessExit: true, performDependencyCheck: true);
            }

            //Content = new TextBlock
            //{
            //    Text = textToDisplay
            //};
        }
    }
}
