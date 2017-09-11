using System;
using System.Collections.Generic;
using RedGate.AppHost.Interfaces;

namespace AppHostCefSharp
{
    public class BrowserServiceLocator : MarshalByRefObject, IAppHostServices
    {
        private readonly string url;
        private readonly string appDataPath;
        private readonly List<BrowserService> services = new List<BrowserService>();

        public BrowserServiceLocator(string url, string appDataPath)
        {
            this.url = url;
            this.appDataPath = appDataPath;
        }

        public T GetService<T>() where T : class
        {
            var service = new BrowserService(url, appDataPath);
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
