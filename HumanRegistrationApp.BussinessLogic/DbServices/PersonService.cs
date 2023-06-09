using humanRegistrationApp.Database.Repositories;
using HumanRegistrationApp.BussinessLogic.DTOs;
using HumanRegistrationApp.BussinessLogic.Extensions;
using HumanRegistrationApp.BussinessLogic.Validations;
using HumanRegistrationApp.Database.Model;
using HumanRegistrationApp.Database.Repositories;
using PersonRegistrationApp.BussinessLogic;
using System;
using System.Drawing;

namespace HumanRegistrationApp.BussinessLogic.DbServices
{
    public class PersonService : IPersonService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonRepository _personRepository;
        public PersonService(IUserRepository userRepository, IPersonRepository personRepository)
        {
            _userRepository = userRepository;
            _personRepository = personRepository;
        }
        public ResponseDto ChangePersonField<t>(string fieldname, t newValue, string userId)
        {
            if (userId.IsGuidCompatible())
            {
                var person = _personRepository.FindPersonByUserId(userId);
                if (person is not null)
                {
                    _personRepository.UpdatePerson(fieldname, newValue, person);
                    return new ResponseDto(true, "Selected value has been Changed");
                }
                return new ResponseDto(false, "Such a person doesn't exist");
            }
            return new ResponseDto(false, "Given Id is either empty or not in Guid format");
        }
        public ResponseDto ChangePersonsAddressField<t>(string fieldname, t newValue, string userId)
        {
            if (userId.IsGuidCompatible())
            {
                var person = _personRepository.FindPersonAndAddressByUserId(userId);
                if (person is not null)
                {
                    _personRepository.UpdatePersonsAddress(fieldname, newValue, person.Address);
                    return new ResponseDto(true, "Selected value has been Changed");
                }
                return new ResponseDto(false, "Such a person doesn't exist");
            }
            return new ResponseDto(false, "Given Id is either empty or not in Guid format");
        }
        public ResponseDto AddPersonToUser(PersonRequestDto personInput,byte[] picture ,string userId)
        {
            if (userId.IsGuidCompatible()) 
            {
                if (_userRepository.GetUser(userId) is null)
                {
                    return new ResponseDto(false, "User doesn't exist");
                }
                var person = _personRepository.FindPersonByUserId(userId);
                if (person is not null)
                {
                    return new ResponseDto(false, "User has already made a person entry");
                }
                var newPerson = ConvertRequestDtoToPerson(personInput,picture,userId);
                _personRepository.AddInformation(newPerson, userId);
                return new ResponseDto(true, "Person has been added");
            }
            return new ResponseDto(false, "Given Id is either empty or not in Guid format");
        }
        public ResponseDto EditPersonEmail(string field, string newValue, string userId)
        {
            if (userId.IsGuidCompatible())
            {
                var check = newValue.ValidateEmail();
                if (check.IsSuccess)
                {
                    return ChangePersonField<string>(field, newValue, userId);
                }
                return new ResponseDto(false, "Incorrect Email format");
            }
            return new ResponseDto(false, "Given Id is either empty or not in Guid format");
        }   
        public PersonDto ShowPersonById(string personId)
        {
            if(personId.IsGuidCompatible())
            { 
                var person = _personRepository.FindPersonAndAddressByPersonId(personId);
                    if (person is not null)
                    {
                        return ConvertPersonToPersonDto(person);
                    }
            }
            return null;
        }
        private PersonDto ConvertPersonToPersonDto(Person requestedInfo)
        {
            var returnedPerson = new PersonDto
            {
                FirstName = requestedInfo.FirstName,
                LastName = requestedInfo.LastName,
                PersonCode = requestedInfo.PersonCode,
                TelephoneNumber = requestedInfo.TelephoneNumber,
                Email = requestedInfo.Email,
                ProfilePicture = requestedInfo.ProfilePicture,
                Address = new AddressDto
                {
                    City = requestedInfo.Address.City,
                    Street = requestedInfo.Address.Street,
                    FlatNumber = requestedInfo.Address.FlatNumber,
                    HouseNumber = requestedInfo.Address.HouseNumber
                }
            };
            return returnedPerson;
        }
        private Person ConvertRequestDtoToPerson(PersonRequestDto personInput, byte[] picture, string userId)
        {
            var newPerson = new Person
            {
                FirstName = personInput.FirstName,
                LastName = personInput.LastName,
                PersonCode = personInput.PersonCode,
                TelephoneNumber = personInput.TelephoneNumber,
                Email = personInput.Email,
                ProfilePicture = picture,
                UserId = Guid.Parse(userId),
                Address = new Address
                {
                    City = personInput.Address.City,
                    Street = personInput.Address.Street,
                    FlatNumber = personInput.Address.FlatNumber,
                    HouseNumber = personInput.Address.HouseNumber
                }
            };
            return newPerson;
        }
            public PersonImage ConvertPersonToImagePerson(PersonDto personInput, Image picture)
        {
            var newPerson = new PersonImage
            {
                FirstName = personInput.FirstName,
                LastName = personInput.LastName,
                PersonCode = personInput.PersonCode,
                TelephoneNumber = personInput.TelephoneNumber,
                Email = personInput.Email,
                ProfilePicture = picture,
                //UserId = Guid.Parse(userId),
                Address = new AddressDto
                {
                    City = personInput.Address.City,
                    Street = personInput.Address.Street,
                    FlatNumber = personInput.Address.FlatNumber,
                    HouseNumber = personInput.Address.HouseNumber
                }
            };
            return newPerson;
        }
    }
}
