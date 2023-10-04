using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using MRF.API.Controllers;
using Azure.Core;

namespace MRF.API.Test.Controllers
{
    public class MrfstatusControllerTest
    {
        private readonly TestFixture fixture;
        private MrfstatusController Controller;

        public MrfstatusControllerTest()
        {
            fixture = new TestFixture();
            Controller = new MrfstatusController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object);

        }

        [Fact]
        public void MrfstatusControllerTest_Constructor_ShouldInitializeDependencies()
        {
            // Assert
            Assert.NotNull(fixture.Controller);

        }

        [Fact]
        public void MrfstatusController_ShouldReturnCount_WhenDataFound()
        {


            // Create a list of sample Mrfstatusmaster for testing
            var SampleMrfstatusDetails = new List<Mrfstatusmaster>
            {
            new Mrfstatusmaster { Id=1, Status="selected",IsActive=true},
            new Mrfstatusmaster { Id=2,Status="selected",IsActive=true},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfstatusmaster.GetAll()).Returns(SampleMrfstatusDetails);


            // Act
            var result = Controller.Get();

            // Assert
            Assert.Equal(SampleMrfstatusDetails.Count, result.Count);
            Assert.NotNull(result);
            result.Should().NotBeNull();
            fixture.MockUnitOfWork.Verify(x => x.Mrfstatusmaster, Times.Once());
        }


        [Fact]
        public void MrfstatusController_ShouldReturnNotFound_WhenDataNotFound()
        {

            fixture.MockUnitOfWork.Setup(x => x.Mrfstatusmaster.GetAll()).Returns(new List<Mrfstatusmaster>());


            // Act
            var result = Controller.Get();

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
            fixture.MockUnitOfWork.Verify(x => x.Mrfstatusmaster, Times.Once());
        }


        [Fact]
        public void GetMrfstatusById_ShouldReturnNoResult_WhenInputIsEqualsZero()
        {
            // Arrange

            int id = 0;

            // Create a list of sample Mrfstatusmaster for testing
            var SampleMrfstatusDetails = new List<Mrfstatusmaster>
            {
            new Mrfstatusmaster  {Status="selected",IsActive=true},
            new Mrfstatusmaster {Status="selected",IsActive=true},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfstatusmaster.GetAll()).Returns(SampleMrfstatusDetails);

            // Act
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));


        }
        [Fact]
        public void GetMrfstatusById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange


            int id = -2;

            // Create a list of sample Mrfstatusmaster for testing
            var SampleMrfstatusDetails = new List<Mrfstatusmaster>
            {
            new Mrfstatusmaster {Status="selected",IsActive=true},
            new Mrfstatusmaster {Status = "selected",IsActive=true},
                // Add more sample data as needed   
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfstatusmaster.GetAll()).Returns(SampleMrfstatusDetails);

            // Act  
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:-2"));
        }
        [Fact]
        public void CreateMrfstatus_ShouldReturnOkResponse_WhenValidRequest()
        {


            var requestModel = new MrfstatusmasterRequestModel
            {
                Status = "selected",
                IsActive = true,
                CreatedOnUtc = DateTime.Now,
                UpdatedByEmployeeId = 1,
                UpdatedOnUtc = DateTime.Now

            };

            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfstatusmaster.Add(It.IsAny<Mrfstatusmaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new MrfstatusmasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(requestModel);

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<MrfstatusmasterResponseModel>(result);
            var okResult = (MrfstatusmasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Mrfstatusmaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfstatusmaster.Add(It.IsAny<Mrfstatusmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        }
        [Fact]
        public void CreateMrfstatus_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfstatusmaster.Add(It.IsAny<Mrfstatusmaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new MrfstatusmasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(new MrfstatusmasterRequestModel());

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<MrfstatusmasterResponseModel>(result);
            var okResult = (MrfstatusmasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Mrfstatusmaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfstatusmaster.Add(It.IsAny<Mrfstatusmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            // Assert
            result.Should().NotBeNull();

        }
        [Fact]
        public void DeleteMrfstatus_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange

            int existingId = 1; // Replace with an existing Id in your test data

            // Mock the behavior of the Get and Remove methods in IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfstatusmaster.Get(It.IsAny<Expression<Func<Mrfstatusmaster, bool>>>()))
                .Returns((Expression<Func<Mrfstatusmaster, bool>> filter) =>
                {
                    // Simulate returning an object when the filter condition matches
                    if (filter.Compile().Invoke(new Mrfstatusmaster { Id = existingId }))
                    {
                        return new Mrfstatusmaster { Id = existingId };
                    }
                    return null;
                });

            // Act
            Controller.Delete(existingId);


            // Assert
            // Verify that the Remove and Save methods of IUnitOfWork were called as expected
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfstatusmaster.Get(It.IsAny<Expression<Func<Mrfstatusmaster, bool>>>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfstatusmaster.Remove(It.IsAny<Mrfstatusmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }
        [Fact]
        public void DeleteMrfstatus_ShouldReturnNotFound_WhenRecordNotFound()
        {

            // Arrange


            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 999; // An ID that is assumed not to exist.
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfstatusmaster.Get(It.IsAny<Expression<Func<Mrfstatusmaster, bool>>>()))
           .Returns((Expression<Func<Mrfstatusmaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfstatusmaster.Remove(It.IsAny<Mrfstatusmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);

        }
        [Fact]
        public void DeleteMrstatus_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange

            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 0;
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfstatusmaster.Get(It.IsAny<Expression<Func<Mrfstatusmaster, bool>>>()))
           .Returns((Expression<Func<Mrfstatusmaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfstatusmaster.Remove(It.IsAny<Mrfstatusmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact]
        public void UpdateMrfstatus_ShouldReturnIsActiveTrue_WhenRecordIsUpdated()
        {
            // Arrange

            int entityId = 1; // Example ID for an existing entity

            var requestModel = new MrfstatusmasterRequestModel
            {
                Status="selected",
                IsActive = true

            };

            // Mock the behavior of IUnitOfWork's Get method to return an existing entity
            var existingEntity = new Mrfstatusmaster
            {
                Id = entityId,


            };

            fixture.MockUnitOfWork.Setup(uow => uow.Mrfstatusmaster.Get(It.IsAny<Expression<Func<Mrfstatusmaster, bool>>>()))
                .Returns((Expression<Func<Mrfstatusmaster, bool>> predicate) => existingEntity);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert
            // Verify that the Update and Save methods were called and the response model contains the updated ID.
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfstatusmaster.Update(It.IsAny<Mrfstatusmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            Assert.True(result.IsActive);
            Assert.Equal(entityId, result.Id);
        }
        [Fact]
        public void UpdateMrfstatus_ShouldReturnIsActiveFalse_WhenInvalidRequest()
        {
            // Arrange
            int entityId = 999; // Id for which the entity is not found

            var requestModel = new MrfstatusmasterRequestModel
            {

                Status="selected",
                IsActive = true

            };

            fixture.MockUnitOfWork.Setup(uow => uow.Mrfstatusmaster.Get(It.IsAny<Expression<Func<Mrfstatusmaster, bool>>>()))
                .Returns((Expression<Func<Mrfstatusmaster, bool>> predicate) => null);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert

            Assert.Equal(0, result.Id);
            Assert.False(result.IsActive);

            // Verify that the Update and Save methods were not called since the entity does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfstatusmaster.Update(It.IsAny<Mrfstatusmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }

    }
}
