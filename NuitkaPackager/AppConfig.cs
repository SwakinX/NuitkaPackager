using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.Extensions.Options;

using NuitkaPackager.Contracts.Services;
using NuitkaPackager.Core.Contracts.Services;
using NuitkaPackager.Core.Helpers;
using NuitkaPackager.Core.Services;
using NuitkaPackager.Helpers;
using NuitkaPackager.Models;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.Reflection;

namespace NuitkaPackager;

public static class AppConfig
{
    private static readonly ILocalSettingsService _localSettingsService = App.GetService<ILocalSettingsService>();

    #region Static Setting
    //基础设置
    public static bool IsEnableIcon
    {
        //get => Task.Run(() => GetValueAsync(true)).Result;
        //set => Task.Run(() => SetValueAsync(value)).Wait();
        get => GetValue(true);
        set => SetValue(value);
    }
    public static string? PyFilePath
    {
        //get => Task.Run(() => GetValueAsync<string>()).Result;
        //set => Task.Run(() => SetValueAsync(value)).Wait();
        get => GetValue<string>();
        set => SetValue(value);
    }
    public static string? IconPath
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
    public static string? OutputPath
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
    public static bool IsEnableOut
    {
        get => GetValue(true);
        set => SetValue(value);
    }
    public static bool IsMingw64
    {
        get => GetValue(true);
        set => SetValue(value);
    }

    public static bool IsaYesfdownload
    {
        get => GetValue(true);
        set => SetValue(value);
    }

    public static bool IsAdmin
    {
        get => GetValue(true);
        set => SetValue(value);
    }

    public static bool IsShowMemory
    {
        get => GetValue(true);
        set => SetValue(value);
    }

    public static bool IsSandalone
    {
        get => GetValue(true);
        set => SetValue(value);
    }
    public static bool IsRemoveOutput
    {
        get => GetValue(true);
        set => SetValue(value);
    }
    public static bool IsDisableConsole
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    public static bool IsOneFile
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    public static bool IsModule
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    public static bool IsFollowImports
    {
        get => GetValue(true);
        set => SetValue(value);
    }
    //模块设置
    public static string? IncludePackage
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public static string? NofollowImport
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public static string? IncludeModule
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public static bool IsQt
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    public static bool IsPyside6
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    public static bool IsTkinter
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    public static bool IsNumpy
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    public static bool IsTorch
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    public static bool IsTensorflow
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    public static bool IsAntibloat
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    public static bool IsMtiprocessing
    {
        get => GetValue(false);
        set => SetValue(value);
    }
    //文件信息
    public static string? CompanyName
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public static string? ProductName
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public static string? FileDescription
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public static string? FileVersion
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public static string? ProductVersion
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
    public static string? Copyright
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
    public static string? Trademarks
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
    //高级设置
    public static bool IsIncludeDataFile
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    public static bool IsIncludeDataDir
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    public static bool IsCopyDependency
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    public static string? DataFile
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public static string? DataDir
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public static string? DependencyDir
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
    //其他设置
    public static bool IsFullyCustom
    {
        get => GetValue(false);
        set => SetValue(value);
    }
    public static bool IsCustomSetting
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    public static string? CustomSetting
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    #endregion

    #region Setting Method
    private static T? ConvertFromString<T>(string? value, T? defaultValue = default)
    {
        if (value is null)
        {
            return defaultValue;
        }
        var converter = TypeDescriptor.GetConverter(typeof(T));
        if (converter == null)
        {
            return defaultValue;
        }
        return (T?)converter.ConvertFromString(value);
    }

    private static T? GetValue<T>(T? defaultValue = default, [CallerMemberName] string? key = null)
    {
        Debug.WriteLine($"Getting {key} with default {defaultValue}");
        return Task.Run(() => GetValueAsync(defaultValue, key)).Result;
    }

