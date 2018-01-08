using SnippingTool.Models.Interfaces;

namespace SnippingTool.Models
{
    public class SettingsManager : ISettingsManager
    {
        private readonly ISettingsRepository _settingsRepository;
        private readonly ISettingsManagerHelper _settingsManagerHelper;

        public UserSettings UserSettings { get; }

        public SettingsManager(ISettingsRepository settingsRepository, ISettingsManagerHelper settingsManagerHelper)
        {
            _settingsManagerHelper = settingsManagerHelper;
            _settingsRepository = settingsRepository;
            UserSettings = new UserSettings();
        }

        /// <summary>
        /// Save current user settings with ISettingsRepository
        /// </summary>
        public void SaveSettings()
        {
            var settingsToSave = new UserSettings()
            {
                SaveDirectory = UserSettings.SaveDirectory,
                ImageExtentions = UserSettings.ImageExtentions
            };
            _settingsRepository.Save(settingsToSave);
        }

        /// <summary>
        /// Load current user settings with ISettingsRepository
        /// </summary>
        public void LoadSettings()
        {
            var settingsFromRepo = _settingsRepository.Load();
            UserSettings.SaveDirectory = settingsFromRepo.SaveDirectory;
            UserSettings.ImageExtentions = settingsFromRepo.ImageExtentions;
        }

        /// <summary>
        /// Reset current user settings to default
        /// </summary>
        public void ResetSettings()
        {
            UserSettings.SaveDirectory = _settingsManagerHelper.GetDefaultSaveDirectory();
            UserSettings.ImageExtentions = "jpg";
        }
    }
}
