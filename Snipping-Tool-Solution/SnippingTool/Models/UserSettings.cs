using System.ComponentModel;
using System.Runtime.CompilerServices;
using SnippingTool.Annotations;

namespace SnippingTool.Models
{
    /// <summary>
    /// Represent a user settings
    /// </summary>
    public class UserSettings : INotifyPropertyChanged
    {
        private string _saveDirectory;
        private ImageExtensions _imageExtension;

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
