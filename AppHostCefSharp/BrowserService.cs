using System;
using System.Runtime.Remoting.Lifetime;
using SteveRGB.AppHostCefSharp.Services;

namespace SteveRGB.AppHostCefSharp
{
    public class BrowserService : MarshalByRefObject, IBrowserService, ISponsor
    {
        public string URL { get; }
        public bool Closed { get; private set; }

        public BrowserService(string url)
        {
            URL = url;
        }

        public void Close()
        {
            Closed = true;
        }

        public TimeSpan Renewal(ILease lease)
        {
            return TimeSpan.FromMinutes(1);
        }

        public override object InitializeLifetimeService()
        {
            ILease ret = (ILease)base.InitializeLifetimeService();
            ret.SponsorshipTimeout = TimeSpan.FromMinutes(2);
            ret.Register(this);
            return ret;
        }
    }
}
