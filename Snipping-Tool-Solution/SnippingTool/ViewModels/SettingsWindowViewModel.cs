using System;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SnippingTool.Models;
using SnippingTool.Models.Interfaces;

namespace SnippingTool.ViewModels
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        private RelayCommand _closeSettingsWindowCommand;

        public event EventHandler CloseSettingsWindowEvent;

        public UserSettings UserSettings { get; set; }

        public SettingsWindowViewModel(ISettingsManager settingsManager)
        {
            UserSettings = settingsManager.UserSettings;
        }

        public SettingsWindowViewModel() :
            this(((App)Application.Current).SettingsManager)
        {

        }

        public RelayCommand CloseSettingsWindowCommand
        {
            get
            {
                return _closeSettingsWindowCommand ?? (_closeSettingsWindowCommand = new RelayCommand(() =>
                {
                    CloseSettingsWindowEvent?.Invoke(this, EventArgs.Empty);
                }));
            }
        }
    }
}
