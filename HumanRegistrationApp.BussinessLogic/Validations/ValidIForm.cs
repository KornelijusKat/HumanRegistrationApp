using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace HumanRegistrationApp.BussinessLogic.Validations
{
    public class ValidIForm
    {
        [Required(AllowEmptyStrings = false)]
        [ValidExtensions(new string[] { ".png", ".jpg", } )]
        public IFormFile Picture { get; set; }
    }
}
