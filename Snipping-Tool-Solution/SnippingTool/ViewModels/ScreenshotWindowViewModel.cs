using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SnippingTool.Models.Screenshots;
using SnippingTool.Models.Screenshots.Interfaces;
using SnippingTool.Models.Settings.Interfaces;

namespace SnippingTool.ViewModels
{
    class ScreenshotWindowViewModel : ViewModelBase
    {
        public ImageSource ScreenImageSource { get; set; }

        private RelayCommand<BitmapFrame> _saveScreenshot;


        private readonly ISettingsManager _settingsManager;
        private readonly IScreenshotHelper _screenshotHelper;

        public ScreenshotWindowViewModel(ISettingsManager settingsManager, IScreenshotHelper screenshotHelper)
        {
            _settingsManager = settingsManager;
            _screenshotHelper = screenshotHelper;
            IScreenshot wholeScreenshot = new Screenshot(_settingsManager.UserSettings, new ScreenshotHelper());
            wholeScreenshot.TakeScreenshot();
            ScreenImageSource = _screenshotHelper.ConverBitmapToImageSource(wholeScreenshot.ScreenshotBitmap);
        }

        public ScreenshotWindowViewModel() :
            this(((App)Application.Current).SettingsManager, new ScreenshotHelper())
        {

        }

        public RelayCommand<BitmapFrame> SaveScreenshot
        {
            get
            {
                return _saveScreenshot ?? (_saveScreenshot = new RelayCommand<BitmapFrame>((bitmapFrame) =>
                {
                    ScreenshotCropped screenshot = new ScreenshotCropped(_settingsManager.UserSettings, new ScreenshotHelper(), bitmapFrame);
                    screenshot.TakeScreenshot();
                    screenshot.SaveScreenshot();
                }));
            }
        }
    }
}
