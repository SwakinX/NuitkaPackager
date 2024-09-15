using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Windows.Storage.Pickers;
using NuitkaPackager.Views;
using Newtonsoft.Json.Linq;

namespace NuitkaPackager.ViewModels;

public partial class BasicSettingsViewModel : ObservableRecipient
{
    //[ObservableProperty]
    //private  ISettingsService settingsService;

    [ObservableProperty]
    private bool _isEnableIcon = AppConfig.IsEnableIcon;
    partial void OnIsEnableIconChanged(bool value)
    {
        AppConfig.IsEnableIcon = value;
    }
    [ObservableProperty]
    private bool _IsEnableOut = AppConfig.IsEnableOut;
    partial void OnIsEnableOutChanged(bool value)
    {
        AppConfig.IsEnableOut = value;
    }

    [ObservableProperty]
    private string? _pyFilePath = AppConfig.PyFilePath;
    partial void OnPyFilePathChanged(string? value)
    {
        AppConfig.PyFilePath = value;
        }
    [ObservableProperty]
    private string? _iconPath = AppConfig.IconPath;
    partial void OnIconPathChanged(string? value)
    {
        AppConfig.IconPath = value;
    }
    [ObservableProperty]
    private string? _outputPath = AppConfig.OutputPath;
    partial void OnOutputPathChanged(string? value)
    {
        AppConfig.OutputPath = value;
    }
    [ObservableProperty]
    private bool _isMingw64 = AppConfig.IsMingw64;
    partial void OnIsMingw64Changed(bool value)
    {
        AppConfig.IsMingw64 = value;
    }

    [ObservableProperty]
    private bool _isaYesfdownload = AppConfig.IsaYesfdownload;
    partial void OnIsaYesfdownloadChanged(bool value)
    {
        AppConfig.IsaYesfdownload = value;
    }

    [ObservableProperty]
    private bool _isAdmin = AppConfig.IsAdmin;
    partial void OnIsAdminChanged(bool value)
    {
        AppConfig.IsAdmin = value;
    }

    [ObservableProperty]
    private bool _isShowMemory = AppConfig.IsShowMemory;
    partial void OnIsShowMemoryChanged(bool value)
    {
        AppConfig.IsShowMemory = value;
    }

    [ObservableProperty]
    private bool _isSandalone = AppConfig.IsSandalone;
    partial void OnIsSandaloneChanged(bool value)
    {
        AppConfig.IsSandalone = value;
    }
    [ObservableProperty]

    private bool _isRemoveOutput = AppConfig.IsRemoveOutput;
    partial void OnIsRemoveOutputChanged(bool value)
    {
        AppConfig.IsRemoveOutput = value;
    }

    [ObservableProperty]
    private bool _isDisableConsole = AppConfig.IsDisableConsole;
    partial void OnIsDisableConsoleChanged(bool value)
    {
        AppConfig.IsDisableConsole = value;
    }

    [ObservableProperty]
    private bool _isOneFile = AppConfig.IsOneFile;
    partial void OnIsOneFileChanged(bool value)
    {
        AppConfig.IsOneFile = value;
    }

    [ObservableProperty]
    private bool _isModule = AppConfig.IsModule;
    partial void OnIsModuleChanged(bool value)
    {
        AppConfig.IsModule = value;
    }

    public BasicSettingsViewModel()
    {
        //SettingsService = settingsService;
        //PyFilePath = settingsService.PyFilePath;
    }

    public void Initialize()
    {

     }

    [RelayCommand]
    private async Task SelectFileAsync(string? parameter = null)
    {
        try
        {
            var file = await AppConfig.OpenFilePickerAsync(parameter);
            if (file != null)
            {
                // 处理选择的文件，例如显示文件路径或执行其他操作
                if (parameter == ".py")
                    PyFilePath = file.Path;
                else if (parameter == ".ico")
                    IconPath = file.Path;
            }
        }
        catch
        {
        }
    }
    [RelayCommand]
    private async Task SelectFolderAsync(string? parameter = null)
    {
        try
        {
            var folder = await AppConfig.OpenFolderPickerAsync();
            if (folder != null)
            {
                    OutputPath = folder.Path;
            }
        }
        catch
        {
        }
    }
}