    private static void SetValue<T>(T value, [CallerMemberName] string? key = null)
    {
        Debug.WriteLine($"Setting {key} with {value}");
        Task.Run(() => SetValueAsync(value, key)).Wait();
    }

    public static async Task<T?> GetValueAsync<T>(T? defaultValue = default, [CallerMemberName] string? key = null)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return defaultValue;
        }
        var value = await _localSettingsService.ReadSettingAsync<string>(key).ConfigureAwait(false);
        return ConvertFromString(value, defaultValue);
    }

    public static async Task SetValueAsync<T>(T value, [CallerMemberName] string? key = null)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return;
        }
        await _localSettingsService.SaveSettingAsync(key, value);
    }

    public static async Task<StorageFile> OpenFilePickerAsync(string? fileTypeFilter = null)
    {
        // 初始化 FileOpenPicker
        FileOpenPicker picker = new FileOpenPicker
        {
            ViewMode = PickerViewMode.List,
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary // 建议的起始地点
        };
        if (!string.IsNullOrWhiteSpace(fileTypeFilter))
        {
            picker.FileTypeFilter.Add(fileTypeFilter);
            if (fileTypeFilter.Contains("ico"))
            {
                string[] fileTypes = { ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".tif", ".tiff" };
                foreach (var fileType in fileTypes)
                {
                    picker.FileTypeFilter.Add(fileType);
                }
            }
        }
        else
        {
            picker.FileTypeFilter.Add("*");
        }
        //picker.FileTypeFilter.Add(".*");
        //获取当前窗口句柄
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
        WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

        return await picker.PickSingleFileAsync();
    }

    public static async Task<StorageFolder> OpenFolderPickerAsync()
    {
        // 初始化 FolderPicker
        var picker = new Windows.Storage.Pickers.FolderPicker();
        picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
        picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
        //获取当前窗口句柄
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
        WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

        return await picker.PickSingleFolderAsync();
    }
    #endregion

    public static Dictionary<string, object?> GetAllSettings()
    {
        var settings = new Dictionary<string, object?>();

        // 获取所有公共静态属性
        var properties = typeof(AppConfig).GetProperties(BindingFlags.Public | BindingFlags.Static);
        foreach (var property in properties)
        {
            // 获取属性的名称和当前值
            settings[property.Name] = property.GetValue(null);
        }

        return settings;
    }

    #region Export and Import Methods

    public static async Task ExportSettingsAsync(string filePath)
    {
        // 创建设置对象
        var settings = GetAllSettings();

        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });

        // 使用传入的文件路径写入文件
        await File.WriteAllTextAsync(filePath, json);
    }

    public static async Task ImportSettingsAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("配置文件未找到", filePath);
        }

        var json = await File.ReadAllTextAsync(filePath);
        var settings = JsonSerializer.Deserialize<Dictionary<string, object?>>(json);

        // 根据解析的设置更新配置
        if (settings != null)
        {
            foreach (var kvp in settings)
            {
                try
                {
                    // 获取 AppConfig 类中对应的属性
                    var property = typeof(AppConfig).GetProperty(kvp.Key, BindingFlags.Public | BindingFlags.Static);
                    if (property != null && property.CanWrite)
                    {
                        // 检查值的实际类型
                        if (kvp.Value is JsonElement jsonElement)
                        {
                            // 使用 JsonSerializer 将 JsonElement 转回原始类型
                            var value = JsonSerializer.Deserialize(jsonElement.GetRawText(), property.PropertyType);
                            property.SetValue(null, value);
                        }
                        else
                        {
                            // 处理其他可能的类型
                            var value = Convert.ChangeType(kvp.Value, property.PropertyType);
                            property.SetValue(null, value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    new ToastContentBuilder()
                .AddText("错误键："+ kvp.Key +" 值：" + kvp.Value +"\n错误信息： " + ex.ToString())
                .SetToastDuration(ToastDuration.Long)
                .Show();
                }
            }
        }

    }



    #endregion
}
