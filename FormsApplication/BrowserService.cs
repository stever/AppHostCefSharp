using System;
using System.Runtime.Remoting.Lifetime;
using Example.FormsApplication.Services;

namespace Example.FormsApplication
{
    public class BrowserService : MarshalByRefObject, IBrowserService, ISponsor
    {
        private readonly string url;

        public BrowserService(string url)
        {
            this.url = url;
        }

        public string URL => url;

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
