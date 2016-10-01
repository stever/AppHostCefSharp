using System.Windows.Controls;
using Example.FormsApplication.Services;

namespace Example.FormsApplication.BrowserClient
{
    public partial class BrowserControl : UserControl
    {
        private readonly IBrowserService service;

        public BrowserControl()
        {
            InitializeComponent();
        }

        public BrowserControl(IBrowserService service) : this()
        {
            this.service = service;

            var url = service.URL;
            if (url != null) Browser.Address = url;
        }
    }
}
