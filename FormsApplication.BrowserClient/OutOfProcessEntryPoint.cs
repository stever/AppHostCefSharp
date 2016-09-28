using System.Windows;
using Example.FormsApplication.Services;
using RedGate.AppHost.Interfaces;

namespace Example.FormsApplication.BrowserClient
{
    public class OutOfProcessEntryPoint : IOutOfProcessEntryPoint
    {
        public FrameworkElement CreateElement(IAppHostServices service)
        {
            var serverThing = service.GetService<IServerImplementedThingThatClientNeeds>();

            string textToDisplay = serverThing.GetTextToDisplay();

            return new UserControl1(textToDisplay);
        }
    }
}
