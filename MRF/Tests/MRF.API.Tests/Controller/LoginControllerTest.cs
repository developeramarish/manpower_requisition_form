using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using MRF.API.Controllers;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using MRF.DataAccess.Repository.IRepository;

namespace MRF.API.Test.Controllers
{
    public class LoginControllerTest
    {
        private readonly TestFixture fixture;
        private LoginController Controller;

        public LoginControllerTest()
        {
            fixture = new TestFixture();
            Controller = new LoginController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object, fixture.MockUserService.Object);

        }

        [Fact]
        public void LoginControllerTest_Constructor_ShouldInitializeDependencies()
        {
            // Assert
            Assert.NotNull(fixture.Controller);

        }
        //[Fact]
        //public void GetLogindetailById_ShouldReturnNoResult_WhenInputIsEqualsZero()
        //{
        //    // Arrange

        //     String userName="kritika";

        //    // Create a list of sample Employeedetails for testing
        //    var sampleLogindetails = new List<Employeedetails>
        //    {
        //    new Employeedetails  {Name = "kritika",Email="k@gmail.com",ContactNo="975546",},
        //    new Employeedetails {Name = "Ragni",Email="k@gmail.com",ContactNo= "975546", },
        //        // Add more sample data as needed
        //    };

        //    // Set up the behavior of the mockUnitOfWork to return the sample data
        //    fixture.MockUnitOfWork.Setup(uow => uow.Employeedetails.GetAll()).Returns(sampleLogindetails);

        //    // Act
        //    var result = Controller.Get(userName);

             
        //    Assert.NotNull(result);
        //    result.Should().NotBeNull();
            
        //    // Assert
             


        //}
        //[Fact]
        //public void GetLogindetailsById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        //{
        //    // Arrange


        //     String  userName = "0";

        //    // Create a list of sample Employeedetails for testing
        //    var sampleLogindetails = new List<Employeedetails>
        //    {
        //    new Employeedetails {Name = "kritika",Email="k@gmail.com",ContactNo="975546",},
        //    new Employeedetails {Name = "kritika",Email="k@gmail.com",ContactNo="975546",},
        //        // Add more sample data as needed   
        //    };

        //    // Set up the behavior of the mockUnitOfWork to return the sample data
        //    fixture.MockUnitOfWork.Setup(uow => uow.Employeedetails.GetAll()).Returns(sampleLogindetails);

        //    // Act  
        //    var result = Controller.Get(userName);

        //    // Assert
        //    result.Should().NotBeNull();
        //    fixture.MockLogger.Verify(logger => logger.LogError("Login Failed:"+ userName));
        //}

        




    }
}
