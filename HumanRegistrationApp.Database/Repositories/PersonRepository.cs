using HumanRegistrationApp.Database.Context;
using HumanRegistrationApp.Database.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace HumanRegistrationApp.Database.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ProjectContext _context;
        public PersonRepository(ProjectContext context)
        {
            _context = context;
        }
        public void AddInformation(Person newPerson, string userid)
        { 
            _context.Persons.Add(newPerson);
            _context.SaveChanges();
        }
        public Person FindPersonAndAddressByPersonId(string personId)
        {
            return  _context.Persons.Include(x => x.Address).FirstOrDefault(x => x.Id == Guid.Parse(personId));         
        }
        public Person FindPersonById(string personId)
        {
            return _context.Persons.SingleOrDefault(x => x.Id == Guid.Parse(personId));
        }
        public Person UpdatePerson<t>(string field, t newValue, Person person)
        {
            person.GetType().GetProperty(field).SetValue(person, newValue, null);
            _context.SaveChanges();
            return person;
        }
        public void UpdatePersonsAddress<t>(string field, t newValue, Address address)
        {
            address.GetType().GetProperty(field).SetValue(address, newValue, null);
            _context.SaveChanges();
        }
        public Person FindPersonByUserId(string userId)
        {
            return _context.Persons.SingleOrDefault(x => x.UserId == Guid.Parse(userId));
        }
        public Person FindPersonAndAddressByUserId(string userId)
        {
            return _context.Persons.Include(s=>s.Address).SingleOrDefault(x => x.UserId == Guid.Parse(userId));
        }
    }
}
