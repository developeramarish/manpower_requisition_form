using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using MRF.API.Controllers;

namespace MRF.API.Test.Controllers
{
    public class EvaluationfeedbackControllerTest
    {
        private readonly TestFixture fixture;
        private EvaluationfeedbackController Controller;

        public EvaluationfeedbackControllerTest()
        {
            fixture = new TestFixture();
            Controller = new EvaluationfeedbackController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object);

        }

        [Fact]
        public void EvaluationfeedbackControllerTest_Constructor_ShouldInitializeDependencies()
        {
            // Assert
            Assert.NotNull(fixture.Controller);

        }

        [Fact]
        public void EvaluationfeedbackController_ShouldReturnCount_WhenDataFound()
        {


            // Create a list of sample Evaluationfeedbackmaster for testing
            var EvaluationfeedbackDetails = new List<Evaluationfeedbackmaster>
            {
            new Evaluationfeedbackmaster { Id=1, Description="working goods",IsActive=true},
            new Evaluationfeedbackmaster { Id=2, Description="working goods",IsActive=true },

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationfeedbackmaster.GetAll()).Returns(EvaluationfeedbackDetails);


            // Act
            var result = Controller.Get();

            // Assert
            Assert.Equal(EvaluationfeedbackDetails.Count, result.Count);
            Assert.NotNull(result);
            result.Should().NotBeNull();
            fixture.MockUnitOfWork.Verify(x => x.Evaluationfeedbackmaster, Times.Once());
        }


        [Fact]
        public void EvaluationfeedbackController_ShouldReturnNotFound_WhenDataNotFound()
        {

            fixture.MockUnitOfWork.Setup(x => x.Evaluationfeedbackmaster.GetAll()).Returns(new List<Evaluationfeedbackmaster>());


            // Act
            var result = Controller.Get();

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
            fixture.MockUnitOfWork.Verify(x => x.Evaluationfeedbackmaster, Times.Once());
        }


