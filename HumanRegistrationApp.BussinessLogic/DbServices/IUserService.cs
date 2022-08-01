using HumanRegistrationApp.BussinessLogic.DTOs;


namespace HumanRegistrationApp.BussinessLogic.DbServices
{
    public interface IUserService
    {
        ResponseDto Login(string username, string password);
        ResponseDto SignUp(string username, string password);
        ResponseDto DeleteUser(string userId);
    }
}
