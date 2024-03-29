﻿using humanRegistrationApp.Database.Repositories;
using HumanRegistrationApp.BussinessLogic.DTOs;
using HumanRegistrationApp.Database.AccountService;
using PersonRegistrationApp.BussinessLogic;

namespace HumanRegistrationApp.BussinessLogic.DbServices
{
    public class UserService :IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountService _accountService;
        public UserService(IUserRepository userRepository, IAccountService accountService)
        {
            _userRepository = userRepository;
            _accountService = accountService;
        }
        public ResponseDto Login(string username, string password)
        {
            var userExist = _accountService.Login(username, password);
            if(userExist is null)
            {
                return new ResponseDto(false, "Log in has failed, check if inputed correct credentials");
            }
            return new ResponseDto(true, userExist.Id.ToString());
        }
        public ResponseDto SignUp(string username,string password)
        {
            var user =_userRepository.GetUserByName(username);
            if(user is not null)
            {
                return new ResponseDto(false, "User already exists");
            }
            _accountService.SignUpAccount(username, password);
            return new ResponseDto(true, "User has been created");
        }
        public ResponseDto DeleteUser(string userId)
        {
            if (userId.IsGuidCompatible())
            {
                var user = _userRepository.GetUser(userId);
                if (user is not null)
                {
                    _userRepository.DeleteUser(user);
                    return new ResponseDto(true, "User successfully deleted");
                }
                return new ResponseDto(false, "No User found");
            }
            return new ResponseDto(false, "Such an User Id is wrong");


        }
    }
}
