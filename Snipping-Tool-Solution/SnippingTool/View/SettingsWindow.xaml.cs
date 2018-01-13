using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        // some rules for key textbox
        private string PreviewKeyUpHandle(string currentText, KeyEventArgs e)
        {
            var resultText = currentText;
            if (e.Key == Key.Back || e.Key == Key.Escape)
                return string.Empty;
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);

            //  filter
            if ((key == Key.LeftShift || key == Key.RightShift) ||
                (key == Key.LeftCtrl || key == Key.RightCtrl) ||
                (key == Key.LeftAlt || key == Key.RightAlt))
            {
                return currentText;
            }

            resultText = key.ToString();
            return resultText;
        }

        private void TakeWholeScreenHotKeyTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            string currentText = ((TextBox)sender).Text;
            TakeWholeScreenHotKey.Text = PreviewKeyUpHandle(currentText, e);
            e.Handled = true;
        }

        private void TakeAndCropScreenHotKeyTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            string currentText = ((TextBox)sender).Text;
            TakeAndCropScreenHotKey.Text = PreviewKeyUpHandle(currentText, e);
            e.Handled = true;
        }
    }
}
