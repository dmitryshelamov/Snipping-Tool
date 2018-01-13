using System;
using System.Drawing.Imaging;
using SnippingTool.Models.Screenshots.Interfaces;
using SnippingTool.Models.Settings;

namespace SnippingTool.Models.Screenshots
{
    class ScreenshotHelper : IScreenshotHelper
    {
        public ImageFormat GetImageFormat(ImageExtensions imageExtensions)
        {
            switch (imageExtensions)
            {
                case ImageExtensions.Jpg:
                    return ImageFormat.Jpeg;
                case ImageExtensions.Png:
                    return ImageFormat.Png;
            }
            throw new ArgumentException($"Current extension {imageExtensions} not mapped to ImageFormat");
        }

        public string GetExtension(ImageExtensions imageExtensions)
        {
            switch (imageExtensions)
            {
                case ImageExtensions.Jpg:
                    return ".jpeg";
                case ImageExtensions.Png:
                    return ".png";
            }
            throw new ArgumentException($"Current extension {imageExtensions} not mapped to file extension");
        }
    }
}
