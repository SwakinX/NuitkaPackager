using Microsoft.UI.Xaml.Controls;

using NuitkaPackager.ViewModels;
using Windows.ApplicationModel.DataTransfer;
using System.Runtime.InteropServices;

namespace NuitkaPackager.Views;

public sealed partial class AboutSettingPage : Page
{
    public AboutSettingViewModel ViewModel
    {
        get;
    }

    public AboutSettingPage()
    {
        ViewModel = App.GetService<AboutSettingViewModel>();
        InitializeComponent();
    }

    
}                                     
