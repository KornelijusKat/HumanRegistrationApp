using humanRegistrationApp.Database.Repositories;
using HumanRegistrationApp.Database.Context;
using HumanRegistrationApp.Database.Model;
using System;
using System.Linq;
using System.Security.Cryptography;


namespace HumanRegistrationApp.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProjectContext _context;
        public UserRepository(ProjectContext context)
        {
            _context = context;
        }
        public User GetUser(string userId)
        {
            return _context.Users.FirstOrDefault(x => x.Id == Guid.Parse(userId));
        }
        public User GetUserByName(string username)
        {
            return _context.Users.FirstOrDefault(x => x.Username == username);
        }
        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
 }
