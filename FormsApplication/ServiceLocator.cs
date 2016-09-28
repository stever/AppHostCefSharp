using System;
using RedGate.AppHost.Interfaces;

namespace Example.FormsApplication
{
    public class ServiceLocator : MarshalByRefObject, IAppHostServices
    {
        public T GetService<T>() where T : class
        {
            return new ServerImplementedThingThatClientNeeds() as T;
        }
    }
}
