using HumanRegistrationApp.BussinessLogic.DTOs;
using HumanRegistrationApp.Database.Model;
using System.Drawing;

namespace HumanRegistrationApp.BussinessLogic.DbServices
{
    public interface IPersonService
    {
        ResponseDto AddPersonToUser(PersonRequestDto personInput, byte[] picture, string userId);
        ResponseDto ChangePersonField<t>(string fieldname, t newValue, string userId);
        ResponseDto EditPersonEmail(string field, string newValue, string userId);
        ResponseDto ChangePersonsAddressField<t>(string fieldname, t newValue, string userId);
        PersonDto ShowPersonById(string personId);
        PersonImage ConvertPersonToImagePerson(PersonDto personInput, Image picture);
    }
}
