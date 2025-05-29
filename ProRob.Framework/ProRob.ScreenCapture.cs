using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProRob.Imaging;

namespace ProRob
{
    public class ScreenCapture
    {
        private static Dictionary<ProRob.Imaging.ImageFormat, ImageCodecInfo> encoders = new Dictionary<ProRob.Imaging.ImageFormat, ImageCodecInfo>();
        static ScreenCapture()
        {
            encoders.Add(ProRob.Imaging.ImageFormat.Bmp, ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == System.Drawing.Imaging.ImageFormat.Bmp.Guid));
            encoders.Add(ProRob.Imaging.ImageFormat.Jpeg, ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == System.Drawing.Imaging.ImageFormat.Jpeg.Guid));
            encoders.Add(ProRob.Imaging.ImageFormat.Png, ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == System.Drawing.Imaging.ImageFormat.Png.Guid));
        }

        public static string GetMediaTypeHeader(Imaging.ImageFormat imageFormat)
        {
            string mediaTypeHeader = String.Empty;

            switch (imageFormat)
            {
                case Imaging.ImageFormat.Bmp:
                    mediaTypeHeader = "image/bmp";
                    break;

                case Imaging.ImageFormat.Jpeg:
                    mediaTypeHeader = "image/jpg";
                    break;

                case Imaging.ImageFormat.Png:
                    mediaTypeHeader = "image/png";
                    break;
            }

            return mediaTypeHeader;
        }

        public static byte[] CaptureFullScreen(Imaging.ImageFormat imageFormat, long quality = 100L)
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);

                MemoryStream stream = new MemoryStream();

                var encParams = new EncoderParameters() { Param = new[] { new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality) } };
                bmp.Save(stream, encoders[imageFormat], encParams);

                return stream.ToArray();
            }
        }

        public Bitmap GetFullScreenBitmap(Imaging.ImageFormat imageFormat, long quality = 100L)
        {
            var bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);

            return bmp;
        }
    }
}
