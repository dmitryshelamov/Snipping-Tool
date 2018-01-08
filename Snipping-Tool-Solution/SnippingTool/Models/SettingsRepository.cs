using System;
using System.IO;
using System.Xml.Serialization;
using SnippingTool.Models.Interfaces;

namespace SnippingTool.Models
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly string _pathToConfigFile;
        private const string ConfigFile = "UserSettings.xml";

        public SettingsRepository()
        {
            _pathToConfigFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFile);
        }

        public void Save(UserSettings userSettings)
        {
            using (FileStream fileStream = new FileStream(_pathToConfigFile, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(UserSettings));
                serializer.Serialize(fileStream, userSettings);
            }
        }

        public UserSettings Load()
        {
            UserSettings userSettings;
            using (FileStream fileStream = new FileStream(_pathToConfigFile, FileMode.Open))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(UserSettings));
                userSettings = deserializer.Deserialize(fileStream) as UserSettings;
            }
            return userSettings;
        }
    }
}
