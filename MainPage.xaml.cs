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
            if (buttonOn)
            {
                ImageSource = "buttonoff.png";
            }
            else
            {
                ImageSource = "buttonon.png";
            }
            buttonOn = !buttonOn;
        }
	}

}
