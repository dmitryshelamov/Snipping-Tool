using System;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SnippingTool.Models.Settings;
using SnippingTool.Models.Settings.Interfaces;

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

        public HotKeyHelper TakeWholeScreenHotKey { get; set; }
        public HotKeyHelper TakeAndCropScreenHotKey { get; set; }


        public SettingsWindowViewModel(ISettingsManager settingsManager, ISettingsManagerHelper settingsManagerHelper)
        {
            TakeWholeScreenHotKey = new HotKeyHelper();
            TakeAndCropScreenHotKey = new HotKeyHelper();
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

                    _settingsManager.UserSettings.TakeWholeScreenHotKey.Key = TakeWholeScreenHotKey.Key;
                    _settingsManager.UserSettings.TakeWholeScreenHotKey.Mod1 = TakeWholeScreenHotKey.Mod1;
                    _settingsManager.UserSettings.TakeWholeScreenHotKey.Mod2 = TakeWholeScreenHotKey.Mod2;

                    _settingsManager.UserSettings.TakeAndCropScreenHotKey.Key = TakeAndCropScreenHotKey.Key;
                    _settingsManager.UserSettings.TakeAndCropScreenHotKey.Mod1 = TakeAndCropScreenHotKey.Mod1;
                    _settingsManager.UserSettings.TakeAndCropScreenHotKey.Mod2 = TakeAndCropScreenHotKey.Mod2;

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

                    TakeWholeScreenHotKey.Key = _settingsManagerHelper.GetDefaultTakeWholeScreenHotKey().Key;
                    TakeWholeScreenHotKey.Mod1 = _settingsManagerHelper.GetDefaultTakeWholeScreenHotKey().Mod1;
                    TakeWholeScreenHotKey.Mod2 = _settingsManagerHelper.GetDefaultTakeWholeScreenHotKey().Mod2;

                    TakeAndCropScreenHotKey.Key = _settingsManagerHelper.GetDefaultTakeAndCropScreenHotKey().Key;
                    TakeAndCropScreenHotKey.Mod1 = _settingsManagerHelper.GetDefaultTakeAndCropScreenHotKey().Mod1;
                    TakeAndCropScreenHotKey.Mod2 = _settingsManagerHelper.GetDefaultTakeAndCropScreenHotKey().Mod2;
                }));
            }
        }

        private void UpdateViewUserSettings()
        {
            UserSettings.SaveDirectory = _settingsManager.UserSettings.SaveDirectory;
            UserSettings.ImageExtension = _settingsManager.UserSettings.ImageExtension;

            TakeWholeScreenHotKey.Key = _settingsManager.UserSettings.TakeWholeScreenHotKey.Key;
            TakeWholeScreenHotKey.Mod1 = _settingsManager.UserSettings.TakeWholeScreenHotKey.Mod1;
            TakeWholeScreenHotKey.Mod2 = _settingsManager.UserSettings.TakeWholeScreenHotKey.Mod2;

            TakeAndCropScreenHotKey.Key = _settingsManager.UserSettings.TakeAndCropScreenHotKey.Key;
            TakeAndCropScreenHotKey.Mod1 = _settingsManager.UserSettings.TakeAndCropScreenHotKey.Mod1;
            TakeAndCropScreenHotKey.Mod2 = _settingsManager.UserSettings.TakeAndCropScreenHotKey.Mod2;
        }
    }
}
