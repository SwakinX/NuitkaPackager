using Microsoft.UI.Xaml.Controls;

using NuitkaPackager.ViewModels;

namespace NuitkaPackager.Views;

public sealed partial class AdvanceSettingsPage : Page
{
    public AdvanceSettingsViewModel ViewModel
    {
        get;
    }

    public AdvanceSettingsPage()
    {
        ViewModel = App.GetService<AdvanceSettingsViewModel>();
        InitializeComponent();
    }
}
