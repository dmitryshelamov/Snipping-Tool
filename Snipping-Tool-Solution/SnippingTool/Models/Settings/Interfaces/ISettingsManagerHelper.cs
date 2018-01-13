using SnippingTool.Models.KeyboardHook;

namespace SnippingTool.Models.Settings.Interfaces
{
    public interface ISettingsManagerHelper
    {
        string GetDefaultSaveDirectory();
        ImageExtensions GetDefaultFileExtension();
        HotKey GetDefaultTakeWholeScreenHotKey();
        HotKey GetDefaultTakeAndCropScreenHotKey();
    }
}