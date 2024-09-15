using CommunityToolkit.Mvvm.ComponentModel;

namespace NuitkaPackager.ViewModels;

public partial class OthersSettingsViewModel : ObservableRecipient
{
    public OthersSettingsViewModel()
    {
    }

    [ObservableProperty]
    private bool _isFullyCustom = AppConfig.IsFullyCustom;
    partial void OnIsFullyCustomChanged(bool value)
    {
        AppConfig.IsFullyCustom = value;
    }
    [ObservableProperty]
    private bool _isCustomSetting = AppConfig.IsCustomSetting;
    partial void OnIsCustomSettingChanged(bool value)
    {
        AppConfig.IsCustomSetting = value;
    }

    [ObservableProperty]
    private string? _customSetting = AppConfig.CustomSetting;
    partial void OnCustomSettingChanged(string? value)
    {
        AppConfig.CustomSetting = value;
    }


}
