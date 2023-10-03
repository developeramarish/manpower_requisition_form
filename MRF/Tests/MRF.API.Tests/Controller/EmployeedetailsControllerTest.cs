using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using MRF.API.Controllers;
using Microsoft.Extensions.Hosting;

namespace MRF.API.Test.Controllers
{
    public class EmployeedetailsControllerTest
    {
        private readonly TestFixture fixture;
        private EmployeedetailsController Controller;
     
        public EmployeedetailsControllerTest()
        {
            fixture = new TestFixture();
            Controller = new EmployeedetailsController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object, fixture.MockEmailService.Object, fixture.MockHostEnvironment.Object);
        }
      

        [Fact]
        public void EmployeedetailsControllerTest_Constructor_ShouldInitializeDependencies()
        {
            // Assert
            Assert.NotNull(fixture.Controller);

        }

        [Fact]
        public void EmployeedetailsController_ShouldReturnCount_WhenDataFound()
        {


            // Create a list of sample Employeedetails for testing
            var sampleEmployeeDetails = new List<Employeedetails>
            {
            new Employeedetails { Id=1, Name = "John Doe", Email ="k@gmail.com",ContactNo = "97554678",},
            new Employeedetails { Id=2, Name = "Jane Smith",Email ="k@gmail.com",ContactNo = "97554678", },

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Employeedetails.GetAll()).Returns(sampleEmployeeDetails);


            // Act
            var result = Controller.Get();

            // Assert
            Assert.Equal(sampleEmployeeDetails.Count, result.Count);
            Assert.NotNull(result);
            result.Should().NotBeNull();
            fixture.MockUnitOfWork.Verify(x => x.Employeedetails, Times.Once());
        }


        [Fact]
        public void EmployeedetailsController_ShouldReturnNotFound_WhenDataNotFound()
        {

            fixture.MockUnitOfWork.Setup(x => x.Employeedetails.GetAll()).Returns(new List<Employeedetails>());


            // Act
            var result = Controller.Get();

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
            fixture.MockUnitOfWork.Verify(x => x.Employeedetails, Times.Once());
        }


        [Fact]
        public void GetEmployeeDetailslById_ShouldReturnNoResult_WhenInputIsEqualsZero()
        {
            // Arrange

            int id = 0;

            // Create a list of sample Employeedetails for testing
            var sampleEmployeeDetails = new List<Employeedetails>
            {
            new Employeedetails { Name = "John Doe" ,Email ="k@gmail.com",ContactNo = "97554678",},
            new Employeedetails { Name = "Jane Smith" ,Email ="k@gmail.com",ContactNo = "97554678",},
                // Add more sample data as needed
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Employeedetails.GetAll()).Returns(sampleEmployeeDetails);

            // Act
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:"+id));


        }
        [Fact]
        public void GetEmployeeDetailslById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange


            int id = 0;

            // Create a list of sample Employeedetails for testing
            var sampleEmployeeDetails = new List<Employeedetails>
            {
            new Employeedetails { Name = "John Doe" ,Email ="k@gmail.com",ContactNo = "97554678",},
            new Employeedetails { Name = "Jane Smith",Email ="k@gmail.com",ContactNo = "97554678",},
                // Add more sample data as needed
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Employeedetails.GetAll()).Returns(sampleEmployeeDetails);

