using NSubstitute;
using NUnit.Framework;
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
    }
}
