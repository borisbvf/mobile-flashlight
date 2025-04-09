using Flashlight.ViewModels;

namespace Flashlight.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();

		BindingContext = settingsViewModel;
	}
}