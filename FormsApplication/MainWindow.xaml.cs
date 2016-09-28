using System;
using System.Windows;
using System.Windows.Controls;
using RedGate.AppHost.Server;

namespace Example.FormsApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                var safeAppHostChildHandle = new ChildProcessFactory().Create("Example.FormsApplication.BrowserClient.dll");

                Content = safeAppHostChildHandle.CreateElement(new ServiceLocator());
            }
            catch (Exception e)
            {
                Content = new TextBlock
                {
                    Text = e.ToString()
                };
            }
        }
    }
}
