namespace SteveRGB.AppHostCefSharp.Services
{
    public interface IBrowserService
    {
        string URL { get; }
        bool Closed { get; }
    }
}
