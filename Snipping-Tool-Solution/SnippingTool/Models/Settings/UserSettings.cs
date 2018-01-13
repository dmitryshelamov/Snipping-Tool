using System.ComponentModel;
using System.Runtime.CompilerServices;
using SnippingTool.Annotations;
using SnippingTool.Models.KeyboardHook;

namespace SnippingTool.Models.Settings
{
    /// <summary>
    /// Represent a user settings
    /// </summary>
    public class UserSettings : INotifyPropertyChanged
    {
        private string _saveDirectory;
        private ImageExtensions _imageExtension;
        private HotKey _takeWholeScreenHotKey;
        private HotKey _takeAndCropScreenHotKey;

        /// <summary>
        /// Directory where screenshot will be saved
        /// </summary>
        public string SaveDirectory
        {
            get => _saveDirectory;
            set
            {
                _saveDirectory = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Extension of saving screenshot
        /// </summary>
        public ImageExtensions ImageExtension
        {
            get => _imageExtension;
            set
            {
                _imageExtension = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Represent a user define hot key for taking full screen
        /// </summary>
        public HotKey TakeWholeScreenHotKey
        {
            get { return _takeWholeScreenHotKey; }
            set
            {
                _takeWholeScreenHotKey = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Represent a user define hot key for taking full screen and crop
        /// </summary>
        public HotKey TakeAndCropScreenHotKey
        {
            get { return _takeAndCropScreenHotKey; }
            set
            {
                _takeAndCropScreenHotKey = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
