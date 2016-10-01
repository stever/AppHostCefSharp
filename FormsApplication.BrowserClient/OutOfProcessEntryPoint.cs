using System.Windows;
using Example.FormsApplication.Services;
using RedGate.AppHost.Interfaces;

namespace Example.FormsApplication.BrowserClient
{
    public class OutOfProcessEntryPoint : IOutOfProcessEntryPoint
    {
        public FrameworkElement CreateElement(IAppHostServices services)
        {
            return new BrowserControl(services.GetService<IBrowserService>());
        }
    }
}
