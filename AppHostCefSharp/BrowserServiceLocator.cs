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

        public void Send(string msg)
        {
            foreach (var service in services)
            {
                service.MessageQueue.Enqueue(msg);
            }
        }

        public int ReturnMessageCount
        {
            get
            {
                var total = 0;
                foreach (var service in services)
                {
                    total += service.ReturnMessageCount;
                }
                return total;
            }
        }

        public string GetReturnMessage()
        {
            foreach (var service in services)
            {
                if (service.ReturnMessageCount > 0)
                {
                    return service.GetReturnMessage();
                }
            }
            return null;
        }
    }
}
