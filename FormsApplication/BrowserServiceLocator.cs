using System;
using RedGate.AppHost.Interfaces;

namespace Example.FormsApplication
{
    public class BrowserServiceLocator : MarshalByRefObject, IAppHostServices
    {
        public T GetService<T>() where T : class
        {
            return new BrowserService() as T;
        }
    }
}
