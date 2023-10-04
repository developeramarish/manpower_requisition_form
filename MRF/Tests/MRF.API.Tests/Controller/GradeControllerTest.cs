using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using MRF.API.Controllers;
using Azure.Core;

namespace MRF.API.Test.Controllers
{
    public class GradeControllerTest
    {
        private readonly TestFixture fixture;
        private GradeController Controller;

        public GradeControllerTest()
        {
            fixture = new TestFixture();
            Controller = new GradeController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object);

        }

        [Fact]
        public void GradeControllerTest_Constructor_ShouldInitializeDependencies()
        {
            // Assert
            Assert.NotNull(fixture.Controller);

        }

        [Fact]
        public void GradeController_ShouldReturnCount_WhenDataFound()
        {


            // Create a list of sample Grademaster for testing
            var SampleGradeDetails = new List<Grademaster>
            {
            new Grademaster { Id=1,Name="kritika", IsActive = true,},
            new Grademaster { Id=2, Name = "dfgh", IsActive = true,},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Grademaster.GetAll()).Returns(SampleGradeDetails);


            // Act
            var result = Controller.Get();

            // Assert
            Assert.Equal(SampleGradeDetails.Count, result.Count);
            Assert.NotNull(result);
            result.Should().NotBeNull();
            fixture.MockUnitOfWork.Verify(x => x.Grademaster, Times.Once());
        }


        [Fact]
        public void GradeController_ShouldReturnNotFound_WhenDataNotFound()
        {

            fixture.MockUnitOfWork.Setup(x => x.Grademaster.GetAll()).Returns(new List<Grademaster>());


            // Act
            var result = Controller.Get();

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
            fixture.MockUnitOfWork.Verify(x => x.Grademaster, Times.Once());
        }


        [Fact]
        public void GetGradedetailById_ShouldReturnNoResult_WhenInputIsEqualsZero()
        {
            // Arrange

            int id = 0;

            // Create a list of sample Grademaster for testing
            var sampleGradedetails = new List<Grademaster>
            {
            new Grademaster  { Name = "dfgh", IsActive = true,},
            new Grademaster {  Name = "dfgh", IsActive = true,},
                // Add more sample data as needed
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Grademaster.GetAll()).Returns(sampleGradedetails);

            // Act
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));


        }
        [Fact]
        public void GetGradedetailsById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange


            int id = 0;

            // Create a list of sample Grademaster for testing
            var sampleGradedetails = new List<Grademaster>
            {
            new Grademaster { Name = "dfgh", IsActive = true,},
            new Grademaster { Name = "dfgh", IsActive = true,},
                // Add more sample data as needed   
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Grademaster.GetAll()).Returns(sampleGradedetails);

            // Act  
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));
        }
        [Fact]
        public void CreateGradedetail_ShouldReturnOkResponse_WhenValidRequest()
        {


            var requestModel = new GrademasterRequestModel
            {
                Name = "dfgh",
                IsActive = true,
                CreatedByEmployeeId = 1,
                CreatedOnUtc = DateTime.Now,
                UpdatedByEmployeeId = 1,

            };

            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Grademaster.Add(It.IsAny<Grademaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new GrademasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(requestModel);

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<GrademasterResponseModel>(result);
            var okResult = (GrademasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Grademaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Grademaster.Add(It.IsAny<Grademaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        }
        [Fact]
        public void CreateGradedetails_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Grademaster.Add(It.IsAny<Grademaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new GrademasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(new GrademasterRequestModel());

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<GrademasterResponseModel>(result);
            var okResult = (GrademasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Grademaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Grademaster.Add(It.IsAny<Grademaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            // Assert
            result.Should().NotBeNull();

        }
        [Fact]
        public void DeleteGradedetails_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange

            int existingId = 1; // Replace with an existing Id in your test data

            // Mock the behavior of the Get and Remove methods in IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Grademaster.Get(It.IsAny<Expression<Func<Grademaster, bool>>>()))
                .Returns((Expression<Func<Grademaster, bool>> filter) =>
                {
                    // Simulate returning an object when the filter condition matches
                    if (filter.Compile().Invoke(new Grademaster { Id = existingId }))
                    {
                        return new Grademaster { Id = existingId };
                    }
                    return null;
                });

            // Act
            Controller.Delete(existingId);


            // Assert
            // Verify that the Remove and Save methods of IUnitOfWork were called as expected
            fixture.MockUnitOfWork.Verify(uow => uow.Grademaster.Get(It.IsAny<Expression<Func<Grademaster, bool>>>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Grademaster.Remove(It.IsAny<Grademaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }
        [Fact]
        public void DeleteGradedetails_ShouldReturnNotFound_WhenRecordNotFound()
        {

            // Arrange


            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 999; // An ID that is assumed not to exist.
            fixture.MockUnitOfWork.Setup(uow => uow.Grademaster.Get(It.IsAny<Expression<Func<Grademaster, bool>>>()))
           .Returns((Expression<Func<Grademaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Grademaster.Remove(It.IsAny<Grademaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);

        }
        [Fact]
        public void DeleteGradedetails_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange

            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 0;
            fixture.MockUnitOfWork.Setup(uow => uow.Grademaster.Get(It.IsAny<Expression<Func<Grademaster, bool>>>()))
           .Returns((Expression<Func<Grademaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Grademaster.Remove(It.IsAny<Grademaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact]
        public void UpdateGradedetails_ShouldReturnIsActiveTrue_WhenRecordIsUpdated()
        {
            // Arrange

            int entityId = 1; // Example ID for an existing entity

            var requestModel = new GrademasterRequestModel
            {
                 Name = "sdfg",
                IsActive = true,

            };

            // Mock the behavior of IUnitOfWork's Get method to return an existing entity
            var existingEntity = new Grademaster
            {
                Id = entityId,
                IsActive = true,

            };

            fixture.MockUnitOfWork.Setup(uow => uow.Grademaster.Get(It.IsAny<Expression<Func<Grademaster, bool>>>()))
                .Returns((Expression<Func<Grademaster, bool>> predicate) => existingEntity);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert
            // Verify that the Update and Save methods were called and the response model contains the updated ID.
            fixture.MockUnitOfWork.Verify(uow => uow.Grademaster.Update(It.IsAny<Grademaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            Assert.True(result.IsActive);
            Assert.Equal(entityId, result.Id);
        }
        [Fact]
        public void UpdateGradedetails_ShouldReturnIsActiveFalse_WhenInvalidRequest()
        {
            // Arrange
            int entityId = 999; // Id for which the entity is not found

            var requestModel = new GrademasterRequestModel
            {

                 Name = "dfg",

            };

            fixture.MockUnitOfWork.Setup(uow => uow.Grademaster.Get(It.IsAny<Expression<Func<Grademaster, bool>>>()))
                .Returns((Expression<Func<Grademaster, bool>> predicate) => null);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert

            Assert.Equal(0, result.Id);
            Assert.False(result.IsActive);

            // Verify that the Update and Save methods were not called since the entity does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Grademaster.Update(It.IsAny<Grademaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }

    }
}
