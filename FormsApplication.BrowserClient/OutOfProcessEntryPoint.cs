using System.Windows;
using CefSharp;
using Example.FormsApplication.Services;
using RedGate.AppHost.Interfaces;

namespace Example.FormsApplication.BrowserClient
{
    public class OutOfProcessEntryPoint : IOutOfProcessEntryPoint
    {
        public FrameworkElement CreateElement(IAppHostServices services)
        {
            if (!Cef.IsInitialized)
            {
                var settings = new CefSettings();

                // Problems on Windows 7 at least need this setting currently.
                settings.CefCommandLineArgs.Add("disable-gpu", "1");

                Cef.Initialize(settings, shutdownOnProcessExit: true, performDependencyCheck: true);
            }

            return new BrowserControl(services.GetService<IBrowserService>());
        }
    }
}
