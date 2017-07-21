namespace AppHostCefSharp.Services
{
    public interface IBrowserService
    {
        string URL { get; }
        string AppDataPath { get; }
        int MessageCount { get; }
        string GetMessage();
        void SendInReturn(string msg);
    }
}
