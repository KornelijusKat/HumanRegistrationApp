using HumanRegistrationApp.BussinessLogic.Validations;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace HumanRegistrationApp.BussinessLogic.DTOs
{
    public class PersonImage
    { 
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "Please enter 11 digit code")]
        public string PersonCode { get; set; }
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"(86|\+3706)\d{3}\d{4}", ErrorMessage = "Wrong Format")]
        public string TelephoneNumber { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
        [Required]
        public Image ProfilePicture { get; set; }
        [Required(AllowEmptyStrings = false)]
        public AddressDto Address { get; set; }
    }
}
