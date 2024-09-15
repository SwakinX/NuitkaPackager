using System.Reflection;
using System.Windows.Input;
using Microsoft.UI.Xaml.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;

using NuitkaPackager.Contracts.Services;
using NuitkaPackager.Helpers;

using Windows.ApplicationModel;
using NuitkaPackager.Views;

namespace NuitkaPackager.ViewModels;

public partial class SettingsViewModel : ObservableRecipient
{

    [ObservableProperty]
    private string _versionDescription;

    [ObservableProperty]
    private object? selected;

    public INavigationService NavigationService
    {
        get;
    }

    public INavigationViewService NavigationViewService
    {
        get;
    }

    public SettingsViewModel(INavigationService navigationService, INavigationViewService navigationViewService)
    {
        
        //NavigationService = navigationService;
        //NavigationService.Navigated += OnNavigated;
        //NavigationViewService = navigationViewService;

        _versionDescription = GetVersionDescription(); 
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {

        var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem != null)
        {
            Selected = selectedItem;
        }
    }

    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;

            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{"AppDisplayName".GetLocalized()} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
}
