using System.Windows;
using CefSharp;
using Example.FormsApplication.Services;
using RedGate.AppHost.Interfaces;

namespace Example.FormsApplication.BrowserClient
{
    public class OutOfProcessEntryPoint : IOutOfProcessEntryPoint
    {
        public FrameworkElement CreateElement(IAppHostServices service)
        {
            if (!Cef.IsInitialized)
            {
                var settings = new CefSettings();

                // Problems on Windows 7 at least need this setting currently.
                settings.CefCommandLineArgs.Add("disable-gpu", "1");

                // The following helps avoid blurry fonts using WPF.
                settings.CefCommandLineArgs.Add("disable-direct-write", "1");

                Cef.Initialize(settings, shutdownOnProcessExit: true, performDependencyCheck: true);
            }

            var serverThing = service.GetService<IBrowserService>();
            string textToDisplay = serverThing.GetTextToDisplay();
            return new BrowserControl(textToDisplay);
        }
    }
}
