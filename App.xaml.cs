using Torch.Services;
using System.ComponentModel;

namespace Torch
{
    public partial class App : Application
    {
        TorchService _torchService;
        public App(TorchService torchService)
        {
            InitializeComponent();

            _torchService = torchService;

            MainPage = new AppShell();

            ThemeService.Instance.LoadFromPreferences();
            SetAppTheme();
            ThemeService.Instance.PropertyChanged += OnSettingsPropertyChanged;

            if (Preferences.Default.ContainsKey(Constants.LanguageKey))
            {
                string lang = Preferences.Default.Get(Constants.LanguageKey, Constants.EnglishLang);
				LocalizationManager.Instance.SetLanguage(lang);
            }
        }

        private void SetAppTheme()
        {
            UserAppTheme = ThemeService.Instance?.Theme ?? AppTheme.Unspecified;
        }

        private void OnSettingsPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ThemeService.Theme))
            {
                SetAppTheme();
            }
        }

		protected override Window CreateWindow(IActivationState? activationState)
		{
            Window window = base.CreateWindow(activationState);
            window.Activated += async (s, e) =>
            {
                if (Preferences.Default.Get(Constants.StartActivation, false))
                    await _torchService.TurnOnOff();
            };
            return window;
		}
    }
}
