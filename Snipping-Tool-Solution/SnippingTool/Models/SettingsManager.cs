using SnippingTool.Models.Interfaces;

namespace SnippingTool.Models
{
    public class SettingsManager : ISettingsManager
    {
        private readonly ISettingsRepository _settingsRepository;
        private readonly ISettingsManagerHelper _settingsManagerHelper;

        public UserSettings UserSettings { get; private set; }

        public SettingsManager(ISettingsRepository settingsRepository, ISettingsManagerHelper settingsManagerHelper)
        {
            _settingsManagerHelper = settingsManagerHelper;
            _settingsRepository = settingsRepository;

            UserSettings = new UserSettings();
            LoadSettings();
        }

        /// <summary>
        /// Save current user settings with ISettingsRepository
        /// </summary>
        public void SaveSettings()
        {
            var settingsToSave = new UserSettings()
            {
                SaveDirectory = UserSettings.SaveDirectory,
                ImageExtension = UserSettings.ImageExtension
            };
            _settingsRepository.Save(settingsToSave);
        }

        /// <summary>
        /// Load current user settings with ISettingsRepository
        /// </summary>
        public void LoadSettings()
        {
            var settingsFromRepo = _settingsRepository.Load();
            if (settingsFromRepo == null)
            {
                ResetSettings();
                SaveSettings();
            }
            else
            {
                UserSettings.SaveDirectory = settingsFromRepo.SaveDirectory;
                UserSettings.ImageExtension = settingsFromRepo.ImageExtension;
            }

        }

        /// <summary>
        /// Reset current user settings to default
        /// </summary>
        public void ResetSettings()
        {
            if (UserSettings == null)
                UserSettings = new UserSettings();
            UserSettings.SaveDirectory = _settingsManagerHelper.GetDefaultSaveDirectory();
            UserSettings.ImageExtension = _settingsManagerHelper.GetDefaultFileExtension();
        }
    }
}
