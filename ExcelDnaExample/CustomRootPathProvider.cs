using System.IO;
using System.Reflection;
using log4net;
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
        private static readonly ILog Log = LogManager.
            GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public string GetRootPath()
        {
#if !DEBUG
            var result = AddIn.AssemblyDirectory;
#else
            var result = Path.GetFullPath(Path.Combine(AddIn.AssemblyDirectory, @"..\..\ExcelDnaExample"));
#endif
            Log.DebugFormat("Root: {0}", result);
            return result;
        }
    }
}
