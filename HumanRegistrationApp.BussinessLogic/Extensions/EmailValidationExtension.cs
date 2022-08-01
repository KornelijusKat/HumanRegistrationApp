using HumanRegistrationApp.BussinessLogic.DTOs;
using System.Text.RegularExpressions;


namespace HumanRegistrationApp.BussinessLogic.Extensions
{
    public static class EmailValidationExtension
    {
        public static ResponseDto ValidateEmail(this string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                return new ResponseDto(false, "Email is not in correct format");
            }
            return new ResponseDto(true);

        }
    }
}
