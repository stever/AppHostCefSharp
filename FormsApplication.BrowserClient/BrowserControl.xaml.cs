using System.Windows.Controls;

namespace Example.FormsApplication.BrowserClient
{
    public partial class BrowserControl : UserControl
    {
        public BrowserControl(string textToDisplay)
        {
            InitializeComponent();

            //Content = new TextBlock
            //{
            //    Text = textToDisplay
            //};
        }
    }
}
