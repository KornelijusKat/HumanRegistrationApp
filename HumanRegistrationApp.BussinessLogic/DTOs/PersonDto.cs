using System.ComponentModel.DataAnnotations;


namespace HumanRegistrationApp.BussinessLogic.DTOs
{
    public class PersonDto
    {
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false)]
        [RegularExpression("^[3-6][0-9]{2}[0,1][0-9][0-9]{2}[0-9]{4}$", ErrorMessage = "Please enter 11 digit Lithuanian format code")]
        public string PersonCode { get; set; }
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"(86|\+3706)\d{3}\d{4}", ErrorMessage = "Wrong Telephone Format, try +3706/86")]
        public string TelephoneNumber { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
        public byte[] ProfilePicture { get; set; }
        [Required(AllowEmptyStrings = false)]
        public AddressDto Address { get; set; }
    }
}
