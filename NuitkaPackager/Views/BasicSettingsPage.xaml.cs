using Microsoft.UI.Xaml.Controls;

using NuitkaPackager.ViewModels;

namespace NuitkaPackager.Views;

public sealed partial class BasicSettingsPage : Page
{
    public BasicSettingsViewModel ViewModel
    {
        get;
    }

    public BasicSettingsPage()
    {
        ViewModel = App.GetService<BasicSettingsViewModel>();
        InitializeComponent();

    }
}
