using System.Windows.Input;
using Torch.Services;

namespace Torch
{
    public partial class MainPage : ContentPage
    {
        TorchService _torchService;
        public MainPage(TorchService torchService)
        {
            InitializeComponent();
            _torchService = torchService;
            BindingContext = this;
			ImageSource = _torchService.TorchIsOn ? "buttonon.png" : "buttonoff.png";
            _torchService.OnSwitchingTorch += (s, e) =>
            {
				ImageSource = _torchService.TorchIsOn ? "buttonon.png" : "buttonoff.png";
			};
		}

		public LocalizationManager LocalizationManager => LocalizationManager.Instance;

        public double ButtonSide
        {
            get => (DeviceDisplay.Current.MainDisplayInfo.Width < DeviceDisplay.Current.MainDisplayInfo.Height
                ? DeviceDisplay.Current.MainDisplayInfo.Width
                : DeviceDisplay.Current.MainDisplayInfo.Height) / DeviceDisplay.Current.MainDisplayInfo.Density * 2 / 3;
		}

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
            (bool isSuccess, string? errorMessage) = await _torchService.TurnOnOff();
            if (!isSuccess)
            {
				await DisplayAlert(
					$"{LocalizationManager["Error"]}",
					$"{errorMessage}",
					$"{LocalizationManager["Ok"]}");
			}
			ImageSource = _torchService.TorchIsOn ? "buttonon.png" : "buttonoff.png";
		}
	}

}
