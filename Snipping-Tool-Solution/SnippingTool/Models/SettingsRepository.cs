﻿using System.IO;
using System.Xml.Serialization;
using SnippingTool.Models.Interfaces;

namespace SnippingTool.Models
{
    public class SettingsRepository : ISettingsRepository
    {
        private IConfigSettings _configSettings;

        public SettingsRepository(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
        }

        /// <summary>
        /// Save current user settings to file
        /// </summary>
        /// <param name="userSettings"></param>
        public void Save(UserSettings userSettings)
        {
            using (FileStream fileStream = new FileStream(_configSettings.ConfigPath, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(UserSettings));
                serializer.Serialize(fileStream, userSettings);
            }
        }

        /// <summary>
        /// Load user settings from file
        /// </summary>
        /// <returns></returns>
        public UserSettings Load()
        {
            UserSettings userSettings;
            //  check if file exist
            if (!File.Exists(_configSettings.ConfigPath))
                return null;
            using (FileStream fileStream = new FileStream(_configSettings.ConfigPath, FileMode.Open))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(UserSettings));
                userSettings = deserializer.Deserialize(fileStream) as UserSettings;
            }
            return userSettings;
        }
    }
}
