using System;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SnippingTool.Models.Screenshots;
using SnippingTool.Models.Screenshots.Interfaces;
using SnippingTool.Models.Settings.Interfaces;

namespace SnippingTool.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private RelayCommand _openSettingsWindowCommand;
        private RelayCommand _takeScreenshotCommand;

        public event EventHandler OpenSettingsEvent;

        private readonly ISettingsManager _settingsManager;

        public MainWindowViewModel(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        public MainWindowViewModel() :
            this(((App)Application.Current).SettingsManager)
        {

        }

        public RelayCommand OpenSettingsWindowCommand
        {
            get
            {
                return _openSettingsWindowCommand ?? (_openSettingsWindowCommand = new RelayCommand(() =>
                {
                    OpenSettingsEvent?.Invoke(this, EventArgs.Empty);
                }));
            }
        }

        public RelayCommand TakeScreenshotCommand
        {
            get
            {
                return _takeScreenshotCommand ?? (_takeScreenshotCommand = new RelayCommand(() =>
                {
                    IScreenshot screenshot = new Screenshot(_settingsManager.UserSettings, new ScreenshotHelper());
                    screenshot.TakeScreenshot();
                    screenshot.SaveScreenshot();
                }));
            }
        }

    }
}
