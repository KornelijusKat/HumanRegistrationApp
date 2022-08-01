using HumanRegistrationApp.BussinessLogic.DTOs;



namespace HumanRegistrationApp.BussinessLogic.DbServices
{
    public interface IPersonService
    {
        ResponseDto AddPersonToUser(PersonRequestDto personInput, byte[] picture, string userId);
        ResponseDto ChangePersonField<t>(string fieldname, t newValue, string userId);
        ResponseDto EditPersonEmail(string field, string newValue, string userId);
        ResponseDto ChangePersonsAddressField<t>(string fieldname, t newValue, string userId);
        PersonDto ShowPersonById(string personId);
    }
}
