using System.Windows;
using RedGate.AppHost.Interfaces;
using SteveRGB.AppHostCefSharp.Services;

namespace SteveRGB.AppHostCefSharp.WebBrowser
{
    public class OutOfProcessEntryPoint : IOutOfProcessEntryPoint
    {
        public FrameworkElement CreateElement(IAppHostServices services)
        {
            return new BrowserControl(services.GetService<IBrowserService>());
        }
    }
}
