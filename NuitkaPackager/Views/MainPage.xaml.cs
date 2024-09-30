using Microsoft.UI.Xaml;
using System.Diagnostics;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using NuitkaPackager.ViewModels;
using System.Text.RegularExpressions;
using Microsoft.UI.Xaml.Media;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI;

namespace NuitkaPackager.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }
    private Process? process;
    private int flag = 0;
    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
   

    private async void ExecuteButton_Click(object sender, RoutedEventArgs e)
    {
        if (process != null && !process.HasExited)
        {
            process.Kill();
        }
        flag = 0;
        process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "python", // 替换为你需要执行的命令
                //Arguments = "-m nuitka -help",
                Arguments = GetCommand.Get(),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };

        process.OutputDataReceived += (s, e) =>
        {
            DispatcherQueue.TryEnqueue(() =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    FormattingConsoleOutput(e.Data);
                }
            });
        };

        process.ErrorDataReceived += (s, e) =>
        {
            DispatcherQueue.TryEnqueue(() =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                   FormattingConsoleOutput(e.Data);
                }
            });
        };

        process.Start();
        ExecuteButton.IsEnabled = false;
        ClearButton.IsEnabled = false;
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        await Task.Run(() => process.WaitForExit());
        //复制文件
        if (flag == 0)
        {
            await CopyFiles();
        }
        else if (flag == -1)
        {
            new ToastContentBuilder()
        .AddText("停止执行")
        .AddAudio(new Uri("ms-winsoundevent:Notification.IM"))
        .SetToastDuration(ToastDuration.Long)
        .Show();
        }
        else
        {
            new ToastContentBuilder()
        .AddText("编译失败")
        .AddAudio(new Uri("ms-winsoundevent:Notification.IM"))
        .SetToastDuration(ToastDuration.Long)
        .Show();
        }
        ClearButton.IsEnabled = true;
        ExecuteButton.IsEnabled = true;
        FormattingConsoleOutput("--------------------------------------------------------------------\n");
    }
    private async Task CopyFiles()
    {
        var currentTime = "";
        if (Directory.Exists(AppConfig.DependencyDir))
        {
            var pyFileName = Path.GetFileNameWithoutExtension(AppConfig.PyFilePath);
            var outdir = "";
            
            if (AppConfig.IsEnableOut)
                outdir = AppConfig.OutputPath;
            else
                outdir = Path.GetDirectoryName(AppConfig.PyFilePath);
            var destinationDir = Path.Combine(outdir, pyFileName + ".dist");
            currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            FormattingConsoleOutput($"INFO: [{currentTime}] 开始复制依赖文件");
            await Task.Run(() => {
                CopyDirectory(AppConfig.DependencyDir, destinationDir);
            });
            currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            FormattingConsoleOutput($"INFO: [{currentTime}] 复制完成");
        }
        else
        {
            currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            FormattingConsoleOutput($"INFO: [{currentTime}] 依赖目录不存在");
        }
        new ToastContentBuilder()
        .AddText("编译执行完成")
        .AddAudio(new Uri("ms-winsoundevent:Notification.IM"))
        .SetToastDuration(ToastDuration.Long)
        .Show();
    }
    private void CopyDirectory(string sourceDir, string destinationDir)
    {
        // 创建目标目录
        Directory.CreateDirectory(destinationDir);
        // 复制文件
        foreach (var file in Directory.GetFiles(sourceDir))
        {
            var destFile = Path.Combine(destinationDir, Path.GetFileName(file));
            File.Copy(file, destFile, true);
        }

        // 递归复制子目录
        foreach (var dir in Directory.GetDirectories(sourceDir))
        {
            var destDir = Path.Combine(destinationDir, Path.GetFileName(dir));
            CopyDirectory(dir, destDir);
        }
    }
    private void FormattingConsoleOutput(string output)
    {
        if (!string.IsNullOrEmpty(output))
        {
            if (output.IndexOf("FATAL", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                flag++;
            }
            Paragraph text;
            if (output.IndexOf("ERROR", StringComparison.OrdinalIgnoreCase) >= 0) { 
                text = new Paragraph { Inlines = { new Run { Text = output, Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 60, 72)) } } };
                flag++;
            }
            else if (output.IndexOf("WARNING", StringComparison.OrdinalIgnoreCase) >= 0)
                text = new Paragraph { Inlines = { new Run { Text = output, Foreground = new SolidColorBrush(Color.FromArgb(255, 249, 241, 165)) } } };
            else
                text = new Paragraph { Inlines = { new Run { Text = output } } };
            
            TerminalOutput.Blocks.Add(text);
        }
        ContentArea.ScrollToVerticalOffset(ContentArea.ExtentHeight);
    }
    private void StopButton_Click(object sender, RoutedEventArgs e)
    {
        if (process != null && !process.HasExited)
        {
            process.Kill();
            //process.WaitForExit(); // 等待进程完全退出
            process.Dispose(); // 释放资源
            process = null; // 重置进程对象
            flag = -1;
            //ClearButton.IsEnabled = true;
        }
    }
    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        TerminalOutput.Blocks.Clear();
    }

}
