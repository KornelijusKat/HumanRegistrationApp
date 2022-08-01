using System;


namespace HumanRegistrationApp.Database.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        public virtual Person Person { get; set; }
    }
}
