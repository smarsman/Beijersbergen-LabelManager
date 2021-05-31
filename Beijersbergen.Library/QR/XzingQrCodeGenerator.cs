using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using ZXing;
using ZXing.Datamatrix.Encoder;
using ZXing.QrCode;
using ZXing.QrCode.Internal;
using ZXing.Rendering;

namespace Beijersbergen.Library.QR
{
    public class XzingQrCodeGenerator : IQrCodeGenerator
    {
        public Bitmap GenerateQrCode(QrCodeOptions options)
        {
            try
            {
                //var bitmap = GenerateQrCodeDefault(options);
                var bitmap = Meh(options);
                //var bitmap = GenerateQrCodeWithoutMargin(options);

                return bitmap;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to generate QR code: {ex.Message}");
            }
        }


        /// <summary>
        /// Generates a barcode with the default margins (whitespace at the edges)
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        private Bitmap GenerateQrCodeDefault(QrCodeOptions options)
        {
            var encodingOptions = new QrCodeEncodingOptions()
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 500, //options.Width,
                Height = 500 //options.Height,
                //Margin = 1,
                //ErrorCorrection = ErrorCorrectionLevel.L
                //QrVersion = 10
            };

            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = encodingOptions
            };

            var bitmap = writer.Write(options.Content);

            // Resize
            //var resizd = ResizeImage(bitmap, options.Width, options.Height);
            //return resized;
            
            return bitmap;
        }

        private Bitmap Meh(QrCodeOptions options)
        {
            
            var writer = new BarcodeWriter()
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Margin = 0,
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    //ErrorCorrection = ErrorCorrectionLevel.L,
                    Width = 200,
                    Height = 200
                }
            };

            var bitmap = writer.Write(options.Content);

            return bitmap;
        }

        public void Resize(string imageFile, string outputFile, double scaleFactor)
        {
            using (var srcImage = Image.FromFile(imageFile))
            {
                var newWidth = (int)(srcImage.Width * scaleFactor);
                var newHeight = (int)(srcImage.Height * scaleFactor);
                using (var newImage = new Bitmap(newWidth, newHeight))
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(srcImage, new Rectangle(0, 0, newWidth, newHeight));
                    newImage.Save(outputFile);
                }
            }
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
