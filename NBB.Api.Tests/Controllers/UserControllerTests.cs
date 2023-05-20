using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NBB.Api.Controllers;
using NBB.Api.Services;
using NBB.Api.ViewModels;

namespace NBB.Api.Tests.Controllers
{
    public class UserControllerTests
    {
        private UserController _controller;
        private InMemoryUserDB _userDB;

        [SetUp]
        public void Setup()
        {
            _userDB = new InMemoryUserDB();
            _controller = new UserController(_userDB);
        }

        [Test]
        public void Get_ReturnUser_WhenUserExists()
        {
            // Arrange
            var username = "test";

            // Act
            var result = _controller.Get(username);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var OkResult = (OkObjectResult)result;
            Assert.AreEqual(StatusCodes.Status200OK,OkResult.StatusCode);
        }

        [Test]
        public void Get_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            var username = "NOTFOUND";

            // Act
            var result = _controller.Get(username);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            var NotFoundResult = (NotFoundResult)result;
            Assert.AreEqual(StatusCodes.Status404NotFound,NotFoundResult.StatusCode);
        }

        [Test]
        public void Post_FindsUser_AfterCreation()
        {
            //Arrange
            var user = new UserCreateViewModel()
            {
                FirstName = "MyTest",
                LastName = "MyTest",
                Password = "MyPassword",
                UserName = "MyTest"
            };

            // Act
            var resultCreation = _controller.Post(user);
            var resultGet = _controller.Get(user.UserName);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(resultCreation);
            var CreatedResult = (CreatedAtActionResult)resultCreation;
            Assert.AreEqual(StatusCodes.Status201Created,CreatedResult.StatusCode);
            Assert.IsInstanceOf<OkObjectResult>(resultGet);
            var OkResult = (OkObjectResult)resultGet;
            Assert.AreEqual(StatusCodes.Status200OK,OkResult.StatusCode);
        }

        [Test]
        public void Post_ReturnsBadRequest_WhenCreatingExistingUser()
        {
            // Arrange
            var user = new UserCreateViewModel()
            {
                FirstName = "Test",
                LastName = "Test",
                UserName = "test",
                Password = "T3st!"
            };

            // Act
            var result = _controller.Post(user);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
            var BadRequestResult = (BadRequestResult)result;
            Assert.AreEqual(StatusCodes.Status400BadRequest,BadRequestResult.StatusCode);
        }

        [Test]
        public void Put_RetursUpdatedUser_AfterUpdate()
        {
            // Arrange
            var user = new UserUpdateViewModel()
            {
                FirstName = "NoTest",
                LastName = "NoTest",
                Password = "N0t3st!",
                UserName = "test"
            };

            // Act
            var result = _controller.Put(user);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var OkResult = (OkObjectResult)result;
            Assert.AreEqual(StatusCodes.Status200OK,OkResult.StatusCode);
        }

        [Test]
        public void Put_ReturnNotFound_WhenUserNotFound()
        {
            // Arrange
            var user = new UserUpdateViewModel()
            {
                FirstName = "NOTFOUND",
                LastName = "NOTFOUND",
                Password = "NOTFOUND",
                UserName = "NOTFOUND"
            };

            // Act
            var result = _controller.Put(user);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            var NotFoundResult = (NotFoundResult)result;
            Assert.AreEqual(StatusCodes.Status404NotFound,NotFoundResult.StatusCode);
        }

        [Test]
        public void Delete_ReturnOK_WhenDeleteUser()
        {
            // Arrange
            var user = new UserCreateViewModel()
            {
                FirstName = "ToDelete",
                LastName = "ToDelete",
                Password = "ToDelete",
                UserName = "ToDelete"
            };

            // Act
            var resultCreation = _controller.Post(user);
            var resultGet = _controller.Get(user.UserName);
            Assert.IsInstanceOf<CreatedAtActionResult>(resultCreation);
            Assert.IsInstanceOf<OkObjectResult>(resultGet);
            var resultDelete = _controller.Delete(user.UserName);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(resultDelete);
            var OkResult = (OkObjectResult)resultDelete;
            Assert.AreEqual(StatusCodes.Status200OK,OkResult.StatusCode);
        }

        [Test]
        public void Delete_ReturnNotFound_WhenUserNotFound()
        {
            // Arrange
            var username = "NOTFOUND";

            // Act
            var result = _controller.Delete(username);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            var NotFoundResult = (NotFoundResult)result;
            Assert.AreEqual(StatusCodes.Status404NotFound, NotFoundResult.StatusCode);
        }
    }
}
