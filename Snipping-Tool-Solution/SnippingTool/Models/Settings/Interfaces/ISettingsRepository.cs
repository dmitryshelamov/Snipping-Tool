namespace SnippingTool.Models.Settings.Interfaces
{
    public interface ISettingsRepository
    {
        void Save(UserSettings userSettings);
        UserSettings Load();
    }
}