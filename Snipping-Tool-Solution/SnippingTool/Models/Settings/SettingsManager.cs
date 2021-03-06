﻿using SnippingTool.Models.Settings.Interfaces;

namespace SnippingTool.Models.Settings
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
                ImageExtension = UserSettings.ImageExtension,
                TakeWholeScreenHotKey = UserSettings.TakeWholeScreenHotKey,
                TakeAndCropScreenHotKey = UserSettings.TakeAndCropScreenHotKey
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
                UserSettings.TakeWholeScreenHotKey = settingsFromRepo.TakeWholeScreenHotKey;
                UserSettings.TakeAndCropScreenHotKey = settingsFromRepo.TakeAndCropScreenHotKey;
            }
        }

        /// <summary>
        /// Reset current user settings to default
        /// </summary>
        private void ResetSettings()
        {
            UserSettings.SaveDirectory = _settingsManagerHelper.GetDefaultSaveDirectory();
            UserSettings.ImageExtension = _settingsManagerHelper.GetDefaultFileExtension();
            UserSettings.TakeWholeScreenHotKey = _settingsManagerHelper.GetDefaultTakeWholeScreenHotKey();
            UserSettings.TakeAndCropScreenHotKey = _settingsManagerHelper.GetDefaultTakeAndCropScreenHotKey();
        }
    }
}
