using HumanRegistrationApp.Database.Model;


namespace humanRegistrationApp.Database.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string userId);
        User GetUserByName(string username);
        void DeleteUser(User user);
    }
}
