using HumanRegistrationApp.Database.Model;


namespace HumanRegistrationApp.BussinessLogic.JwtService
{
    public interface IJwtService
    {
        string GetJwtToken(User user, string role);
    }
}
