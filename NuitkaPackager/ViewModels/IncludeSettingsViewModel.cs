using CommunityToolkit.Mvvm.ComponentModel;

namespace NuitkaPackager.ViewModels;

public partial class IncludeSettingsViewModel : ObservableRecipient
{
    public IncludeSettingsViewModel()
    {
    }
    [ObservableProperty]
    private bool _isFollowImports = AppConfig.IsFollowImports;
    partial void OnIsFollowImportsChanged(bool value)
    {
        AppConfig.IsFollowImports = value;
    }

    [ObservableProperty]
    private string? _includePackage = AppConfig.IncludePackage;
    partial void OnIncludePackageChanged(string? value)
    {
        AppConfig.IncludePackage = value;
    }

    [ObservableProperty]
    private string? _nofollowImport = AppConfig.NofollowImport;
    partial void OnNofollowImportChanged(string? value)
    {
        AppConfig.NofollowImport = value;
    }

    [ObservableProperty]
    private string? _includeModule = AppConfig.IncludeModule;
    partial void OnIncludeModuleChanged(string? value)
    {
        AppConfig.IncludeModule = value;
    }

    [ObservableProperty]
    private bool _isQt = AppConfig.IsQt;
    partial void OnIsQtChanged(bool value)
    {
        AppConfig.IsQt = value;
    }

    [ObservableProperty]
    private bool _isPyside6 = AppConfig.IsPyside6;
    partial void OnIsPyside6Changed(bool value)
    {
        AppConfig.IsPyside6 = value;
    }

    [ObservableProperty]
    private bool _isTkinter = AppConfig.IsTkinter;
    partial void OnIsTkinterChanged(bool value)
    {
        AppConfig.IsTkinter = value;
    }

    [ObservableProperty]
    private bool _isNumpy = AppConfig.IsNumpy;
    partial void OnIsNumpyChanged(bool value)
    {
        AppConfig.IsNumpy = value;
    }

    [ObservableProperty]
    private bool _isTorch = AppConfig.IsTorch;
    partial void OnIsTorchChanged(bool value)
    {
        AppConfig.IsTorch = value;
    }

    [ObservableProperty]
    private bool _isTensorflow = AppConfig.IsTensorflow;
    partial void OnIsTensorflowChanged(bool value)
    {
        AppConfig.IsTensorflow = value;
    }

    [ObservableProperty]
    private bool _isAntibloat = AppConfig.IsAntibloat;
    partial void OnIsAntibloatChanged(bool value)
    {
        AppConfig.IsAntibloat = value;
    }

    [ObservableProperty]
    private bool _isMtiprocessing = AppConfig.IsMtiprocessing;
    partial void OnIsMtiprocessingChanged(bool value)
    {
        AppConfig.IsMtiprocessing = value;
    }

}