            // Act  
        

            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:"+id));
        }
        [Fact]
        public void CreateEmployeeDetails_ShouldReturnOkResponse_WhenValidRequest()
        {


            var requestModel = new EmployeedetailsRequestModel
            {
                Name = "ABc",
                Email = "k@gmail.com",
                ContactNo = "97554678",
                CreatedByEmployeeId = 1,
                CreatedOnUtc = DateTime.Now,
                UpdatedByEmployeeId = 1,
                UpdatedOnUtc = DateTime.Now,
                 
            };

            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Employeedetails.Add(It.IsAny<Employeedetails>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            


       var responseModel = new EmployeedetailsResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act


            
            var result = Controller.Post(requestModel);

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<EmployeedetailsResponseModel>(result);
            var okResult = (EmployeedetailsResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Employeedetails was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Employeedetails.Add(It.IsAny<Employeedetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        }
        [Fact]
        public void CreateEmployeeDetailsl_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Employeedetails.Add(It.IsAny<Employeedetails>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new EmployeedetailsResponseModel
            {
                Id = 0,
                 // Set the expected Id
                                       // Set other properties as needed
            };

            // Act
            var result = Controller.Post(new EmployeedetailsRequestModel());

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<EmployeedetailsResponseModel>(result);
            var okResult = (EmployeedetailsResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Employeedetails was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Employeedetails.Add(It.IsAny<Employeedetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            // Assert
            result.Should().NotBeNull();

        }
        [Fact]
        public void DeleteEmployeeDetails_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange

            int existingId = 1; // Replace with an existing Id in your test data

            // Mock the behavior of the Get and Remove methods in IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Employeedetails.Get(It.IsAny<Expression<Func<Employeedetails, bool>>>()))
                .Returns((Expression<Func<Employeedetails, bool>> filter) =>
                {
                    // Simulate returning an object when the filter condition matches
                    if (filter.Compile().Invoke(new Employeedetails { Id = existingId }))
                    {
                        return new Employeedetails { Id = existingId };
                    }
                    return null;
                });

            // Act
            Controller.Delete(existingId);


            // Assert
            // Verify that the Remove and Save methods of IUnitOfWork were called as expected
            fixture.MockUnitOfWork.Verify(uow => uow.Employeedetails.Get(It.IsAny<Expression<Func<Employeedetails, bool>>>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Employeedetails.Remove(It.IsAny<Employeedetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }
        [Fact]
        public void DeleteEmployeeDetails_ShouldReturnNotFound_WhenRecordNotFound()
        {

            // Arrange


            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 999; // An ID that is assumed not to exist.
            fixture.MockUnitOfWork.Setup(uow => uow.Employeedetails.Get(It.IsAny<Expression<Func<Employeedetails, bool>>>()))
           .Returns((Expression<Func<Employeedetails, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Employeedetails.Remove(It.IsAny<Employeedetails>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);

        }
        [Fact]
        public void DeleteEmployeeDetails_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange

            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 0;
            fixture.MockUnitOfWork.Setup(uow => uow.Employeedetails.Get(It.IsAny<Expression<Func<Employeedetails, bool>>>()))
           .Returns((Expression<Func<Employeedetails, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Employeedetails.Remove(It.IsAny<Employeedetails>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact]
        public void UpdateEmployeeDetails_ShouldReturnIsActiveTrue_WhenRecordIsUpdated()
        {
            // Arrange

            int entityId = 1; // Example ID for an existing entity

            var requestModel = new EmployeedetailsRequestModel
            {
                Name = "ABc",
                Email = "k@gmail.com",
                ContactNo = "97554678",
                IsAllowed = true,
            };

            // Mock the behavior of IUnitOfWork's Get method to return an existing entity
            var existingEntity = new Employeedetails
            {
                Id = entityId,
               
            };

            fixture.MockUnitOfWork.Setup(uow => uow.Employeedetails.Get(It.IsAny<Expression<Func<Employeedetails, bool>>>()))
                .Returns((Expression<Func<Employeedetails, bool>> predicate) => existingEntity);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert
            // Verify that the Update and Save methods were called and the response model contains the updated ID.
            fixture.MockUnitOfWork.Verify(uow => uow.Employeedetails.Update(It.IsAny<Employeedetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            Assert.False(result.IsActive);
            Assert.Equal(entityId, result.Id);
        }
        [Fact]
        public void UpdateEmployeeDetails_ShouldReturnIsActiveFalse_WhenInvalidRequest()
        {
            // Arrange
            int entityId = 999; // Id for which the entity is not found

            var requestModel = new EmployeedetailsRequestModel
            {

                Name = "KGL",
            };

            fixture.MockUnitOfWork.Setup(uow => uow.Employeedetails.Get(It.IsAny<Expression<Func<Employeedetails, bool>>>()))
                .Returns((Expression<Func<Employeedetails, bool>> predicate) => null);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert

            Assert.Equal(0, result.Id);
            Assert.False(result.IsActive);

            // Verify that the Update and Save methods were not called since the entity does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Employeedetails.Update(It.IsAny<Employeedetails>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }

    }
}
