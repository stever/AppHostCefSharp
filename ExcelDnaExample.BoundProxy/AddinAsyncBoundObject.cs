using System.ServiceModel;
using ExcelDnaExample.Services;

namespace ExcelDnaExample.BoundProxy
{
    public class AddinAsyncBoundObject
    {
        private readonly IAddinServiceHost pipeProxy;

        public AddinAsyncBoundObject()
        {
            var pipeFactory = new ChannelFactory<IAddinServiceHost>(
                new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/ExcelAddinServiceHost"));

            pipeProxy = pipeFactory.CreateChannel();
        }

        public string GetSheetNames()
        {
            return pipeProxy.GetSheetNames();
        }

        public void SelectSheet(string name)
        {
            pipeProxy.SelectSheet(name);
        }
    }
}
