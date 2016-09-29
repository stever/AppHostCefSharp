using System.Windows;
using System.Windows.Forms.Integration;
using Example.FormsApplication;
using ExcelDna.Integration;

namespace ExcelDnaExample
{
    public class AddIn : IExcelAddIn
    {
        void IExcelAddIn.AutoOpen()
        { }

        void IExcelAddIn.AutoClose()
        { }

        public static void ShowExampleForm()
        {
            Show(new BrowserWindow());
        }

        public static void Show(Window window)
        {
            ExcelAsyncUtil.QueueAsMacro(() =>
            {
                if (Application.Current == null)
                {
                    new Application().ShutdownMode = ShutdownMode.OnExplicitShutdown;
                }

                ElementHost.EnableModelessKeyboardInterop(window);

                if (Application.Current != null)
                {
                    Application.Current.MainWindow = window;
                }

                window.ShowDialog();
            });
        }
    }
}
