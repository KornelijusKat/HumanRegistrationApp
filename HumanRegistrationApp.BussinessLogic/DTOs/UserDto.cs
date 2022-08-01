using System.ComponentModel.DataAnnotations;

namespace HumanRegistrationApp.BussinessLogic.DTOs
{
    public class UserDto
    {
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
