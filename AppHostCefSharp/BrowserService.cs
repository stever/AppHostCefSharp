using System;
using System.Diagnostics;
using System.Runtime.Remoting.Lifetime;
using AppHostCefSharp.Services;

namespace AppHostCefSharp
{
    public class BrowserService : MarshalByRefObject, IBrowserService, ISponsor
    {
        public string URL { get; }
        public string AppDataPath { get; }
        public bool Closed { get; private set; }

        public BrowserService(string url, string appDataPath)
        {
            URL = url;
            AppDataPath = appDataPath;
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
            var ret = (ILease)base.InitializeLifetimeService();
            Debug.Assert(ret != null);
            ret.SponsorshipTimeout = TimeSpan.FromMinutes(2);
            ret.Register(this);
            return ret;
        }
    }
}
