namespace Torch.Services;
public class TorchService
{
	public LocalizationManager LocalizationManager => LocalizationManager.Instance;

	public bool TorchIsOn { get; set; }

	public async Task<(bool IsSuccess, string? ErrorMessage)> TurnOnOff()
	{
		bool isSuccess = false;
		string? errorMessage = null;
		bool isSupported = await Flashlight.Default.IsSupportedAsync();
		if (isSupported)
		{
			try
			{
				if (!TorchIsOn)
				{
					await Flashlight.Default.TurnOnAsync();
					HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
				}
				else
				{
					await Flashlight.Default.TurnOffAsync();
					HapticFeedback.Default.Perform(HapticFeedbackType.Click);
				}
				isSuccess = true;
				TorchIsOn = !TorchIsOn;
				OnSwitchingTorch?.Invoke(this, new EventArgs());
			}
			catch (FeatureNotSupportedException ex)
			{
				errorMessage = $"{LocalizationManager["MsgNotSupported"].ToString()} : {ex.Message}";
			}
			catch (PermissionException ex)
			{
				errorMessage = $"{LocalizationManager["MsgNoPermission"].ToString()} : {ex.Message}";
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}
		}
		else
		{
			errorMessage = LocalizationManager["MsgNotSupported"].ToString();
		}
		return (isSuccess, errorMessage);
	}

	public event EventHandler? OnSwitchingTorch;
}
