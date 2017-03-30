using System;
using System.Collections.Generic;
using RedGate.AppHost.Interfaces;

namespace SteveRGB.AppHostCefSharp
{
    public class BrowserServiceLocator : MarshalByRefObject, IAppHostServices
    {
        private readonly string url;
        private readonly List<BrowserService> services = new List<BrowserService>();

        public BrowserServiceLocator(string url)
        {
            this.url = url;
        }

        public T GetService<T>() where T : class
        {
            var service = new BrowserService(url);
            services.Add(service);
            return service as T;
        }

        public void Close()
        {
            foreach (var service in services)
            {
                service.Close();
            }
        }
    }
}
