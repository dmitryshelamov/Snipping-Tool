using System.Windows;
using SnippingTool.ViewModels;

namespace SnippingTool.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var viewModel = DataContext as MainWindowViewModel;
            viewModel.OpenSettingsEvent += (sender, args) =>
            {
                var settingsWindow = new SettingsWindow();
                settingsWindow.ShowInTaskbar = false;
                settingsWindow.Owner = this;
                settingsWindow.ShowDialog();
            };

        }
    }
}
