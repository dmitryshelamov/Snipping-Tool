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
        private RelayCommand _openScreenshotWindowCommand;

        public event EventHandler OpenSettingsEvent;
        public event EventHandler OpenScreenshotWindowEvent;

        private readonly ISettingsManager _settingsManager;
        private string _toolTip;

        public string ToolTip
        {
            get { return _toolTip; }
            set
            {
                _toolTip = value;
                RaisePropertyChanged();
            }
        }


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

        public RelayCommand OpenScreenshotWindowCommand
        {
            get
            {
                return _openScreenshotWindowCommand ?? (_openScreenshotWindowCommand = new RelayCommand(() =>
                {
                    OpenScreenshotWindowEvent?.Invoke(this, EventArgs.Empty);
                }));
            }
        }

        public void UpdateToolTip(ToolTipCategory toolTip)
        {
            switch (toolTip)
            {
                case ToolTipCategory.Common:
                    ToolTip = "A simple program for taking screenshot and crop on the fly.";
                    break;
                case ToolTipCategory.Settings:
                    ToolTip = "User settings. You can choose, where to save, which format.";
                    break;
                case ToolTipCategory.TakeWholeScreen:
                    ToolTip = "Take a screenshot of whole screen and save to directory and prefered format. You can change it in settings window.";
                    break;
                case ToolTipCategory.TakeAndCrop:
                    ToolTip = "Take a screenshot of whole screen and give you abiliy to crop it. Double click inside cropped area will save cropped screenshot. Press 'Escape' button to cancel.";
                    break;
            }
        }

    }
}
