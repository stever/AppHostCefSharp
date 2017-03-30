using System.IO;
using Nancy;

namespace SteveRGB.ExcelDnaExample
{
    /// <summary>
    /// This class is provided to set root path depending on the build
    /// configuration. During development it is useful to use the content folder
    /// in the source folder, so that this can be edited and developed without
    /// restarting Excel and the embedded web server. Otherwise we want to copy
    /// the content to the built application output folder.
    /// </summary>
    public class CustomRootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
#if !DEBUG
            return AddIn.AssemblyDirectory;
#else
            return Path.GetFullPath(Path.Combine(AddIn.AssemblyDirectory, @"..\.."));
#endif
        }
    }
}
