

namespace NuitkaPackager.ViewModels;

public partial class AdvanceSettingsViewModel : ObservableRecipient
{
    public AdvanceSettingsViewModel()
    {
    }
    [ObservableProperty]
    private bool _isIncludeDataFile = AppConfig.IsIncludeDataFile;
    partial void OnIsIncludeDataFileChanged(bool value)
    {
        AppConfig.IsIncludeDataFile = value;
    }

    [ObservableProperty]
    private bool _isIncludeDataDir = AppConfig.IsIncludeDataDir;
    partial void OnIsIncludeDataDirChanged(bool value)
    {
        AppConfig.IsIncludeDataDir = value;
    }

    [ObservableProperty]
    private bool _isCopyDependency = AppConfig.IsCopyDependency;
    partial void OnIsCopyDependencyChanged(bool value)
    {
        AppConfig.IsCopyDependency = value;
    }

    [ObservableProperty]
    private string? _dataFile = AppConfig.DataFile;
    partial void OnDataFileChanged(string? value)
    {
        AppConfig.DataFile = value;
    }

    [ObservableProperty]
    private string? _dataDir = AppConfig.DataDir;
    partial void OnDataDirChanged(string? value)
    {
        AppConfig.DataDir = value;
    }

    [ObservableProperty]
    private string? _dependencyDir = AppConfig.DependencyDir;
    partial void OnDependencyDirChanged(string? value)
    {
        AppConfig.DependencyDir = value;
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
                DataFile += file.Path + "=\n";
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
                if (parameter == "DataDir")
                {
                    DataDir += folder.Path + "=\n";
                }
                else if (parameter == "DependencyDir")
                {
                    DependencyDir = folder.Path;
                }
            }
        }
        catch
        {

        }

}
}
