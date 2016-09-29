using System;
using RedGate.AppHost.Interfaces;

namespace Example.FormsApplication
{
    public class BrowserServiceLocator : MarshalByRefObject, IAppHostServices
    {
        private readonly string url;

        public BrowserServiceLocator(string url)
        {
            this.url = url;
        }

        public T GetService<T>() where T : class
        {
            return new BrowserService(url) as T;
        }
    }
}
