using System.Net;
using Microsoft.AspNetCore.Mvc;
using NBB.Api.Controllers;
using NBB.Api.Models;
using NBB.Api.Services;

namespace NBB.Api.Tests.Controllers
{
    public class EnterpriseControllerTests
    {
        [SetUp]
        public void Setup()
        {



        }

        [Test]
        public void GetAll_DoesNotReturnNull()
        {
            // Arrange
            var controller = new EnterpriseController(new InMemoryDB());

            // Act
            var result = controller.GetAll();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Get_Returns404WhenEnterpriseNotFound()
        {
            // Arrange
            var controller = new EnterpriseController(new InMemoryDB());

            // Act
            var ex = controller.Get("NotFound");

            //Assert
            Assert.IsInstanceOf<NotFoundResult>(ex);

        }

        [Test]
        public void Get_ReturnsOkWhenEnterpriseFound()
        {
            //Arrange
            var controller = new EnterpriseController(new InMemoryDB());
            var takumi = new Enterprise
            {
                EnterpriseName = "TAKUMI RAMEN KITCHEN ANTWERPEN",
                Address = new Address
                {
                    Street = "Marnixplein",
                    Number = "10",
                    City = "Antwerpen",
                    PostalCode = "2000",
                    CountryCode = "BE"
                },
                EnterpriseNumber = "0712.657.911"
            };

            //Act
            var result = controller.Get("0712.657.911");

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}