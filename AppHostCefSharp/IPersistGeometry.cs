using System.Windows;

namespace SteveRGB.AppHostCefSharp
{
    public interface IPersistGeometry
    {
        void Persist(Window window);
        void Restore(Window window);
    }
}
