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
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configSettings.XmlName);
            configSettings.ConfigPath.Returns(path);
            UserSettings settings = new UserSettings()
            {
                SaveDirectory = "TestSaveDirectory",
                ImageExtension = ImageExtensions.Jpg
            };

            //  act
            SettingsRepository settingsRepository = new SettingsRepository(configSettings);
            settingsRepository.Save(settings);

            XDocument xDoc = XDocument.Load(configSettings.ConfigPath);
            UserSettings expectedSettings = new UserSettings()
            {
                SaveDirectory = xDoc.Element("UserSettings").Element("SaveDirectory").Value,
                ImageExtension = (ImageExtensions)Enum.Parse(typeof(ImageExtensions), xDoc.Element("UserSettings").Element("ImageExtension").Value)
            };
            //  assert
            Assert.IsTrue(File.Exists(configSettings.ConfigPath));
            Assert.AreEqual(expectedSettings.SaveDirectory, settings.SaveDirectory);
            Assert.AreEqual(expectedSettings.ImageExtension, settings.ImageExtension);
        }

        [Test]
        public void Load_LoadValidUserSettings_SettingsLoaded()
        {
            //  arrange
            IConfigSettings configSettings = Substitute.For<IConfigSettings>();
            configSettings.XmlName.Returns("TestUserSettings.xml");
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configSettings.XmlName);
            configSettings.ConfigPath.Returns(path);
            var expectedSaveDirectory = "ExpectedSaveDirectory";
            var expectedImageExtentions = "ExpectedImageExtentions";
            var xml = "<?xml version=\"1.0\"?>" +
                       "<UserSettings xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/MLSchema\">" +
                       "<SaveDirectory>" + expectedSaveDirectory + "</SaveDirectory>" +
                       "<ImageExtentions>" + expectedImageExtentions + "</ImageExtentions>" +
                       "</UserSettings>";
            File.WriteAllText(configSettings.ConfigPath, xml);
            //  act
            SettingsRepository settingsRepository = new SettingsRepository(configSettings);
            UserSettings userSettings = settingsRepository.Load();
            //  assert
            Assert.AreEqual(expectedSaveDirectory, userSettings.SaveDirectory);
            Assert.AreEqual(expectedImageExtentions, userSettings.ImageExtension);
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
