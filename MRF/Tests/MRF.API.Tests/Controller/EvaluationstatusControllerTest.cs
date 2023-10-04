using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using MRF.API.Controllers;

namespace MRF.API.Test.Controllers
{
    public class EvaluationstatusControllerTest
    {
        private readonly TestFixture fixture;
        private EvaluationstatusController Controller;

        public EvaluationstatusControllerTest()
        {
            fixture = new TestFixture();
            Controller = new EvaluationstatusController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object);

        }

        [Fact]
        public void EvaluationstatusControllerTest_Constructor_ShouldInitializeDependencies()
        {
            // Assert
            Assert.NotNull(fixture.Controller);

        }

        [Fact]
        public void EvaluationstatusController_ShouldReturnCount_WhenDataFound()
        {


            // Create a list of sample Evaluationstatusmaster for testing
            var EvaluationstatusDetails = new List<Evaluationstatusmaster>
            {
            new Evaluationstatusmaster { Id=1,  Status="Selected",IsActive=true},
            new Evaluationstatusmaster { Id=2,  Status="not selected",IsActive=true },

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationstatusmaster.GetAll()).Returns(EvaluationstatusDetails);


            // Act
            var result = Controller.Get();

            // Assert
            Assert.Equal(EvaluationstatusDetails.Count, result.Count);
            Assert.NotNull(result);
            result.Should().NotBeNull();
            fixture.MockUnitOfWork.Verify(x => x.Evaluationstatusmaster, Times.Once());
        }


        [Fact]
        public void EvaluationstatusController_ShouldReturnNotFound_WhenDataNotFound()
        {

            fixture.MockUnitOfWork.Setup(x => x.Evaluationstatusmaster.GetAll()).Returns(new List<Evaluationstatusmaster>());


            // Act
            var result = Controller.Get();

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
            fixture.MockUnitOfWork.Verify(x => x.Evaluationstatusmaster, Times.Once());
        }


