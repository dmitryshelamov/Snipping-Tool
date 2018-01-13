namespace SnippingTool.Models.Settings.Interfaces
{
    public interface ISettingsManager
    {
        UserSettings UserSettings { get; }
        void SaveSettings();
        void LoadSettings();
    }
}