using System.ComponentModel.DataAnnotations;


namespace HumanRegistrationApp.BussinessLogic.DTOs
{
    public class AddressDto
    {
        [Required(AllowEmptyStrings = false)]
        public string City { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Street { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int HouseNumber { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int FlatNumber { get; set; }
    }
}
