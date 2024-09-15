using Microsoft.UI.Xaml.Controls;

using NuitkaPackager.ViewModels;

namespace NuitkaPackager.Views;

public sealed partial class FileInfoSettingsPage : Page
{
    public FileInfoSettingsViewModel ViewModel
    {
        get;
    }

    public FileInfoSettingsPage()
    {
        ViewModel = App.GetService<FileInfoSettingsViewModel>();
        InitializeComponent();
    }
}
