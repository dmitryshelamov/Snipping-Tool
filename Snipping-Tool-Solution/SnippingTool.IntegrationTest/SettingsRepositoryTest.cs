using System;
using System.IO;
using System.Xml.Linq;
using NSubstitute;
using NUnit.Framework;
using SnippingTool.Models;
using SnippingTool.Models.Interfaces;

namespace SnippingTool.IntegrationTest
{
    [TestFixture]
    class SettingsRepositoryTest
    {
        [Test]
        public void Save_SaveUserSettingsToFile_SettingsSaved()
        {
            //  arrange
            IConfigSettings configSettings = Substitute.For<IConfigSettings>();
            configSettings.XmlName.Returns("TestUserSettings.xml");
            configSettings.ConfigPath.Returns(AppDomain.CurrentDomain.BaseDirectory);
            UserSettings settings = new UserSettings()
            {
                SaveDirectory = "TestSaveDirectory",
                ImageExtentions = "TestExtenstions"
            };

            //  act
            SettingsRepository settingsRepository = new SettingsRepository(configSettings);
            settingsRepository.Save(settings);

            XDocument xDoc = XDocument.Load(Path.Combine(configSettings.ConfigPath, configSettings.XmlName));
            UserSettings expectedSettings = new UserSettings()
            {
                SaveDirectory = xDoc.Element("UserSettings").Element("SaveDirectory").Value,
                ImageExtentions = xDoc.Element("UserSettings").Element("ImageExtentions").Value
            };
            //  assert
            Assert.IsTrue(File.Exists(Path.Combine(configSettings.ConfigPath, configSettings.XmlName)));
            Assert.AreEqual(expectedSettings.SaveDirectory, settings.SaveDirectory);
            Assert.AreEqual(expectedSettings.ImageExtentions, settings.ImageExtentions);
        }

        [Test]
        public void Load_LoadValidUserSettings_SettingsLoaded()
        {
            //  arrange
            var expectedSaveDirectory = "ExpectedSaveDirectory";
            var expectedImageExtentions = "ExpectedImageExtentions";
            var xml = "<?xml version=\"1.0\"?>" +
                       "<UserSettings xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/MLSchema\">" +
                       "<SaveDirectory>" + expectedSaveDirectory + "</SaveDirectory>" +
                       "<ImageExtentions>" + expectedImageExtentions + "</ImageExtentions>" +
                       "</UserSettings>";
            string fileName = "UserSettings.xml";
            string pathToConfigFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            File.WriteAllText(pathToConfigFile, xml);
            //  act
            SettingsRepository settingsRepository = new SettingsRepository(new ConfigSettings());
            UserSettings userSettings = settingsRepository.Load();
            //  assert
            Assert.AreEqual(expectedSaveDirectory, userSettings.SaveDirectory);
            Assert.AreEqual(expectedImageExtentions, userSettings.ImageExtentions);
        }

        [Test]
        public void Load_UserSettingsFileNotExist_ShouldReturnNull()
        {
            //  arrange
            IConfigSettings configSettings = Substitute.For<IConfigSettings>();
            configSettings.XmlName.Returns("TestUserSettings.xml");
            configSettings.ConfigPath.Returns(AppDomain.CurrentDomain.BaseDirectory);
            //  act
            SettingsRepository settingsRepository = new SettingsRepository(configSettings);
            UserSettings userSettings = settingsRepository.Load();
            //  assert
            Assert.IsNull(userSettings);
        }
    }
}