        [Fact]
        public void GetEvaluationstatusById_ShouldReturnNoResult_WhenInputIsEqualsZero()
        {
            // Arrange

            int id = 0;

            // Create a list of sample Evaluationstatusmaster for testing
            var sampleEvaluationstatusDetails = new List<Evaluationstatusmaster>
            {
            new Evaluationstatusmaster { Status="Selected",IsActive =true},
            new Evaluationstatusmaster {  Status="Not Selected",IsActive =true},
                // Add more sample data as needed
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationstatusmaster.GetAll()).Returns(sampleEvaluationstatusDetails);

            // Act
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));


        }
        [Fact]
        public void GetEvaluationstatusById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange


            int id = 0;

            // Create a list of sample Evaluationstatusmaster for testing
            var sampleEvaluationstatusDetails = new List<Evaluationstatusmaster>
            {
            new Evaluationstatusmaster {  Status="Selected", IsActive =true},
            new Evaluationstatusmaster {   Status="Not Selected",IsActive=true },
                // Add more sample data as needed   
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationstatusmaster.GetAll()).Returns(sampleEvaluationstatusDetails);

            // Act  
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));
        }
        [Fact]
        public void CreateEvaluationstatusDetails_ShouldReturnOkResponse_WhenValidRequest()
        {


            var requestModel = new EvaluationstatusmasterRequestModel
            {
                Status = "Selected",
                IsActive = true,
                CreatedByEmployeeId = 1,
                CreatedOnUtc = DateTime.Now,
                UpdatedByEmployeeId = 1,
                 
            };

            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationstatusmaster.Add(It.IsAny<Evaluationstatusmaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new EvaluationstatusmasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(requestModel);

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<EvaluationstatusmasterResponseModel>(result);
            var okResult = (EvaluationstatusmasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Evaluationstatusmaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Evaluationstatusmaster.Add(It.IsAny<Evaluationstatusmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        }
        [Fact]
        public void CreateEvaluationstatusDetails_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationstatusmaster.Add(It.IsAny<Evaluationstatusmaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new EvaluationstatusmasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(new EvaluationstatusmasterRequestModel());

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<EvaluationstatusmasterResponseModel>(result);
            var okResult = (EvaluationstatusmasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Evaluationstatusmaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Evaluationstatusmaster.Add(It.IsAny<Evaluationstatusmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            // Assert
            result.Should().NotBeNull();

        }
        [Fact]
        public void DeleteEvaluationstatusDetails_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange

            int existingId = 1; // Replace with an existing Id in your test data

            // Mock the behavior of the Get and Remove methods in IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationstatusmaster.Get(It.IsAny<Expression<Func<Evaluationstatusmaster, bool>>>()))
                .Returns((Expression<Func<Evaluationstatusmaster, bool>> filter) =>
                {
                    // Simulate returning an object when the filter condition matches
                    if (filter.Compile().Invoke(new Evaluationstatusmaster { Id = existingId }))
                    {
                        return new Evaluationstatusmaster { Id = existingId };
                    }
                    return null;
                });

            // Act
            Controller.Delete(existingId);


            // Assert
            // Verify that the Remove and Save methods of IUnitOfWork were called as expected
            fixture.MockUnitOfWork.Verify(uow => uow.Evaluationstatusmaster.Get(It.IsAny<Expression<Func<Evaluationstatusmaster, bool>>>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Evaluationstatusmaster.Remove(It.IsAny<Evaluationstatusmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }
        [Fact]
        public void DeleteEvaluationstatusDetails_ShouldReturnNotFound_WhenRecordNotFound()
        {

            // Arrange


            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 999; // An ID that is assumed not to exist.
            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationstatusmaster.Get(It.IsAny<Expression<Func<Evaluationstatusmaster, bool>>>()))
           .Returns((Expression<Func<Evaluationstatusmaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Evaluationstatusmaster.Remove(It.IsAny<Evaluationstatusmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);

        }
        [Fact]
        public void DeleteEvaluationstatusDetails_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange

            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 0;
            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationstatusmaster.Get(It.IsAny<Expression<Func<Evaluationstatusmaster, bool>>>()))
           .Returns((Expression<Func<Evaluationstatusmaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Evaluationstatusmaster.Remove(It.IsAny<Evaluationstatusmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact]
        public void UpdateEvaluationstatusDetails_ShouldReturnIsActiveTrue_WhenRecordIsUpdated()
        {
            // Arrange

            int entityId = 1; // Example ID for an existing entity

            var requestModel = new EvaluationstatusmasterRequestModel
            {

                IsActive = true,
            };

            // Mock the behavior of IUnitOfWork's Get method to return an existing entity
            var existingEntity = new Evaluationstatusmaster
            {
                Id = entityId,
                IsActive = true,
            };

            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationstatusmaster.Get(It.IsAny<Expression<Func<Evaluationstatusmaster, bool>>>()))
                .Returns((Expression<Func<Evaluationstatusmaster, bool>> predicate) => existingEntity);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert
            // Verify that the Update and Save methods were called and the response model contains the updated ID.
            fixture.MockUnitOfWork.Verify(uow => uow.Evaluationstatusmaster.Update(It.IsAny<Evaluationstatusmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            Assert.True(result.IsActive);
            Assert.Equal(entityId, result.Id);
        }
        [Fact]
        public void UpdateEvaluationstatusDetails_ShouldReturnIsActiveFalse_WhenInvalidRequest()
        {
            // Arrange
            int entityId = 999; // Id for which the entity is not found

            var requestModel = new EvaluationstatusmasterRequestModel
            {

                IsActive = true,
            };

            fixture.MockUnitOfWork.Setup(uow => uow.Evaluationstatusmaster.Get(It.IsAny<Expression<Func<Evaluationstatusmaster, bool>>>()))
                .Returns((Expression<Func<Evaluationstatusmaster, bool>> predicate) => null);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert

            Assert.Equal(0, result.Id);
            Assert.False(result.IsActive);

            // Verify that the Update and Save methods were not called since the entity does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Evaluationstatusmaster.Update(It.IsAny<Evaluationstatusmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }

    }
}
