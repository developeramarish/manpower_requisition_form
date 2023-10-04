using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using MRF.API.Controllers;
using Azure.Core;

namespace MRF.API.Test.Controllers
{
    public class LocationControllerTest
    {
        private readonly TestFixture fixture;
        private LocationController Controller;

        public LocationControllerTest()
        {
            fixture = new TestFixture();
            Controller = new LocationController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object);

        }

        [Fact]
        public void LocationControllerTest_Constructor_ShouldInitializeDependencies()
        {
            // Assert
            Assert.NotNull(fixture.Controller);

        }

        [Fact]
        public void LocationController_ShouldReturnCount_WhenDataFound()
        {


            // Create a list of sample Locationmaster for testing
            var SampleLocationDetails = new List<Locationmaster>
            {
            new Locationmaster { Id=1, Location="Mumbai",ShortCode="Mum", IsActive = true,},
            new Locationmaster { Id=2, Location = "Noida",ShortCode="Noi", IsActive = true,},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Locationmaster.GetAll()).Returns(SampleLocationDetails);


            // Act
            var result = Controller.Get();

            // Assert
            Assert.Equal(SampleLocationDetails.Count, result.Count);
            Assert.NotNull(result);
            result.Should().NotBeNull();
            fixture.MockUnitOfWork.Verify(x => x.Locationmaster, Times.Once());
        }


        [Fact]
        public void LocationController_ShouldReturnNotFound_WhenDataNotFound()
        {

            fixture.MockUnitOfWork.Setup(x => x.Locationmaster.GetAll()).Returns(new List<Locationmaster>());


            // Act
            var result = Controller.Get();

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
            fixture.MockUnitOfWork.Verify(x => x.Locationmaster, Times.Once());
        }


        [Fact]
        public void GetLocationdetailById_ShouldReturnNoResult_WhenInputIsEqualsZero()
        {
            // Arrange

            int id = 0;

            // Create a list of sample Locationmaster for testing
            var sampleLocationdetails = new List<Locationmaster>
            {
            new Locationmaster  {Location = "Mumbai",ShortCode="Mum", IsActive = true,},
            new Locationmaster {Location = "Mumbai",ShortCode="Mum", IsActive = true,},
                // Add more sample data as needed
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Locationmaster.GetAll()).Returns(sampleLocationdetails);

            // Act
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));


        }
        [Fact]
        public void GetLocationdetailsById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange


            int id = 0;

            // Create a list of sample Locationmaster for testing
            var sampleLocationdetails = new List<Locationmaster>
            {
            new Locationmaster {Location = "Mumbai",ShortCode="Mum", IsActive = true,},
            new Locationmaster {Location = "Mumbai",ShortCode="Mum", IsActive = true,},
                // Add more sample data as needed   
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Locationmaster.GetAll()).Returns(sampleLocationdetails);

            // Act  
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));
        }
        [Fact]
        public void CreateLocationdetail_ShouldReturnOkResponse_WhenValidRequest()
        {


            var requestModel = new LocationmasterRequestModel
            {
                 Location = "Bangalore",
                 ShortCode="Ban",
                IsActive = true,
                CreatedByEmployeeId = 1,
                CreatedOnUtc = DateTime.Now,
                UpdatedByEmployeeId = 1,

            };

            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Locationmaster.Add(It.IsAny<Locationmaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new LocationmasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(requestModel);

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<LocationmasterResponseModel>(result);
            var okResult = (LocationmasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Locationmaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Locationmaster.Add(It.IsAny<Locationmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        }
        [Fact]
        public void CreateLocationdetails_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Locationmaster.Add(It.IsAny<Locationmaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new LocationmasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(new LocationmasterRequestModel());

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<LocationmasterResponseModel>(result);
            var okResult = (LocationmasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Locationmaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Locationmaster.Add(It.IsAny<Locationmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            // Assert
            result.Should().NotBeNull();

        }
        [Fact]
        public void DeleteLocationdetails_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange

            int existingId = 1; // Replace with an existing Id in your test data

            // Mock the behavior of the Get and Remove methods in IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Locationmaster.Get(It.IsAny<Expression<Func<Locationmaster, bool>>>()))
                .Returns((Expression<Func<Locationmaster, bool>> filter) =>
                {
                    // Simulate returning an object when the filter condition matches
                    if (filter.Compile().Invoke(new Locationmaster { Id = existingId }))
                    {
                        return new Locationmaster { Id = existingId };
                    }
                    return null;
                });

            // Act
            Controller.Delete(existingId);


            // Assert
            // Verify that the Remove and Save methods of IUnitOfWork were called as expected
            fixture.MockUnitOfWork.Verify(uow => uow.Locationmaster.Get(It.IsAny<Expression<Func<Locationmaster, bool>>>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Locationmaster.Remove(It.IsAny<Locationmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }
        [Fact]
        public void DeleteLocationdetails_ShouldReturnNotFound_WhenRecordNotFound()
        {

            // Arrange


            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 999; // An ID that is assumed not to exist.
            fixture.MockUnitOfWork.Setup(uow => uow.Locationmaster.Get(It.IsAny<Expression<Func<Locationmaster, bool>>>()))
           .Returns((Expression<Func<Locationmaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Locationmaster.Remove(It.IsAny<Locationmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);

        }
        [Fact]
        public void DeleteLocationdetails_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange

            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 0;
            fixture.MockUnitOfWork.Setup(uow => uow.Locationmaster.Get(It.IsAny<Expression<Func<Locationmaster, bool>>>()))
           .Returns((Expression<Func<Locationmaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Locationmaster.Remove(It.IsAny<Locationmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact]
        public void UpdateLocationdetails_ShouldReturnIsActiveTrue_WhenRecordIsUpdated()
        {
            // Arrange

            int entityId = 1; // Example ID for an existing entity

            var requestModel = new LocationmasterRequestModel
            {
                 Location = "sdfg",
                 ShortCode="1",
                IsActive = true,

            };

            // Mock the behavior of IUnitOfWork's Get method to return an existing entity
            var existingEntity = new Locationmaster
            {
                Id = entityId,
                IsActive = true,

            };

            fixture.MockUnitOfWork.Setup(uow => uow.Locationmaster.Get(It.IsAny<Expression<Func<Locationmaster, bool>>>()))
                .Returns((Expression<Func<Locationmaster, bool>> predicate) => existingEntity);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert
            // Verify that the Update and Save methods were called and the response model contains the updated ID.
            fixture.MockUnitOfWork.Verify(uow => uow.Locationmaster.Update(It.IsAny<Locationmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            Assert.True(result.IsActive);
            Assert.Equal(entityId, result.Id);
        }
        [Fact]
        public void UpdateLocationdetails_ShouldReturnIsActiveFalse_WhenInvalidRequest()
        {
            // Arrange
            int entityId = 999; // Id for which the entity is not found

            var requestModel = new LocationmasterRequestModel
            {

                 Location = "dfg",

            };

            fixture.MockUnitOfWork.Setup(uow => uow.Locationmaster.Get(It.IsAny<Expression<Func<Locationmaster, bool>>>()))
                .Returns((Expression<Func<Locationmaster, bool>> predicate) => null);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert

            Assert.Equal(0, result.Id);
            Assert.False(result.IsActive);

            // Verify that the Update and Save methods were not called since the entity does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Locationmaster.Update(It.IsAny<Locationmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }

    }
}
