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
        public User SignUpAccount(string username, string password)
        {
            var account = CreateAccount(username, password);
            _context.Users.Add(account);
            _context.SaveChanges();
            return account;
        }
        private User CreateAccount(string username, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            var account = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                Role = "User",
            };
            return account;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
        public User Login(string username, string passwords)
        {
            User account = _context.Users.FirstOrDefault(x => x.Username == username);
            if (account is not null)
            {
                if (VerifyPasswordHash(passwords, account.Password, account.PasswordSalt))
                {
                    return account;
                }
            }
            return null;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
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
