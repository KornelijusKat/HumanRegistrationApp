using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace HumanRegistrationApp.Database.Model
{
   public class Address
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int FlatNumber { get; set; }
        [ForeignKey("PersonId")]
        public Guid PersonId { get; set; }
        public Address()
        {
            Id = Guid.NewGuid();
        }
    }
}