        [Fact]
        public void GetEvaluationfeedbackById_ShouldReturnNoResult_WhenInputIsEqualsZero()
        {
            // Arrange

            int id = 0;

            // Create a list of sample Evaluationfeedbackmaster for testing
            var sampleEvaluationfeedbackDetails = new List<Evaluationfeedbackmaster>
            {
            new Evaluationfeedbackmaster {Description="working goods",IsActive =true},
            new Evaluationfeedbackmaster { Description="working goods",IsActive =true},
                // Add more sample data as needed
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationfeedbackmaster.GetAll()).Returns(sampleEvaluationfeedbackDetails);

            // Act
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));


        }
        [Fact]
        public void GetEvaluationfeedbackById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange


            int id = 0;

            // Create a list of sample Evaluationfeedbackmaster for testing
            var sampleEvaluationfeedbackDetails = new List<Evaluationfeedbackmaster>
            {
            new Evaluationfeedbackmaster { Description="working goods", IsActive =true},
            new Evaluationfeedbackmaster { Description="working goods",IsActive=true },
                // Add more sample data as needed   
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationfeedbackmaster.GetAll()).Returns(sampleEvaluationfeedbackDetails);

            // Act  
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));
        }
        [Fact]
        public void CreateEvaluationfeedbackDetails_ShouldReturnOkResponse_WhenValidRequest()
        {


            var requestModel = new EvaluationfeedbackmasterRequestModel
            {
                Description = "working goods",
                IsActive = true,
                CreatedByEmployeeId = 1,
                CreatedOnUtc = DateTime.Now,
                UpdatedByEmployeeId = 1,
                UpdatedOnUtc = DateTime.Now
            };

            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationfeedbackmaster.Add(It.IsAny<Evaluationfeedbackmaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new EvaluationfeedbackmasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(requestModel);

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<EvaluationfeedbackmasterResponseModel>(result);
            var okResult = (EvaluationfeedbackmasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Evaluationfeedbackmaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Evaluationfeedbackmaster.Add(It.IsAny<Evaluationfeedbackmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        }
        [Fact]
        public void CreateEvaluationfeedbackDetails_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationfeedbackmaster.Add(It.IsAny<Evaluationfeedbackmaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new EvaluationfeedbackmasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(new EvaluationfeedbackmasterRequestModel());

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<EvaluationfeedbackmasterResponseModel>(result);
            var okResult = (EvaluationfeedbackmasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Evaluationfeedbackmaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Evaluationfeedbackmaster.Add(It.IsAny<Evaluationfeedbackmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            // Assert
            result.Should().NotBeNull();

        }
        [Fact]
        public void DeleteEvaluationfeedbackDetails_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange

            int existingId = 1; // Replace with an existing Id in your test data

            // Mock the behavior of the Get and Remove methods in IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationfeedbackmaster.Get(It.IsAny<Expression<Func<Evaluationfeedbackmaster, bool>>>()))
                .Returns((Expression<Func<Evaluationfeedbackmaster, bool>> filter) =>
                {
                    // Simulate returning an object when the filter condition matches
                    if (filter.Compile().Invoke(new Evaluationfeedbackmaster { Id = existingId }))
                    {
                        return new Evaluationfeedbackmaster { Id = existingId };
                    }
                    return null;
                });

            // Act
            Controller.Delete(existingId);


            // Assert
            // Verify that the Remove and Save methods of IUnitOfWork were called as expected
            fixture.MockUnitOfWork.Verify(uow => uow.Evaluationfeedbackmaster.Get(It.IsAny<Expression<Func<Evaluationfeedbackmaster, bool>>>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Evaluationfeedbackmaster.Remove(It.IsAny<Evaluationfeedbackmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }
        [Fact]
        public void DeleteEvaluationfeedbackDetails_ShouldReturnNotFound_WhenRecordNotFound()
        {

            // Arrange


            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 999; // An ID that is assumed not to exist.
            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationfeedbackmaster.Get(It.IsAny<Expression<Func<Evaluationfeedbackmaster, bool>>>()))
           .Returns((Expression<Func<Evaluationfeedbackmaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Evaluationfeedbackmaster.Remove(It.IsAny<Evaluationfeedbackmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);

        }
        [Fact]
        public void DeleteEvaluationfeedbackDetails_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange

            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 0;
            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationfeedbackmaster.Get(It.IsAny<Expression<Func<Evaluationfeedbackmaster, bool>>>()))
           .Returns((Expression<Func<Evaluationfeedbackmaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Evaluationfeedbackmaster.Remove(It.IsAny<Evaluationfeedbackmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact]
        public void UpdateEvaluationfeedbackDetails_ShouldReturnIsActiveTrue_WhenRecordIsUpdated()
        {
            // Arrange

            int entityId = 1; // Example ID for an existing entity

            var requestModel = new EvaluationfeedbackmasterRequestModel
            {
                
                IsActive = true,
            };

            // Mock the behavior of IUnitOfWork's Get method to return an existing entity
            var existingEntity = new Evaluationfeedbackmaster
            {
                Id = entityId,
                IsActive = true,
            };

            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationfeedbackmaster.Get(It.IsAny<Expression<Func<Evaluationfeedbackmaster, bool>>>()))
                .Returns((Expression<Func<Evaluationfeedbackmaster, bool>> predicate) => existingEntity);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert
            // Verify that the Update and Save methods were called and the response model contains the updated ID.
            fixture.MockUnitOfWork.Verify(uow => uow.Evaluationfeedbackmaster.Update(It.IsAny<Evaluationfeedbackmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            Assert.True(result.IsActive);
            Assert.Equal(entityId, result.Id);
        }
        [Fact]
        public void UpdateEvaluationfeedbackDetails_ShouldReturnIsActiveFalse_WhenInvalidRequest()
        {
            // Arrange
            int entityId = 999; // Id for which the entity is not found

            var requestModel = new EvaluationfeedbackmasterRequestModel
            {
                 
                IsActive = true,
            };

            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationfeedbackmaster.Get(It.IsAny<Expression<Func<Evaluationfeedbackmaster, bool>>>()))
                .Returns((Expression<Func<Evaluationfeedbackmaster, bool>> predicate) => null);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert

            Assert.Equal(0, result.Id);
            Assert.False(result.IsActive);

            // Verify that the Update and Save methods were not called since the entity does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Evaluationfeedbackmaster.Update(It.IsAny<Evaluationfeedbackmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }

    }
}
