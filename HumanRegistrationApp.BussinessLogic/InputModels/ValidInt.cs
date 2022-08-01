using System.ComponentModel.DataAnnotations;


namespace HumanRegistrationApp.BussinessLogic.InputModels
{
    public class ValidInt
    {
        [Required(AllowEmptyStrings = false)]
        public int Integer { get; set; }
    }
}
