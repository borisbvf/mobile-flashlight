using System.Windows.Input;
using Torch.Services;

namespace Torch
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

		public LocalizationManager LocalizationManager => LocalizationManager.Instance;

        private bool buttonOn = false;

        private string imageSource = "buttonoff.png";
        public string ImageSource
        {
            get => imageSource;
            set
            {
                if (value != imageSource)
                {
                    imageSource = value;
                    OnPropertyChanged();
                }
            }
        }

		public ICommand SettingsCommand => new Command(GoToSettings);
        private async void GoToSettings()
        {
            await Shell.Current.GoToAsync($"{Constants.SettingsPageRoute}");
        }

        public ICommand TurnOnOffCommand => new Command(TurnOnOff);
        private async void TurnOnOff()
        {
            bool isSupported = await Flashlight.Default.IsSupportedAsync();
            if (isSupported)
            {
                try
                {
                    if (!buttonOn)
                    {
                        await Flashlight.Default.TurnOnAsync();
                    }
                    else
                    {
                        await Flashlight.Default.TurnOffAsync();
                    }
					buttonOn = !buttonOn;
				}
                catch (FeatureNotSupportedException ex)
                {
                    await DisplayAlert(
                        LocalizationManager["Error"].ToString(),
                        $"{LocalizationManager["MsgNotSupported"].ToString()} {ex.Message}",
						LocalizationManager["Ok"].ToString());
                }
                catch (PermissionException ex)
                {
                    await DisplayAlert(
                        LocalizationManager["Error"].ToString(),
                        $"{LocalizationManager["MsgNoPermission"].ToString()} {ex.Message}",
						LocalizationManager["Ok"].ToString());
                }
                catch (Exception ex)
                {
                    await DisplayAlert(
                        LocalizationManager["Error"].ToString(),
                        $"{ex.Message}",
						LocalizationManager["Ok"].ToString());
                }
				ImageSource = buttonOn ? "buttonon.png" : "buttonoff.png";
			}
            else
            {
                await DisplayAlert(
                    LocalizationManager["Error"].ToString(), 
                    LocalizationManager["MsgNotSupported"].ToString(),
					LocalizationManager["Ok"].ToString());
            }
        }
	}

}
