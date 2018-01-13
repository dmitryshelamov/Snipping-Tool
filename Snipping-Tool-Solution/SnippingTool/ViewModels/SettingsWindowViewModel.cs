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
        private RelayCommand _resetSettingsCommand;

        public event EventHandler CloseSettingsWindowEvent;

        public UserSettings UserSettings { get; set; }
        private readonly ISettingsManager _settingsManager;
        private readonly ISettingsManagerHelper _settingsManagerHelper;


        public SettingsWindowViewModel(ISettingsManager settingsManager, ISettingsManagerHelper settingsManagerHelper)
        {
            UserSettings = new UserSettings();
            _settingsManager = settingsManager;
            _settingsManagerHelper = settingsManagerHelper;
            UpdateViewUserSettings();

        }

        public SettingsWindowViewModel() :
            this(((App)Application.Current).SettingsManager, new SettingsManagerHelper())
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
                    _settingsManager.UserSettings.SaveDirectory = UserSettings.SaveDirectory;
                    _settingsManager.UserSettings.ImageExtension = UserSettings.ImageExtension;
                    _settingsManager.SaveSettings();
                }));
            }
        }

        public RelayCommand ResetSettingsCommand
        {
            get
            {
                return _resetSettingsCommand ?? (_resetSettingsCommand = new RelayCommand(() =>
                {
                    UserSettings.SaveDirectory = _settingsManagerHelper.GetDefaultSaveDirectory();
                    UserSettings.ImageExtension = _settingsManagerHelper.GetDefaultFileExtension();
                }));
            }
        }

        private void UpdateViewUserSettings()
        {
            UserSettings.SaveDirectory = _settingsManager.UserSettings.SaveDirectory;
            UserSettings.ImageExtension = _settingsManager.UserSettings.ImageExtension;
        }
    }
}
