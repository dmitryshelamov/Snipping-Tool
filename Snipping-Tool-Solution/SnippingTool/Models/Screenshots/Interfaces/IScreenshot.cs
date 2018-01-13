using System.Drawing;

namespace SnippingTool.Models.Screenshots.Interfaces
{
    public interface IScreenshot
    {
        Bitmap ScreenshotBitmap { get; set; }
        void TakeScreenshot();
        void SaveScreenshot();
    }
}