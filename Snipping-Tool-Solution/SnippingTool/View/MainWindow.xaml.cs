using System.Windows;
using SnippingTool.ViewModels;

namespace SnippingTool.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ScreenshotWindow _screenshotWindow;
        private readonly MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = DataContext as MainWindowViewModel;
            _viewModel.OpenSettingsEvent += (sender, args) =>
            {
                var settingsWindow = new SettingsWindow();
                settingsWindow.ShowInTaskbar = false;
                settingsWindow.Owner = this;
                settingsWindow.ShowDialog();
            };


            _viewModel.OpenScreenshotWindowEvent += (sender, args) =>
            {
                if (_screenshotWindow == null)
                {
                    _screenshotWindow = new ScreenshotWindow();
                    _screenshotWindow.Closing += (o, eventArgs) => { _screenshotWindow = null; };
                    _screenshotWindow.Show();
                }
            };

        }
    }
}
