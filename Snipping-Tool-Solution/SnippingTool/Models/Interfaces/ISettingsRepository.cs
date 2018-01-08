namespace SnippingTool.Models.Interfaces
{
    public interface ISettingsRepository
    {
        void Save(UserSettings userSettings);
        UserSettings Load();
    }
}