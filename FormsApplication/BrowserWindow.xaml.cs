using System;
using System.Windows;
using System.Windows.Controls;
using RedGate.AppHost.Server;

namespace Example.FormsApplication
{
    /// <summary>
    /// Interaction logic for BrowserWindow.xaml
    /// </summary>
    public partial class BrowserWindow : Window
    {
        public BrowserWindow()
        {
            InitializeComponent();

            try
            {
                var safeAppHostChildHandle = new ChildProcessFactory().Create("Example.FormsApplication.BrowserClient.dll");

                Content = safeAppHostChildHandle.CreateElement(new BrowserServiceLocator());
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
