using System.Windows;
using SnippingTool.ViewModels;

namespace SnippingTool.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            var viewModel = DataContext as SettingsWindowViewModel;
            viewModel.CloseSettingsWindowEvent += (sender, args) => Close();
        }
    }
}
