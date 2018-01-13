using System.Windows;
using System.Windows.Controls;
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

        //  logic for updating tooltips
        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var button = sender as Button;

            if (button.Name == SettingsButton.Name)
                _viewModel.UpdateToolTip(ToolTipCategory.Settings);
            if (button.Name == TakeAndCropButton.Name)
                _viewModel.UpdateToolTip(ToolTipCategory.TakeAndCrop);
            if (button.Name == TakeWholeScreenButton.Name)
                _viewModel.UpdateToolTip(ToolTipCategory.TakeWholeScreen);
        }

        //  logic for updating tooltips
        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _viewModel.UpdateToolTip(ToolTipCategory.Common);
        }
    }
}
