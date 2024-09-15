using CommunityToolkit.Mvvm.ComponentModel;

namespace NuitkaPackager.ViewModels;

public partial class FileInfoSettingsViewModel : ObservableRecipient
{
    public FileInfoSettingsViewModel()
    {
    }
    [ObservableProperty]
    private string? _companyName = AppConfig.CompanyName;
    partial void OnCompanyNameChanged(string? value)
    {
        AppConfig.CompanyName = value;
    }

    [ObservableProperty]
    private string? _productName = AppConfig.ProductName;
    partial void OnProductNameChanged(string? value)
    {
        AppConfig.ProductName = value;
    }

    [ObservableProperty]
    private string? _fileDescription = AppConfig.FileDescription;
    partial void OnFileDescriptionChanged(string? value)
    {
        AppConfig.FileDescription = value;
    }

    [ObservableProperty]
    private string? _fileVersion = AppConfig.FileVersion;
    partial void OnFileVersionChanged(string? value)
    {
        AppConfig.FileVersion = value;
    }

    [ObservableProperty]
    private string? _productVersion = AppConfig.ProductVersion;
    partial void OnProductVersionChanged(string? value)
    {
        AppConfig.ProductVersion = value;
    }
    [ObservableProperty]
    private string? _copyright = AppConfig.Copyright;
    partial void OnCopyrightChanged(string? value)
    {
        AppConfig.Copyright = value;
    }

    [ObservableProperty]
    private string? _trademarks = AppConfig.Trademarks;
    partial void OnTrademarksChanged(string? value)
    {
        AppConfig.Trademarks = value;
    }
}
