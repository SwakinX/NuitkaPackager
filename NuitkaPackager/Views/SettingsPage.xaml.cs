using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using NuitkaPackager.Helpers;
using NuitkaPackager.ViewModels;
using NuitkaPackager.Views;
using Windows.System;
using NuitkaPackager.Contracts.Services;
using CommunityToolkit.Mvvm.Messaging;

namespace NuitkaPackager.Views;

// TODO: Set the URL for your privacy policy by updating SettingsPage_PrivacyTermsLink.NavigateUri in Resources.resw.
public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {

        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();
        SettingFrame.Navigate(typeof(AboutSettingPage));
        //ViewModel.NavigationService.Frame = SettingFrame;
        //ViewModel.NavigationViewService.Initialize(NavigationViewControl);
    }

    private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        try
        {
            if (args.InvokedItemContainer?.IsSelected ?? false)
            {
                return;
            }
            else
            {
                var item = args.InvokedItemContainer as NavigationViewItem;
                if (item != null)
                {
                   
                    var type = item.Tag switch
                    {
                        nameof(AboutSettingPage) => typeof(AboutSettingPage),
                        nameof(BasicSettingsPage) => typeof(BasicSettingsPage),
                        nameof(FileInfoSettingsPage) => typeof(FileInfoSettingsPage),
                        nameof(IncludeSettingsPage) => typeof(IncludeSettingsPage),
                        nameof(AdvanceSettingsPage) => typeof(AdvanceSettingsPage),
                        nameof(OthersSettingsPage) => typeof(OthersSettingsPage),
                        _ => null,
                    };
                    if (type is not null)
                    {
                        SettingFrame.Navigate(type);
                    }
                }
            }
        }
        catch { }
    }

}
