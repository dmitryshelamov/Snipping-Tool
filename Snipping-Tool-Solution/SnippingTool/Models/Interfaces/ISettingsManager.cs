namespace SnippingTool.Models.Interfaces
{
    public interface ISettingsManager
    {
        UserSettings UserSettings { get; }
        void SaveSettings();
        void LoadSettings();
    }
}