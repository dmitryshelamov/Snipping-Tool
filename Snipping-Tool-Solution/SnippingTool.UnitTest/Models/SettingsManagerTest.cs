using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using SnippingTool.Models;
using SnippingTool.Models.Settings;
using SnippingTool.Models.Settings.Interfaces;

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
        public void LoadSettings_SettingsRepositoryReturnValidSettings_UserSettingsHaveValues()
        {
            //  arrange
            var saveDir = "TestSaveDirectory";
            var imgExt = ImageExtensions.Jpg;
            var settingsRepository = Substitute.For<ISettingsRepository>();
            var settingsHelper = Substitute.For<ISettingsManagerHelper>();
            settingsRepository.Load().Returns(new UserSettings()
            {
                SaveDirectory = saveDir,
                ImageExtension = imgExt
            });
            //  act
            ISettingsManager settingsManager = new SettingsManager(settingsRepository, settingsHelper);
            settingsManager.LoadSettings();
            //  assert
            Assert.NotNull(settingsManager.UserSettings);
            Assert.AreEqual(settingsManager.UserSettings.SaveDirectory, saveDir);
            Assert.AreEqual(settingsManager.UserSettings.ImageExtension, imgExt);
        }

        [Test]
        public void LoadSettings_SettingsRepositoryReturnNull_ShouldResetSettings()
        {
            //  arrange
            var saveDir = "TestSaveDirectory";
            var imgExt = ImageExtensions.Jpg;
            var settingsRepository = Substitute.For<ISettingsRepository>();
            var settingsHelper = Substitute.For<ISettingsManagerHelper>();
            settingsHelper.GetDefaultSaveDirectory().Returns(saveDir);
            settingsHelper.GetDefaultFileExtension().Returns(imgExt);
            settingsRepository.Load().ReturnsNull();
            //  act
            ISettingsManager settingsManager = new SettingsManager(settingsRepository, settingsHelper);
            settingsManager.LoadSettings();
            //  assert
            Assert.NotNull(settingsManager.UserSettings);
            Assert.AreEqual(settingsManager.UserSettings.SaveDirectory, saveDir);
            Assert.AreEqual(settingsManager.UserSettings.ImageExtension, imgExt);
        }

        [Test]
        public void SaveSettings_CallMethod_SettingsRepositoryRecivedCall()
        {
            //  arrange
            var saveDir = "TestSaveDirectory";
            var imgExt = ImageExtensions.Jpg;
            var settingsRepository = Substitute.For<ISettingsRepository>();
            var settingsHelper = Substitute.For<ISettingsManagerHelper>();
            //  act
            ISettingsManager settingsManager = new SettingsManager(settingsRepository, settingsHelper);
            settingsManager.UserSettings.SaveDirectory = saveDir;
            settingsManager.UserSettings.ImageExtension = imgExt;
            settingsManager.SaveSettings();
            //  assert
            settingsRepository.Received().Save(Arg.Is<UserSettings>(x => x.SaveDirectory == saveDir && x.ImageExtension == imgExt));
        }
    }
}
