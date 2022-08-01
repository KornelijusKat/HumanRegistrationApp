using HumanRegistrationApp.Database.Model;


namespace HumanRegistrationApp.Database.Repositories
{
    public interface IPersonRepository
    {
        Person UpdatePerson<t>(string field,t newValue, Person James);
        Person FindPersonById(string personId);
        Person FindPersonAndAddressByPersonId(string personId);
        void AddInformation(Person newPerson, string userid);
        Person FindPersonByUserId(string userId);
        Person FindPersonAndAddressByUserId(string userId);
        void UpdatePersonsAddress<t>(string field, t newValue, Address James);

    }
}
