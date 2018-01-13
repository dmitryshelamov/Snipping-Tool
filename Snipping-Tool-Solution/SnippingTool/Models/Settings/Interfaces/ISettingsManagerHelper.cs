namespace SnippingTool.Models.Settings.Interfaces
{
    public interface ISettingsManagerHelper
    {
        string GetDefaultSaveDirectory();
        ImageExtensions GetDefaultFileExtension();
    }
}