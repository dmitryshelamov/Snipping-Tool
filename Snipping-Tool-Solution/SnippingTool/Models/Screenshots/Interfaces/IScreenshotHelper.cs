using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SnippingTool.Models.Settings;

namespace SnippingTool.Models.Screenshots.Interfaces
{
    public interface IScreenshotHelper
    {
        ImageFormat GetImageFormat(ImageExtensions imageExtensions);
        string GetExtension(ImageExtensions imageExtensions);
        BitmapEncoder GetEncoder(ImageExtensions imageExtensions);
        Bitmap ConvertImageSourceToBitmap(BitmapFrame image, BitmapEncoder bitmapEncoder);
        ImageSource ConverBitmapToImageSource(Bitmap bitmap);
    }
}