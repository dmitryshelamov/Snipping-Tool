using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using SnippingTool.Models.Screenshots.Interfaces;
using SnippingTool.Models.Settings;

namespace SnippingTool.Models.Screenshots
{
    class ScreenshotCropped : IScreenshot
    {
        private readonly UserSettings _userSettings;
        private readonly IScreenshotHelper _screenshotHelper;
        private readonly BitmapFrame _bitmapFrame;
        public Bitmap ScreenshotBitmap { get; set; }

        public ScreenshotCropped(UserSettings userSettings, IScreenshotHelper screenshotHelper, BitmapFrame bitmapFrame)
        {
            _userSettings = userSettings;
            _screenshotHelper = screenshotHelper;
            _bitmapFrame = bitmapFrame;
        }

        public void TakeScreenshot()
        {
            BitmapEncoder encoder = _screenshotHelper.GetEncoder(_userSettings.ImageExtension);
            ScreenshotBitmap = _screenshotHelper.ConvertImageSourceToBitmap(_bitmapFrame, encoder);
        }

        public void SaveScreenshot()
        {
            var screenshotName = DateTime.Now.ToString("ddd, dd MMM yyyy, HH'-'mm'-'ss");
            var filePath = Path.Combine(_userSettings.SaveDirectory,
                screenshotName + _screenshotHelper.GetExtension(_userSettings.ImageExtension));
            var format = _screenshotHelper.GetImageFormat(_userSettings.ImageExtension);
            ScreenshotBitmap.Save(filePath, format);
        }
    }
}
