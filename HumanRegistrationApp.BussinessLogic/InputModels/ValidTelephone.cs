using System.ComponentModel.DataAnnotations;


namespace HumanRegistrationApp.BussinessLogic.InputModels
{
   public class ValidTelephone
    {
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"(86|\+3706)\d{3}\d{4}", ErrorMessage = "Wrong telephone number format, try Lithuanian formats like +3706/86")]
        public string TelephoneNumber { get; set; }
    }
}
