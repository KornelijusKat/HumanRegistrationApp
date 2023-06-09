using HumanRegistrationApp.BussinessLogic.ImageServices;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace HumanRegistrationSystem.ImageHandler.ImageServices
{
    public class ImageService: IImageService
    {
        public Image ImageResize(IFormFile imageRequest)
        {
            using var memoryStream = new MemoryStream();
            imageRequest.CopyTo(memoryStream);
            Image image = Image.FromStream(memoryStream, true, true);
            Size newSize = new Size(200, 200);
            Image neImage = new Bitmap(newSize.Width,newSize.Height);
            using (Graphics gr = Graphics.FromImage((Bitmap)neImage))
            {
                gr.DrawImage(image, new Rectangle(Point.Empty, newSize));
            }
            return neImage;
        }
        public byte[] ImageToByteArray(Image image)
        {
            ImageConverter converter = new ImageConverter();
            byte[] MyImageArray = (byte[])converter.ConvertTo(image, typeof(byte[]));
            return MyImageArray;
        }
        public byte[] GetByteArray(IFormFile imageRequest)
        {
            var redrawn = ImageResize(imageRequest);
            return ImageToByteArray(redrawn);
        }
        public Image ByteArrayToImage(byte[] imageBytes)
        {
            //ImageConverter converter = new ImageConverter();
            //Image myImage = (Image)converter.ConvertTo(imageBytes, typeof(Image));
            //return myImage;
            Image image = Image.FromStream(new MemoryStream(imageBytes));
           
            return image;
        }

    }
}
