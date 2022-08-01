using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace HumanRegistrationApp.Database.Model
{
 
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonCode { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public byte[] ProfilePicture { get; set; }
        public virtual Address Address { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
