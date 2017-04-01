using System.Windows;

namespace AppHostCefSharp
{
    public interface IPersistGeometry
    {
        void Persist(Window window);
        void Restore(Window window);
    }
}
