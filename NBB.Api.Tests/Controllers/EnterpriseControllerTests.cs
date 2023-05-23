using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NBB.Api.Controllers;
using NBB.Api.Models;
using NBB.Api.Services;
using NBB.Api.ViewModels;

namespace NBB.Api.Tests.Controllers
{
    public class EnterpriseControllerTests
    {
        private EnterpriseController controller;

        [SetUp]
        public void Setup()
        {
            controller = new EnterpriseController(new InMemoryDB());
        }

        [Test]
        public void GetAll_DoesNotReturnNull()
        {
            // Act
            var result = controller.GetAll();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Get_ReturnsNotFound_WhenEnterpriseNotFound()
        {
            // Arrange
            var enterpriseID = "NOTFOUND";

            // Act
            var result = controller.Get(enterpriseID);

            //Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            var NFResult = (NotFoundResult)result;
            Assert.AreEqual(StatusCodes.Status404NotFound,NFResult.StatusCode);
        }

        [Test]
        public void Get_ReturnsOk_WhenEnterpriseFound()
        {
            //Arrange
            var takumi = new Enterprise
            {
                ReferenceNumber = "2021-00000148",
                DepositDate = "2021-12-07",
                startDate = "2020-01-01",
                endDate = "2020-12-31",
                ModelType = "m02-f",
                DepositType = "Initial",
                Language = "NL",
                Currency = "EUR",
                EnterpriseName = "TAKUMI RAMEN KITCHEN ANTWERPEN",
                Address = new Address
                {
                    Box = null,
                    Street = "Marnixplein",
                    Number = "10",
                    City = "Antwerpen",
                    PostalCode = "2000",
                    CountryCode = "BE"
                },
                EnterpriseNumber = "0712657911",
                LegalForm = "014",
                LegalSituation = "000",
                FullFillLegalValidation = true,
                ActivityCode = null,
                GeneralAssemblyDate = "2021-06-01",
                DataVersion = "Authentic",
                ImprovementDate = null,
                CorrectedData = null,
                FinancialData = new FinancialData()
                {
                    Id = 1,
                    Year = 2021,
                    Profit = 10000,
                    Revenue = 21102
                }
                };

            //Act
            var result = controller.Get("0712657911");

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(StatusCodes.Status200OK,okResult.StatusCode);
        }

        [Test]
        public void GetDouble_ValidEnterpriseNumbers_ReturnsListOfEnterprises()
        {
            // Arrange
            var entArray = new EnterpriseArrayViewModel
            {
                EnterpriseNumbers = new List<string> { "0712657911", "0764896369" } 
            };

            // Act
            var result = controller.GetDouble(entArray);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

            var enterprises = (List<Enterprise>)okResult.Value;
            Assert.AreEqual(2, enterprises.Count); 
        }

        [Test]
        public void GetDouble_ReturnNotFound_WhenBothEnterprisesNotFound()
        {
            // Arrange
            var entArray = new EnterpriseArrayViewModel
            {
                EnterpriseNumbers = new List<string> { "NOTFOUND", "NOTFOUND" }
            };

            // Act
            var result = controller.GetDouble(entArray);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            var NFResult = (NotFoundResult)result;
            Assert.AreEqual(StatusCodes.Status404NotFound,NFResult.StatusCode);
        }

        [Test]
        public void GetDouble_ReturnNotFound_WhenOneEnterpriseFound()
        {
            // Arrange
            var entArray = new EnterpriseArrayViewModel
            {
                EnterpriseNumbers = new List<string> { "0712657911", "NOTFOUND" }
            };

            // Act
            var result = controller.GetDouble(entArray);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            var NFResult = (NotFoundResult)result;
            Assert.AreEqual(StatusCodes.Status404NotFound, NFResult.StatusCode);
        }
    }
}