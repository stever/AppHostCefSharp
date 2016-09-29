using System.Windows.Controls;

namespace Example.FormsApplication.BrowserClient
{
    public partial class BrowserControl : UserControl
    {
        public BrowserControl() : this("about:blank")
        { }

        public BrowserControl(string url)
        {
            InitializeComponent();

            if (url != null)
                Browser.Address = url;
        }
    }
}
