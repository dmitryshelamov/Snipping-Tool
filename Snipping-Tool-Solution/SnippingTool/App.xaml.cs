using System.Windows;
using SnippingTool.Models;
using SnippingTool.Models.Interfaces;

namespace SnippingTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ISettingsManager SettingsManager { get; private set; }

        //  Main entry point
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            SettingsManager = new SettingsManager(new SettingsRepository(new ConfigSettings()), new SettingsManagerHelper());
        }
    }
}
