using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CroppingImageLibrary;
using SnippingTool.ViewModels;

namespace SnippingTool.View
{
    /// <summary>
    /// Interaction logic for ScreenshotWindow.xaml
    /// </summary>
    public partial class ScreenshotWindow : Window
    {
        private CroppingAdorner _croppingAdorner;
        private readonly ScreenshotWindowViewModel _viewModel;

        public ScreenshotWindow()
        {
            InitializeComponent();

            _viewModel = DataContext as ScreenshotWindowViewModel;

            _viewModel.CloseSettingsEvent += (sender, args) => Close();

            Left = SystemParameters.VirtualScreenLeft;
            Top = SystemParameters.VirtualScreenTop;
            Width = SystemParameters.VirtualScreenWidth;
            Height = SystemParameters.VirtualScreenHeight;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            WindowState = WindowState.Normal;
            Topmost = true;

            //  use this for debugging
//            Width = 600;
//            Height = 600;
//            ResizeMode = ResizeMode.CanResize;
//            WindowStyle = WindowStyle.SingleBorderWindow;
        }

        private void RootGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _croppingAdorner.CaptureMouse();
            _croppingAdorner.MouseLeftButtonDownEventHandler(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(CanvasPanel);
            _croppingAdorner = new CroppingAdorner(CanvasPanel);
            layer.Add(_croppingAdorner);
            _croppingAdorner.OnRectangleDoubleClickEvent += (s, a) =>
            {
                _viewModel.SaveScreenshot.Execute(a.BitmapFrame);
                Close();
            };
        }
    }
}
