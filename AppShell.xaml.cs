using Flashlight.Views;

namespace Flashlight
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
