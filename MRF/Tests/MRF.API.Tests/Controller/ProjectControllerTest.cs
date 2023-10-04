using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using MRF.API.Controllers;
using Azure.Core;

namespace MRF.API.Test.Controllers
{
    public class ProjectControllerTest
    {
        private readonly TestFixture fixture;
        private ProjectController Controller;

        public ProjectControllerTest()
        {
            fixture = new TestFixture();
            Controller = new ProjectController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object);

        }

        [Fact]
        public void ProjectControllerTest_Constructor_ShouldInitializeDependencies()
        {
            // Assert
            Assert.NotNull(fixture.Controller);

        }

        [Fact]
        public void ProjectController_ShouldReturnCount_WhenDataFound()
        {


            // Create a list of sample Projectmaster for testing
            var SampleProjectDetails = new List<Projectmaster>
            {
            new Projectmaster { Id=1,  Name="select",IsActive=true},
            new Projectmaster { Id=2, Name="MRF",IsActive=true},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Projectmaster.GetAll()).Returns(SampleProjectDetails);


            // Act
            var result = Controller.Get();

            // Assert
            Assert.Equal(SampleProjectDetails.Count, result.Count);
            Assert.NotNull(result);
            result.Should().NotBeNull();
            fixture.MockUnitOfWork.Verify(x => x.Projectmaster, Times.Once());
        }


        [Fact]
        public void ProjectController_ShouldReturnNotFound_WhenDataNotFound()
        {

            fixture.MockUnitOfWork.Setup(x => x.Projectmaster.GetAll()).Returns(new List<Projectmaster>());


            // Act
            var result = Controller.Get();

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
            fixture.MockUnitOfWork.Verify(x => x.Projectmaster, Times.Once());
        }


        [Fact]
        public void GetProjectById_ShouldReturnNoResult_WhenInputIsEqualsZero()
        {
            // Arrange

            int id = 0;

            // Create a list of sample Projectmaster for testing
            var SampleProjectDetails = new List<Projectmaster>
            {
            new Projectmaster  {Name="sky",IsActive=true},
            new Projectmaster {Name="tootle",IsActive=true},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Projectmaster.GetAll()).Returns(SampleProjectDetails);

            // Act
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));


        }
        [Fact]
        public void GetProjectById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange


            int id = -2;

            // Create a list of sample Projectmaster for testing
            var SampleProjectDetails = new List<Projectmaster>
            {
            new Projectmaster {Name="select",IsActive=true},
            new Projectmaster {Name = "sky",IsActive=true},
                // Add more sample data as needed   
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Projectmaster.GetAll()).Returns(SampleProjectDetails);

            // Act  
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:-2"));
        }
        [Fact]
        public void CreateProject_ShouldReturnOkResponse_WhenValidRequest()
        {


            var requestModel = new ProjectmasterRequestModel
            {
                Name = "select",
                IsActive = true,
                CreatedOnUtc = DateTime.Now,
                UpdatedByEmployeeId = 1,
                UpdatedOnUtc = DateTime.Now

            };

            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Projectmaster.Add(It.IsAny<Projectmaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new ProjectmasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(requestModel);

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<ProjectmasterResponseModel>(result);
            var okResult = (ProjectmasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Projectmaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Projectmaster.Add(It.IsAny<Projectmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        }
        [Fact]
        public void CreateProject_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Projectmaster.Add(It.IsAny<Projectmaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new ProjectmasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(new ProjectmasterRequestModel());

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<ProjectmasterResponseModel>(result);
            var okResult = (ProjectmasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Projectmaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Projectmaster.Add(It.IsAny<Projectmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            // Assert
            result.Should().NotBeNull();

        }
        [Fact]
        public void DeleteProject_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange

            int existingId = 1; // Replace with an existing Id in your test data

            // Mock the behavior of the Get and Remove methods in IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Projectmaster.Get(It.IsAny<Expression<Func<Projectmaster, bool>>>()))
                .Returns((Expression<Func<Projectmaster, bool>> filter) =>
                {
                    // Simulate returning an object when the filter condition matches
                    if (filter.Compile().Invoke(new Projectmaster { Id = existingId }))
                    {
                        return new Projectmaster { Id = existingId };
                    }
                    return null;
                });

            // Act
            Controller.Delete(existingId);


            // Assert
            // Verify that the Remove and Save methods of IUnitOfWork were called as expected
            fixture.MockUnitOfWork.Verify(uow => uow.Projectmaster.Get(It.IsAny<Expression<Func<Projectmaster, bool>>>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Projectmaster.Remove(It.IsAny<Projectmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }
        [Fact]
        public void DeleteProject_ShouldReturnNotFound_WhenRecordNotFound()
        {

            // Arrange


            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 999; // An ID that is assumed not to exist.
            fixture.MockUnitOfWork.Setup(uow => uow.Projectmaster.Get(It.IsAny<Expression<Func<Projectmaster, bool>>>()))
           .Returns((Expression<Func<Projectmaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Projectmaster.Remove(It.IsAny<Projectmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);

        }
        [Fact]
        public void DeleteProject_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange

            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 0;
            fixture.MockUnitOfWork.Setup(uow => uow.Projectmaster.Get(It.IsAny<Expression<Func<Projectmaster, bool>>>()))
           .Returns((Expression<Func<Projectmaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Projectmaster.Remove(It.IsAny<Projectmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact]
        public void UpdateProject_ShouldReturnIsActiveTrue_WhenRecordIsUpdated()
        {
            // Arrange

            int entityId = 1; // Example ID for an existing entity

            var requestModel = new ProjectmasterRequestModel
            {
                 Name = "MRF",
                IsActive = true

            };

            // Mock the behavior of IUnitOfWork's Get method to return an existing entity
            var existingEntity = new Projectmaster
            {
                Id = entityId,


            };

            fixture.MockUnitOfWork.Setup(uow => uow.Projectmaster.Get(It.IsAny<Expression<Func<Projectmaster, bool>>>()))
                .Returns((Expression<Func<Projectmaster, bool>> predicate) => existingEntity);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert
            // Verify that the Update and Save methods were called and the response model contains the updated ID.
            fixture.MockUnitOfWork.Verify(uow => uow.Projectmaster.Update(It.IsAny<Projectmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            Assert.True(result.IsActive);
            Assert.Equal(entityId, result.Id);
        }
        [Fact]
        public void UpdateProject_ShouldReturnIsActiveFalse_WhenInvalidRequest()
        {
            // Arrange
            int entityId = 999; // Id for which the entity is not found

            var requestModel = new ProjectmasterRequestModel
            {

                 Name = "sky",
                IsActive = true

            };

            fixture.MockUnitOfWork.Setup(uow => uow.Projectmaster.Get(It.IsAny<Expression<Func<Projectmaster, bool>>>()))
                .Returns((Expression<Func<Projectmaster, bool>> predicate) => null);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert

            Assert.Equal(0, result.Id);
            Assert.False(result.IsActive);

            // Verify that the Update and Save methods were not called since the entity does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Projectmaster.Update(It.IsAny<Projectmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }

    }
}
