using System.ComponentModel.DataAnnotations;


namespace HumanRegistrationApp.BussinessLogic.InputModels
{
   public class ValidPersonCode
    {
        [Required(AllowEmptyStrings = false)]
        [RegularExpression("^[3-6][0-9]{2}[0,1][0-9][0-9]{2}[0-9]{4}$", ErrorMessage = "Please enter 11 digit code")]
        public string PersonCode { get; set; }
    }
}
