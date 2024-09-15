using System.Runtime.InteropServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.Notifications;
using NuitkaPackager.ViewModels;
using Windows.ApplicationModel.DataTransfer;
using WinUIEx.Messaging;

namespace NuitkaPackager.Views;

public sealed partial class OthersSettingsPage : Page
{
    public OthersSettingsViewModel ViewModel
    {
        get;
    }

    public OthersSettingsPage()
    {
        ViewModel = App.GetService<OthersSettingsViewModel>();
        InitializeComponent();
    }
    private async void ExportButton_Click(object sender, RoutedEventArgs e)
    {
        // 选择导出目录并执行导出
        var folder = await AppConfig.OpenFilePickerAsync(".json");
        if (folder != null)
        {
            await AppConfig.ExportSettingsAsync(folder.Path);
        }
    }

    private async void ImportButton_Click(object sender, RoutedEventArgs e)
    {
        // 选择导入文件并执行导入
        var file = await AppConfig.OpenFilePickerAsync(".json");
        if (file != null)
        {
            await AppConfig.ImportSettingsAsync(file.Path);
        }
    }
    #region 导出命令
    private async void ExportCommandButton_Click(object sender, RoutedEventArgs e)
    {
        var dataPackage = new DataPackage();
        // 设置文本内容
        dataPackage.SetText("python " + GetCommand.Get());
        // 设置剪贴板内容
        Clipboard.SetContent(dataPackage);
        // 使用记事本
        new ToastContentBuilder()
        .AddText("命令已复制到剪贴板")
        .AddAudio(new Uri("ms-winsoundevent:Notification.IM"))
        .Show();
        await SimulatePasteAsync();
    }

    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    public static extern void SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

    private const byte VK_CONTROL = 0x11;
    private const byte VK_V = 0x56;
    private const uint KEYEVENTF_KEYUP = 0x0002;

    // 模拟粘贴操作
    private async Task SimulatePasteAsync()
    {
        // 获取记事本的进程
        System.Diagnostics.Process.Start("cmd.exe", "/c start notepad.exe");
        await Task.Delay(500);
        var notepadProcesses = System.Diagnostics.Process.GetProcessesByName("notepad");
        if (notepadProcesses.Length == 0)
        {
            // 如果没有找到记事本进程，返回
            return;
        }
        // 获取第一个记事本进程的主窗口句柄
        var hWnd = notepadProcesses[0].MainWindowHandle;

        // 将记事本窗口设置为前景窗口
        SetForegroundWindow(hWnd);
        await Task.Delay(800);
        // 模拟按下 CTRL + V 进行粘贴
        keybd_event(VK_CONTROL, 0, 0, 0); // 按下 Ctrl
        keybd_event(VK_V, 0, 0, 0);       // 按下 V
        keybd_event(VK_V, 0, KEYEVENTF_KEYUP, 0); // 释放 V
        keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0); // 释放 Ctrl
    }
    #endregion


}
