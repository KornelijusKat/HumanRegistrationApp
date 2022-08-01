using HumanRegistrationApp.Database.Model;


namespace humanRegistrationApp.Database.Repositories
{
    public interface IUserRepository
    {
        User SignUpAccount(string username, string password);
        User GetUser(string userId);
        User Login(string username, string passwords);
        User GetUserByName(string username);
        void DeleteUser(User user);
    }
}
