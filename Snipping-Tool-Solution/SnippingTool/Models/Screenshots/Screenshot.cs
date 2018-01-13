using System;
using System.Drawing;
using System.IO;
using SnippingTool.Models.Screenshots.Interfaces;
using SnippingTool.Models.Settings;

namespace SnippingTool.Models.Screenshots
{
    class Screenshot : IScreenshot
    {
        public Bitmap ScreenshotBitmap { get; set; }
        private readonly UserSettings _userSettings;
        private readonly IScreenshotHelper _screenshotHelper;

        public Screenshot(UserSettings userSettings, IScreenshotHelper screenshotHelper)
        {
            _userSettings = userSettings;
            _screenshotHelper = screenshotHelper;
        }

        public void TakeScreenshot()
        {
            Bitmap screenBitmap = new Bitmap((int)System.Windows.SystemParameters.VirtualScreenWidth, (int)System.Windows.SystemParameters.VirtualScreenHeight);
            Graphics graphics = Graphics.FromImage(screenBitmap);
            graphics.CopyFromScreen(0, 0, 0, 0, screenBitmap.Size, CopyPixelOperation.SourceCopy);
            ScreenshotBitmap = screenBitmap;
        }

        public void SaveScreenshot()
        {
            var screenshotName = DateTime.Now.ToString("ddd, dd MMM yyyy, HH'-'mm'-'ss");
            var format = _screenshotHelper.GetImageFormat(_userSettings.ImageExtension);
            ScreenshotBitmap.Save(Path.Combine(_userSettings.SaveDirectory, screenshotName + _screenshotHelper.GetExtension(_userSettings.ImageExtension)), format);
            ScreenshotBitmap.Dispose();
        }
    }
}
