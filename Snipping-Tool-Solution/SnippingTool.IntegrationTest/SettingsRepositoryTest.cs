using System;
using System.IO;
using System.Xml.Linq;
using NUnit.Framework;
using SnippingTool.Models;

namespace SnippingTool.IntegrationTest
{
    [TestFixture]
    class SettingsRepositoryTest
    {
        [Test]
        public void Save_SaveUserSettingsToFile_PropertiesSaved()
        {
            //  arrange
            string fileName = "UserSettings.xml";
            string pathToConfigFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            UserSettings settings = new UserSettings()
            {
                SaveDirectory = "TestSaveDirectory",
                ImageExtentions = "TestExtenstions"
            };

            //  act
            SettingsRepository settingsRepository = new SettingsRepository();
            settingsRepository.Save(settings);

            XDocument xDoc = XDocument.Load(pathToConfigFile);
            UserSettings expectedSettings = new UserSettings()
            {
                SaveDirectory = xDoc.Element("UserSettings").Element("SaveDirectory").Value,
                ImageExtentions = xDoc.Element("UserSettings").Element("ImageExtentions").Value
            };
            //  assert
            Assert.IsTrue(File.Exists(pathToConfigFile));
            Assert.AreEqual(expectedSettings.SaveDirectory, settings.SaveDirectory);
            Assert.AreEqual(expectedSettings.ImageExtentions, settings.ImageExtentions);
        }


        [Test]
        public void Load_SaveDirectoryProperty_PropertyLoaded()
        {
            //  arrange
            var expectedSaveDirectory = "ExpectedSaveDirectory";
            var expectedImageExtentions = "ExpectedImageExtentions";
            var xml =
                "<?xml version=\"1.0\"?>\n<UserSettings xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n" +
                "<SaveDirectory>" + expectedSaveDirectory + "</SaveDirectory>\n" +
                "<ImageExtentions>" + expectedImageExtentions + "</ImageExtentions>\n</UserSettings>";
            string fileName = "UserSettings.xml";
            string pathToConfigFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            File.WriteAllText(pathToConfigFile, xml);
            //  act
            SettingsRepository settingsRepository = new SettingsRepository();
            UserSettings userSettings = settingsRepository.Load();
            //  assert
            Assert.AreEqual(expectedSaveDirectory, userSettings.SaveDirectory);
            Assert.AreEqual(expectedImageExtentions, userSettings.ImageExtentions);
        }
    }
}
