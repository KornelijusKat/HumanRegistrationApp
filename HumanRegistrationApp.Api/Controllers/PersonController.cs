using HumanRegistrationApp.BussinessLogic.DbServices;
using HumanRegistrationApp.BussinessLogic.DTOs;
using HumanRegistrationApp.BussinessLogic.ImageServices;
using HumanRegistrationApp.BussinessLogic.InputModels;
using HumanRegistrationApp.BussinessLogic.Validations;
using HumanRegistrationApp.Database.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonRegistrationApp.BussinessLogic;

namespace PersonRegistrationApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User,Admin")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IImageService _imageService;
        public PersonController(IPersonService personService, IImageService imageService)
        {
            _personService = personService;
            _imageService = imageService;
        }
        [HttpPost("PostingPersonData")]
        public ActionResult<ResponseDto> AddPersonDetails([FromForm] PersonRequestDto person,[FromForm] string userId)
        {
            var pictureByteArray = _imageService.GetByteArray(person.ProfilePicture.Picture);
            var response = _personService.AddPersonToUser(person,pictureByteArray ,userId);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return response;
        }
        [HttpPut("ChangeEmail")]
        public ActionResult<ResponseDto> ChangeEmail([FromBody] string newValue,[FromQuery] string userId)
        {
            var response = _personService.EditPersonEmail("Email", newValue, userId);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return response;
        }
        [HttpPut("ChangeTelephoneNumber")]
        public ActionResult<ResponseDto> ChangeTelephoneNumber([FromBody] ValidTelephone newValue,[FromQuery] string userId)
        {
            var response = _personService.ChangePersonField<string>("TelephoneNumber", newValue.TelephoneNumber,userId );
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return response;
        }
        [HttpPut("ChangeFirstName")]
        public ActionResult<ResponseDto> ChangeFirstName([FromBody] ValidString newValue,[FromQuery] string userId)
        {
            var response = _personService.ChangePersonField<string>("FirstName", newValue.StringInput, userId);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return response;
        }
        [HttpPut("ChangeLastName")]
        public ActionResult<ResponseDto> ChangeLastName([FromBody] ValidString newValue, string userId)
        {
            var response = _personService.ChangePersonField<string>("LastName", newValue.StringInput, userId);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return response;
        }
        [HttpPut("ChangePersonCode")]
        public ActionResult<ResponseDto> ChangePersonCode([FromBody] ValidPersonCode newValue,[FromQuery] string userId)
        {
            var response = _personService.ChangePersonField<string>("PersonCode", newValue.PersonCode, userId);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return response;
        }
        [HttpPut("ChangeCity")]
        public ActionResult<ResponseDto> ChangeCity([FromBody] ValidString newValue,string userId)
        {
            var response = _personService.ChangePersonsAddressField<string>("City", newValue.StringInput, userId);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return response;
        }
        [HttpPut("ChangeStreet")]
        public ActionResult<ResponseDto> ChangeStreet([FromBody] ValidString newValue, string userId)
        {
           var response = _personService.ChangePersonsAddressField<string>("Street", newValue.StringInput, userId);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return response;
        }
        [HttpPut("ChangeHouseNumber")]
        public ActionResult<ResponseDto> ChangeHouseNumber([FromBody] ValidInt newValue, string userId)
        {
            var response = _personService.ChangePersonsAddressField<int>("HouseNumber", newValue.Integer, userId);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return response;
        }
        [HttpPut("Change Apartment Number")]
        public ActionResult<ResponseDto> ChangeFlatNumber([FromBody]ValidInt newValue, string userId)
        {
            var response = _personService.ChangePersonsAddressField<int>("FlatNumber", newValue.Integer, userId);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return response;
        }
        [HttpPost("GetInformationOnPerson")]
        public ActionResult GetPerson([FromBody] ValidString personId)
        {
            if (personId.StringInput.IsGuidCompatible())
            {
                var resposne = _personService.ShowPersonById(personId.StringInput);
                if (resposne is null)
                {
                    return BadRequest("Person doesn't exist");
                }
                return Ok(resposne);
            }
            return BadRequest("PersonId is not valid, check if input is not empty and is Guid compatible");
        }
        [HttpPut("ChangeProfilePicture")]
        public ActionResult<ResponseDto> ChangePicture([FromForm] ValidIForm imageRequest,string userId)
        {
            var newImageArray = _imageService.GetByteArray(imageRequest.Picture);
            var response = _personService.ChangePersonField<byte[]>("ProfilePicture",newImageArray,userId);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return response;
        }
        [HttpPost("GetInfoPicture")]
        public ActionResult GetPersonWithImage([FromBody] ValidString personId)
        {
            if (personId.StringInput.IsGuidCompatible())
            {
                var resposne = _personService.ShowPersonById(personId.StringInput);
                if (resposne is null)
                {
                    return BadRequest("Person doesn't exist");
                }
                var newImage = _imageService.ByteArrayToImage(resposne.ProfilePicture);
                var convertedResponse = _personService.ConvertPersonToImagePerson(resposne, newImage);
                return Ok(convertedResponse);
            }
            return BadRequest("PersonId is not valid, check if input is not empty and is Guid compatible");
        }
    }
}
