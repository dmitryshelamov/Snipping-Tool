using NSubstitute;
using NUnit.Framework;
using SnippingTool.Models;
using SnippingTool.Models.Interfaces;

namespace SnippingTool.UnitTest.Models
{
    [TestFixture]
    class SettingsManagerTest
    {
        [Test]
        public void SettingsManager_CallConstructor_UserSettingsNotNull()
        {
            //  arrange
            var settingsRepository = Substitute.For<ISettingsRepository>();
            var settingsHelper = Substitute.For<ISettingsManagerHelper>();
            //  act
            ISettingsManager settingsManager = new SettingsManager(settingsRepository, settingsHelper);
            //  assert
            Assert.NotNull(settingsManager.UserSettings);
        }

        [Test]
        public void LoadSettings_CallMethod_UserSettingsHaveValues()
        {
            //  arrange
            var saveDir = "TestSaveDirectory";
            var imgExt = "TestImageExtensions";
            var settingsRepository = Substitute.For<ISettingsRepository>();
            var settingsHelper = Substitute.For<ISettingsManagerHelper>();
            settingsRepository.Load().Returns(new UserSettings()
            {
                SaveDirectory = saveDir,
                ImageExtentions = imgExt
            });
            //  act
            ISettingsManager settingsManager = new SettingsManager(settingsRepository, settingsHelper);
            settingsManager.LoadSettings();
            //  assert
            Assert.NotNull(settingsManager.UserSettings);
            Assert.AreEqual(settingsManager.UserSettings.SaveDirectory, saveDir);
            Assert.AreEqual(settingsManager.UserSettings.ImageExtentions, imgExt);
        }

        [Test]
        public void SaveSettings_CallMethod_SettingsRepositoryRecivedCall()
        {
            //  arrange
            var saveDir = "TestSaveDirectory";
            var imgExt = "TestImageExtensions";
            var settingsRepository = Substitute.For<ISettingsRepository>();
            var settingsHelper = Substitute.For<ISettingsManagerHelper>();
            //  act
            ISettingsManager settingsManager = new SettingsManager(settingsRepository, settingsHelper);
            settingsManager.UserSettings.SaveDirectory = saveDir;
            settingsManager.UserSettings.ImageExtentions = imgExt;
            settingsManager.SaveSettings();
            //  arrange
            settingsRepository.Received().Save(Arg.Is<UserSettings>(x => x.SaveDirectory == saveDir && x.ImageExtentions == imgExt));
        }

        [Test]
        public void ResetSettings_CallMethod_UserSettingsResetToDefault()
        {
            //  arrange
            var saveDir = "TestSaveDirectory";
            var imgExt = "jpg";
            var settingsRepository = Substitute.For<ISettingsRepository>();
            var settingsHelper = Substitute.For<ISettingsManagerHelper>();
            settingsHelper.GetDefaultSaveDirectory().Returns(saveDir);
            ISettingsManager settingsManager = new SettingsManager(settingsRepository, settingsHelper);
            //  act
            settingsManager.ResetSettings();
            //  assert
            Assert.AreEqual(saveDir, settingsManager.UserSettings.SaveDirectory);
            Assert.AreEqual(imgExt, settingsManager.UserSettings.ImageExtentions);
        }
    }
}
