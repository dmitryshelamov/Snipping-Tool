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

            if (IsUserSettingsValid() == false)
            {
                ResetSettings();
                SaveSettings();
            }
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
            if (settingsFromRepo == null)
            {
                ResetSettings();
                SaveSettings();
            }
            else
            {
                UserSettings.SaveDirectory = settingsFromRepo.SaveDirectory;
                UserSettings.ImageExtentions = settingsFromRepo.ImageExtentions;
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
            UserSettings.ImageExtentions = _settingsManagerHelper.GetDefaultFileExtension();
        }

        /// <summary>
        /// Check current user settings for null values
        /// </summary>
        /// <returns></returns>
        private bool IsUserSettingsValid()
        {
            if (string.IsNullOrEmpty(UserSettings.SaveDirectory))
                return false;
            if (string.IsNullOrEmpty(UserSettings.ImageExtentions))
                return false;
            return true;
        }
    }
}
