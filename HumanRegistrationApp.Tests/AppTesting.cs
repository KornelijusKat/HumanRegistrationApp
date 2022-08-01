using AutoFixture.Xunit2;
using humanRegistrationApp.Database.Repositories;
using HumanRegistrationApp.BussinessLogic.DbServices;
using HumanRegistrationApp.BussinessLogic.DTOs;
using HumanRegistrationApp.BussinessLogic.InputModels;
using HumanRegistrationApp.BussinessLogic.JwtService;
using HumanRegistrationApp.Database.Model;
using HumanRegistrationApp.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonRegistrationApp.Api.Controllers;
using PersonRegistrationApp.BussinessLogic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace HumanRegistrationApp.Tests
{
    public class AppTesting
    {

        [Theory, AutoData]
        public void Check_if_False_Response_Retuns_BadRequest_Result_IN_HttpLogin(UserDto request)
        {
            var serMock = new Mock<IUserService>();
            var jwtMock = new Mock<IJwtService>();
            var repMock = new Mock<IUserRepository>();
            var sut = new UserController(repMock.Object, serMock.Object, jwtMock.Object);
            serMock.Setup(x => x.Login(request.UserName, request.Password)).Returns(new ResponseDto(false));
            repMock.Setup(x => x.GetUserByName(request.UserName)).Returns((User)null);
            var testresult = sut.Login(request);       
            Assert.IsType<BadRequestObjectResult>(testresult.Result);
        }
        [Theory, AutoData]
        public void Test_If_Singing_Up_Response_Is_Equal_To_Message_When_GetUser_Returns_NotNull(User newUser)
        {
            string password = "john";
            var repMock = new Mock<IUserRepository>();
            var serMock = new Mock<IUserService>();
            var sut = new UserService(repMock.Object);
            repMock.Setup(x => x.GetUserByName(newUser.Username)).Returns(newUser);
            //Act
            var testobject = sut.SignUp(newUser.Username, password);
            //Assert
            var expected = "User already exists";
            Assert.Equal(expected, testobject.Message);
        }
        [Theory, AutoData]
        public void Test_If_Singing_Up_Response_Is_True_If_User_Does_Not_Exist(User newUser)
        {
            string password = "john";
            var repMock = new Mock<IUserRepository>();
            var serMock = new Mock<IUserService>();
            var sut = new UserService(repMock.Object);
            repMock.Setup(x => x.GetUserByName(newUser.Username)).Returns((User)null);
            //Act
            var testobject = sut.SignUp(newUser.Username, password);
            //Assert
            Assert.True(testobject.IsSuccess);
        }
        [Theory, AutoData]
        public void Test_If_Singing_Up_Response_Is_False_If_User_Exists(User newUser)
        {
            string password = "john";
            var repMock = new Mock<IUserRepository>();
            var serMock = new Mock<IUserService>();
            var sut = new UserService(repMock.Object);
            repMock.Setup(x => x.GetUserByName(newUser.Username)).Returns(newUser);
            //Act
            var testobject = sut.SignUp(newUser.Username, password);
            //Assert
            Assert.False(testobject.IsSuccess);
        }
        [Theory, AutoData]
        public void Test_If_User_Can_Add_Person(User user, PersonDto personDto)
        {
            var repPersonMock = new Mock<IPersonRepository>();
            var repUserMock = new Mock<IUserRepository>();
            var sut = new PersonService(repUserMock.Object, repPersonMock.Object);
            var personRequest = new PersonRequestDto()
            {
                Address = personDto.Address,
                Email = personDto.Email,
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                PersonCode = personDto.PersonCode,
                ProfilePicture = null,
                TelephoneNumber = personDto.TelephoneNumber
            };
            repUserMock.Setup(x => x.GetUser(user.Id.ToString())).Returns(user);
            repPersonMock.Setup(x => x.FindPersonByUserId(user.Id.ToString())).Returns((Person)null);
            var testObject = sut.AddPersonToUser(personRequest, personDto.ProfilePicture, user.Id.ToString());
            var expected = "Person has been added";
            Assert.Equal(expected, testObject.Message);
        }
        [Theory, AutoData]
        public void Test_If_User_Can_Add_A_Second_Person(User user, PersonDto personDto, Person person)
        {
            var repPersonMock = new Mock<IPersonRepository>();
            var repUserMock = new Mock<IUserRepository>();
            var sut = new PersonService(repUserMock.Object, repPersonMock.Object);
            var personRequest = new PersonRequestDto()
            {
                Address = personDto.Address,
                Email = personDto.Email,
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                PersonCode = personDto.PersonCode,
                ProfilePicture = null,
                TelephoneNumber = personDto.TelephoneNumber
            };
            repUserMock.Setup(x => x.GetUser(user.Id.ToString())).Returns(user);
            repPersonMock.Setup(x => x.FindPersonByUserId(user.Id.ToString())).Returns(person);
            var testObject = sut.AddPersonToUser(personRequest, personDto.ProfilePicture, user.Id.ToString());
            var expected = "User has already made a person entry";
            Assert.Equal(expected, testObject.Message);
        }
        [Theory, AutoData]
        public void Test_Delete_If_UserId_Returns_Null(string userdId)
        {
            var repMock = new Mock<IUserRepository>();
            var sut = new UserService(repMock.Object);
            repMock.Setup(x => x.GetUser(userdId)).Returns((User)null);
            var testObject = sut.DeleteUser(userdId);
            var expected = "Such an User Id is wrong";
            Assert.False(testObject.IsSuccess);
        }
        [Fact]
        public void Test_If_IsGuidCompatible_Returns_False_When_String_Is_Not_Guid_Parsable()
        {
            string testSubject = "14544456";
            var result = testSubject.IsGuidCompatible();
            Assert.False(result);
        }
        [Fact]
        public void Test_If_IsGuidCompatible_Returns_True_On_Compatible_String()
        {
            string testSubject = "C2DE9BF7-2420-4A25-9869-F786322345C8";
            var result = testSubject.IsGuidCompatible();
            Assert.True(result);
        }
        [Theory, AutoData]
        public void Test_If_Person_Returns_Null(Person person, ValidTelephone things)
        {
            string userId = "C2DE9BF7-2420-4A25-9869-F786322345C8";

            var repPersonMock = new Mock<IPersonRepository>();
            var repUserMock = new Mock<IUserRepository>();
            var sut = new PersonService(repUserMock.Object, repPersonMock.Object);
            repPersonMock.Setup(x => x.FindPersonByUserId(userId)).Returns((Person)null);
            repPersonMock.Setup(x => x.UpdatePerson("TelephoneNumber", things.TelephoneNumber, person)).Returns(new Person());
            var testObject = sut.ChangePersonField("TelephoneNumber", things.TelephoneNumber, userId);
            Assert.Equal("Such a person doesn't exist", testObject.Message);
        }
        [Fact]
        public void Test_ValidTelephone_Reg_Expression_Attribute_When_String_Is_Not_Compatible()
        {
            var propInfo = typeof(Person).GetProperty("TelephoneNumber");
            var attr = propInfo.GetCustomAttributes(typeof(RegularExpressionAttribute), true);
            var testProp = "+3706321456171";
            Assert.False(((RegularExpressionAttribute)attr[0]).IsValid(testProp));

        }
        [Fact]
        public void Test_ValidTelephone_Reg_Expression_Attribute_With_2_Valid_Formats()
        {
            var propInfo = typeof(Person).GetProperty("TelephoneNumber");
            var attr = propInfo.GetCustomAttributes(typeof(RegularExpressionAttribute), true);
            var testProp1 = "+37063215611";
            var testProp2 = "863271234";
            Assert.True(((RegularExpressionAttribute)attr[0]).IsValid(testProp1) && ((RegularExpressionAttribute)attr[0]).IsValid(testProp2));
        }
        [Fact]
        public void Test_ValidPersonCode_Reg_Expression_Attribute_When_String_Is_Not_Compatible()
        {
            var propInfo = typeof(Person).GetProperty("PersonCode");
            var attr = propInfo.GetCustomAttributes(typeof(RegularExpressionAttribute), true);
            var testProp = "3706321456171";
            Assert.False(((RegularExpressionAttribute)attr[0]).IsValid(testProp));
        }
    }
}
