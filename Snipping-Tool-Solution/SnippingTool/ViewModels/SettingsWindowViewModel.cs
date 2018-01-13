using System.Windows;
using GalaSoft.MvvmLight;
using SnippingTool.Models;
using SnippingTool.Models.Interfaces;

namespace SnippingTool.ViewModels
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        public UserSettings UserSettings { get; set; }

        public SettingsWindowViewModel(ISettingsManager settingsManager)
        {
            UserSettings = settingsManager.UserSettings;
        }

        public SettingsWindowViewModel() :
            this(((App)Application.Current).SettingsManager)
        {

        }
    }
}
