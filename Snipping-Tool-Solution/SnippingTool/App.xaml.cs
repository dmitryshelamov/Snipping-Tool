using System;
using System.Windows;
using SnippingTool.Models.KeyboardHook;
using SnippingTool.Models.Screenshots;
using SnippingTool.Models.Settings;
using SnippingTool.Models.Settings.Interfaces;
using SnippingTool.View;

namespace SnippingTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ISettingsManager SettingsManager { get; private set; }
        public HotKeyManager HotKeyManager { get; private set; }

        private ScreenshotWindow _screenshotWindow;

        //  Main entry point
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            SettingsManager = new SettingsManager(new SettingsRepository(new ConfigSettings()), new SettingsManagerHelper());

            HotKeyManager = new HotKeyManager(SettingsManager.UserSettings, new GlobalKeyboardHook());

            HotKeyManager.TakeWholeScreenEvent += (o, args) =>
            {
                Screenshot screenshot = new Screenshot(SettingsManager.UserSettings, new ScreenshotHelper());
                screenshot.TakeScreenshot();
                screenshot.SaveScreenshot();
            };

            HotKeyManager.TakeAndCropScreenEvent += (o, args) =>
            {
                if (_screenshotWindow == null)
                {
                    _screenshotWindow = new ScreenshotWindow();
                    _screenshotWindow.Show();
                    _screenshotWindow.Closing += ((sen, a) => _screenshotWindow = null);
                }
            };
        }
    }
}
