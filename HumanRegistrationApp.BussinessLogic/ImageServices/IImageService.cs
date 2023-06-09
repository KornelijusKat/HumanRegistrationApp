using Microsoft.AspNetCore.Http;
using System.Drawing;


namespace HumanRegistrationApp.BussinessLogic.ImageServices
{
   public interface IImageService
    {
        byte[] ImageToByteArray(Image image);
        Image ImageResize(IFormFile imageRequest);
        byte[] GetByteArray(IFormFile imageRequest);
        Image ByteArrayToImage(byte[] imageBytes);
    }
}
