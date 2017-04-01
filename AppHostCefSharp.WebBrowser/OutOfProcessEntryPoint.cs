using System.Windows;
using RedGate.AppHost.Interfaces;
using AppHostCefSharp.Services;

namespace AppHostCefSharp.WebBrowser
{
    public class OutOfProcessEntryPoint : IOutOfProcessEntryPoint
    {
        public FrameworkElement CreateElement(IAppHostServices services)
        {
            return new BrowserControl(services.GetService<IBrowserService>());
        }
    }
}
