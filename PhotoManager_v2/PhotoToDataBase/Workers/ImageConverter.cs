using System.IO;
using System.Windows.Media.Imaging;

namespace PhotoToDataBase.Workers
{
    class ImageConverter
    {
        public static byte[] ConvertImageToByteArray(BitmapImage bitmapImage)
        {
            MemoryStream memoryStream = new MemoryStream();
            JpegBitmapEncoder jpggBitmapEncoder = new JpegBitmapEncoder();
            jpggBitmapEncoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            jpggBitmapEncoder.Save(memoryStream);
            return memoryStream.ToArray();
        }

        public static BitmapImage ConvertByteArrayToBitmapImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream memoryStream = new MemoryStream(imageData))
            {
                memoryStream.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.UriSource = null;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
            }
            bitmapImage.Freeze();
            return bitmapImage;
        }
    }
}
