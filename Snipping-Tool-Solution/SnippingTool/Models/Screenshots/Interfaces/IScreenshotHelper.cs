using System.Drawing.Imaging;
using SnippingTool.Models.Settings;

namespace SnippingTool.Models.Screenshots.Interfaces
{
    public interface IScreenshotHelper
    {
        ImageFormat GetImageFormat(ImageExtensions imageExtensions);
        string GetExtension(ImageExtensions imageExtensions);
    }
}