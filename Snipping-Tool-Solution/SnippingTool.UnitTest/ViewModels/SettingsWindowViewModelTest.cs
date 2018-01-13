using NSubstitute;
using NUnit.Framework;
using SnippingTool.Models;
using SnippingTool.Models.Interfaces;
using SnippingTool.ViewModels;

namespace SnippingTool.UnitTest.ViewModels
{
    [TestFixture]
    class SettingsWindowViewModelTest
    {
        [Test]
        public void CloseSettingsWindowCommand_ExecuteCommand_CloseSettingsWindowEventtTriggered()
        {
            //  arrange 
            bool eventFired = false;
            ISettingsManager settingsManager = Substitute.For<ISettingsManager>();
            SettingsWindowViewModel viewModel = new SettingsWindowViewModel(settingsManager);
            viewModel.CloseSettingsWindowEvent += (sender, args) => eventFired = true;
            //  act
            viewModel.CloseSettingsWindowCommand.Execute(null);
            //  assert
            Assert.IsTrue(eventFired);
        }

        [Test]
        public void SaveSettingsCommand_ExecuteCommand_SettiingsManagerReciveCall()
        {
            //  arrange
            var expectedDir = "ExpectedDir";
            var settingsManager = Substitute.For<ISettingsManager>();
            settingsManager.UserSettings.Returns(new UserSettings()
            {
                SaveDirectory = expectedDir,
                ImageExtension = ImageExtensions.Jpg
            });
            SettingsWindowViewModel viewModel = new SettingsWindowViewModel(settingsManager);
            //  act
            viewModel.SaveSettingsCommand.Execute(null);
            //  assert
            settingsManager.Received().SaveSettings();
            Assert.AreEqual(expectedDir, settingsManager.UserSettings.SaveDirectory);
            Assert.AreEqual(ImageExtensions.Jpg, settingsManager.UserSettings.ImageExtension);
        }
    }
}
