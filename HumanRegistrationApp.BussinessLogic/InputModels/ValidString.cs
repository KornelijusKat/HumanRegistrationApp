using System.ComponentModel.DataAnnotations;


namespace HumanRegistrationApp.BussinessLogic.InputModels
{
   public class ValidString
    {
        [Required(AllowEmptyStrings = false)]
        public string StringInput { get; set; }
    }
}
