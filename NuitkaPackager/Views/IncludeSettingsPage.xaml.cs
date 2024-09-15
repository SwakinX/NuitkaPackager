using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using NuitkaPackager.ViewModels;

namespace NuitkaPackager.Views;

public sealed partial class IncludeSettingsPage : Page
{
    public IncludeSettingsViewModel ViewModel
    {
        get;
    }
    //public ObservableCollection<string> CheckBoxItems
    //{
    //    get; set;
    //}
    public IncludeSettingsPage()
    {
        ViewModel = App.GetService<IncludeSettingsViewModel>();
        InitializeComponent();
    }
    //public ObservableCollection<CheckBox> CheckBoxItems
    //{
    //    get;
    //} = new ObservableCollection<CheckBox>
    //{
    //    new() { Content = "qt-plugins", IsChecked = AppConfig.IsQt },
    //    new() { Content = "pyside6", IsChecked = AppConfig.IsPyside6 },
    //    new() { Content = "tk-inter", IsChecked = AppConfig.IsTkinter },
    //    new() { Content = "numpy", IsChecked = AppConfig.IsNumpy },
    //    new() { Content = "torch", IsChecked = AppConfig.IsTorch },
    //    new() { Content = "tensorflow", IsChecked = AppConfig.IsTensorflow },
    //    new() { Content = "anti-bloat", IsChecked = AppConfig.IsAntibloat },
    //    new() { Content = "multiprocessing", IsChecked = AppConfig.IsMtiprocessing }
    //};

}
