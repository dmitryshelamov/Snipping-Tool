using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

        public BitmapEncoder GetEncoder(ImageExtensions imageExtensions)
        {
            BitmapEncoder encoder = null;
            if (imageExtensions == ImageExtensions.Jpg)
                encoder = new JpegBitmapEncoder();
            if (imageExtensions == ImageExtensions.Png)
                encoder = new PngBitmapEncoder();
            return encoder;
        }

        public Bitmap ConvertImageSourceToBitmap(BitmapFrame image, BitmapEncoder bitmapEncoder)
        {
            Bitmap bitmap;
            bitmapEncoder.Frames.Add(BitmapFrame.Create(image));
            //  memory stream should be open
            //  https://stackoverflow.com/questions/336387/image-save-throws-a-gdi-exception-because-the-memory-stream-is-closed
            var memoryStream = new MemoryStream();
            bitmapEncoder.Save(memoryStream);
            memoryStream.Flush();
            bitmap = Image.FromStream(memoryStream) as Bitmap;
            return bitmap;
        }

        public ImageSource ConverBitmapToImageSource(Bitmap bitmap)
        {
            var source = new BitmapImage();
            source.BeginInit();
            source.StreamSource = new MemoryStream(BitmapToBytes(bitmap));
            source.EndInit();
            return source;
        }

        private static byte[] BitmapToBytes(Bitmap bitmap)
        {
            byte[] byteArray;
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Close();
                byteArray = stream.ToArray();
            }
            return byteArray;
        }
    }
}
