using humanRegistrationApp.Database.Repositories;
using HumanRegistrationApp.BussinessLogic.DbServices;
using HumanRegistrationApp.BussinessLogic.DTOs;
using HumanRegistrationApp.BussinessLogic.JwtService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PersonRegistrationApp.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repService;
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        public UserController(IUserRepository repService, IUserService userService, IJwtService jwtService)
        {
            _repService = repService;
            _userService = userService;
            _jwtService = jwtService;
        }
        [HttpPost("Sign up")]
        public ActionResult<ResponseDto> SingUp([FromBody] UserDto user)
        {
            var response = _userService.SignUp(user.UserName, user.Password);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
        [HttpPost("Login")]
        public ActionResult<ResponseDto> Login([FromBody] UserDto userDto)
        {
            var response = _userService.Login(userDto.UserName,userDto.Password);
                if(response.IsSuccess)
                {
                var user =_repService.GetUser(response.Message);
                var theToken = Ok(_jwtService.GetJwtToken(user, user.Role));
                return Ok(new { Token = theToken });
                }
            return BadRequest(response.Message);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("Delete User")]
        public ActionResult<ResponseDto> DeleteUser(string userId)
        { 
           var response = _userService.DeleteUser(userId);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
           return response;
        }
    }
}
