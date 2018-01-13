using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace SnippingTool.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private RelayCommand _openSettingsWindowCommand;
        public event EventHandler OpenSettingsEvent;

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

    }
}
