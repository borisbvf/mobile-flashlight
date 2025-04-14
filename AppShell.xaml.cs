using Torch.Views;

namespace Torch
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(Constants.SettingsPageRoute, typeof(SettingsPage));

            BindingContext = this;
        }
    }
}
