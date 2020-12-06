using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting.Lifetime;
using AppHostCefSharp.Services;

namespace AppHostCefSharp
{
    public class BrowserService : MarshalByRefObject, IBrowserService, ISponsor
    {
        public string URL { get; }
        public string AppDataPath { get; }
        public int MessageCount => MessageQueue.Count;
        public int ReturnMessageCount => ReturnMessageQueue.Count;
        public Queue<string> MessageQueue { get; } = new Queue<string>();
        public Queue<string> ReturnMessageQueue { get; } = new Queue<string>();

        public BrowserService(string url, string appDataPath)
        {
            URL = url;
            AppDataPath = appDataPath;
        }

        public string GetMessage()
        {
            return MessageQueue.Count > 0 
                ? MessageQueue.Dequeue() 
                : null;
        }

        public string GetReturnMessage()
        {
            return ReturnMessageQueue.Count > 0 
                ? ReturnMessageQueue.Dequeue() 
                : null;
        }

        public override object InitializeLifetimeService()
        {
            var ret = (ILease)base.InitializeLifetimeService();
            Debug.Assert(ret != null);
            ret.SponsorshipTimeout = TimeSpan.FromMinutes(2);
            ret.Register(this);
            return ret;
        }

        public TimeSpan Renewal(ILease lease)
        {
            return TimeSpan.FromMinutes(1);
        }

        public void SendInReturn(string msg)
        {
            ReturnMessageQueue.Enqueue(msg);
        }
    }
}
