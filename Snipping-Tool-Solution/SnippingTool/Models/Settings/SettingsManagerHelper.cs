using System;
using SnippingTool.Models.Settings.Interfaces;

namespace SnippingTool.Models.Settings
{
    class SettingsManagerHelper : ISettingsManagerHelper
    {
        /// <summary>
        /// Get path to "MyPictures" folder
        /// </summary>
        /// <returns>Path to ""MMyPictures" folder</returns>
        public string GetDefaultSaveDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        }

        /// <summary>
        /// Get default file extensions
        /// </summary>
        /// <returns>Path to ""MMyPictures" folder</returns>
        public ImageExtensions GetDefaultFileExtension()
        {
            return ImageExtensions.Jpg;
        }
    }
}
