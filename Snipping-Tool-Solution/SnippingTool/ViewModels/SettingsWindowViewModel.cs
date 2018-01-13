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
        private RelayCommand _saveSettingsCommand;

        public event EventHandler CloseSettingsWindowEvent;

        public UserSettings UserSettings { get; set; }
        private readonly ISettingsManager _settingsManager;

        public SettingsWindowViewModel(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
            UserSettings = _settingsManager.UserSettings;
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

        public RelayCommand SaveSettingsCommand
        {
            get
            {
                return _saveSettingsCommand ?? (_saveSettingsCommand = new RelayCommand(() =>
                {
                    //_settingsManager.UserSettings.SaveDirectory = UserSettings.SaveDirectory;
                    //_settingsManager.UserSettings.ImageExtension = UserSettings.ImageExtension;

                    _settingsManager.SaveSettings();
                }));
            }
        }
    }
}
