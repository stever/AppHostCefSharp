namespace AppHostCefSharp.Services
{
    public interface IBrowserService
    {
        string URL { get; }
        string AppDataPath { get; }
        bool Closed { get; }
    }
}
