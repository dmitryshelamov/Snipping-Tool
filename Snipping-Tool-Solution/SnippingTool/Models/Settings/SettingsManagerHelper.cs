using System;
using System.Windows.Input;
using SnippingTool.Models.KeyboardHook;
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

        public HotKey GetDefaultTakeWholeScreenHotKey()
        {
            return new HotKey()
            {
                Key = Key.B,
                Mod1 = ModifierKeys.Shift,
                Mod2 = ModifierKeys.Control
            };
        }

        public HotKey GetDefaultTakeAndCropScreenHotKey()
        {
            return new HotKey()
            {
                Key = Key.Z,
                Mod1 = ModifierKeys.Shift,
                Mod2 = ModifierKeys.Control
            };
        }
    }
}
