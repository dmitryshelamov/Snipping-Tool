using NUnit.Framework;
using SnippingTool.ViewModels;

namespace SnippingTool.UnitTest.ViewModels
{
    [TestFixture]
    class MainWindowViewModelTest
    {
        [Test]
        public void OpenSettingsCommand_ExecuteCommand_OpenSettingsEventTriggered()
        {
            //  arrange 
            bool eventFired = false;
            MainWindowViewModel viewModel = new MainWindowViewModel();
            viewModel.OpenSettingsEvent += (sender, args) => eventFired = true;
            //  act
            viewModel.OpenSettingsWindowCommand.Execute(null);
            //  assert
            Assert.IsTrue(eventFired);
        }

    }
}
