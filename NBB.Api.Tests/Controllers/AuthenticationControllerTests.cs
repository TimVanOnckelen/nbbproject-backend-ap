using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NBB.Api.Controllers;
using NBB.Api.Services;
using Moq;
using NBB.Api.Entities;
using NBB.Api.Models;
using NBB.Api.ViewModels;

namespace NBB.Api.Tests.Controllers
{
    public class AuthenticationControllerTests
    {
        private AuthenticationController _controller;
        private InMemoryUserDB _userRepo;
        private Mock<Microsoft.Extensions.Configuration.IConfiguration> _configuration;

        [SetUp]
        public void Setup()
        {
            _userRepo = new InMemoryUserDB();
            _configuration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            _controller = new AuthenticationController(_configuration.Object, _userRepo);
        }

        [Test]
        public void CreateToken_ReturnToken_WhenSuccesfullAuth()
        {
            // Arrange
            var loginModel = new UserLoginViewModel
            {
                UserName = "test",
                Password = "T3st!"
            };
            _configuration.Setup(x => x["JWT:ServerSecret"]).Returns("this is my custom Secret key for testing purpose!");
            _configuration.Setup(x => x["JWT:Issuer"]).Returns("TestSuite");

            // Act
            var result = _controller.CreateToken(loginModel);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void CreateToken_ReturnUnauthorized_WhenUnsuccesfullAuth()
        {
            // Arrange
            var loginModel = new UserLoginViewModel
            {
                UserName = "Test",
                Password = "WRONG"
            };

            // Act
            var result = _controller.CreateToken(loginModel);

            // Assert
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }
    }
}
