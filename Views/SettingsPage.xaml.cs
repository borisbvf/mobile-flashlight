using Torch.ViewModels;

namespace Torch.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();

		BindingContext = settingsViewModel;
	}
}