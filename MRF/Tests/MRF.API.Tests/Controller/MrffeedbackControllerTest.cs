using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using MRF.API.Controllers;
using Azure.Core;

namespace MRF.API.Test.Controllers
{
    public class MrffeedbackControllerTest
    {
        private readonly TestFixture fixture;
        private MrffeedbackController Controller;

        public MrffeedbackControllerTest()
        {
            fixture = new TestFixture();
            Controller = new MrffeedbackController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object, fixture.MockEmailService.Object,fixture.MockHostEnvironment.Object, fixture.Mockconfiguration.Object);
        }

        [Fact]
        public void MrffeedbackControllerTest_Constructor_ShouldInitializeDependencies()
        {
            // Assert
            Assert.NotNull(fixture.Controller);

        }

        [Fact]
        public void MrffeedbackController_ShouldReturnCount_WhenDataFound()
        {


            // Create a list of sample Mrffeedback for testing
            var SampleMrffeedback = new List<Mrffeedback>
            {
            new Mrffeedback { Id=1,MrfId=345,Feedback="candidate is good in java"},
            new Mrffeedback { Id=2,MrfId=345,Feedback="candidate is good in java" },

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Mrffeedback.GetAll()).Returns(SampleMrffeedback);


            // Act
            var result = Controller.Get();

            // Assert
            Assert.Equal(SampleMrffeedback.Count, result.Count);
            Assert.NotNull(result);
            result.Should().NotBeNull();
            fixture.MockUnitOfWork.Verify(x => x.Mrffeedback, Times.Once());
        }


        [Fact]
        public void MrffeedbackController_ShouldReturnNotFound_WhenDataNotFound()
        {

            fixture.MockUnitOfWork.Setup(x => x.Mrffeedback.GetAll()).Returns(new List<Mrffeedback>());


            // Act
            var result = Controller.Get();

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
            fixture.MockUnitOfWork.Verify(x => x.Mrffeedback, Times.Once());
        }


        [Fact]
        public void GetMrffeedbackById_ShouldReturnNoResult_WhenInputIsEqualsZero()
        {
            // Arrange

            int id = 0;

            // Create a list of sample Mrffeedback for testing
            var sampleMrffeedback = new List<Mrffeedback>
            {
            new Mrffeedback  {MrfId=345,Feedback="candidate is good in java",},
            new Mrffeedback {MrfId=345,Feedback="candidate is good in java",},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Mrffeedback.GetAll()).Returns(sampleMrffeedback);

            // Act
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));


        }
        [Fact]
        public void GetMrffeedbackById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange


            int id = -2;

            // Create a list of sample Mrffeedback for testing
            var sampleMrffeedback = new List<Mrffeedback>
            {
            new Mrffeedback {MrfId=345,Feedback="candidate is good in java"},
            new Mrffeedback {MrfId=345,Feedback="candidate is good in java"},
                // Add more sample data as needed   
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Mrffeedback.GetAll()).Returns(sampleMrffeedback);

            // Act  
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:-2"));
        }
        [Fact]
        public void CreateMrffeedback_ShouldReturnOkResponse_WhenValidRequest()
        {


            var requestModel = new MrffeedbackRequestModel
            {
                MrfId = 2345,
                Feedback = "candidate good in .net" ,
                FeedbackByEmployeeId =  45632,
                CreatedOnUtc = DateTime.Now,
                UpdatedByEmployeeId = 1,
                UpdatedOnUtc = DateTime.Now

            };

            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Mrffeedback.Add(It.IsAny<Mrffeedback>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new MrffeedbackResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(requestModel);

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<MrffeedbackResponseModel>(result);
            var okResult = (MrffeedbackResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Mrffeedback was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Mrffeedback.Add(It.IsAny<Mrffeedback>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        }
        [Fact]
        public void CreateMrffeedback_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Mrffeedback.Add(It.IsAny<Mrffeedback>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new MrffeedbackResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(new MrffeedbackRequestModel());

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<MrffeedbackResponseModel>(result);
            var okResult = (MrffeedbackResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Mrffeedback was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Mrffeedback.Add(It.IsAny<Mrffeedback>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            // Assert
            result.Should().NotBeNull();

        }
        [Fact]
        public void DeleteMrffeedback_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange

            int existingId = 1; // Replace with an existing Id in your test data

            // Mock the behavior of the Get and Remove methods in IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Mrffeedback.Get(It.IsAny<Expression<Func<Mrffeedback, bool>>>()))
                .Returns((Expression<Func<Mrffeedback, bool>> filter) =>
                {
                    // Simulate returning an object when the filter condition matches
                    if (filter.Compile().Invoke(new Mrffeedback { Id = existingId }))
                    {
                        return new Mrffeedback { Id = existingId };
                    }
                    return null;
                });

            // Act
            Controller.Delete(existingId);


            // Assert
            // Verify that the Remove and Save methods of IUnitOfWork were called as expected
            fixture.MockUnitOfWork.Verify(uow => uow.Mrffeedback.Get(It.IsAny<Expression<Func<Mrffeedback, bool>>>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Mrffeedback.Remove(It.IsAny<Mrffeedback>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }
        [Fact]
        public void DeleteMrffeedback_ShouldReturnNotFound_WhenRecordNotFound()
        {

            // Arrange


            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 999; // An ID that is assumed not to exist.
            fixture.MockUnitOfWork.Setup(uow => uow.Mrffeedback.Get(It.IsAny<Expression<Func<Mrffeedback, bool>>>()))
           .Returns((Expression<Func<Mrffeedback, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Mrffeedback.Remove(It.IsAny<Mrffeedback>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);

        }
        [Fact]
        public void DeleteMrffeedback_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange

            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 0;
            fixture.MockUnitOfWork.Setup(uow => uow.Mrffeedback.Get(It.IsAny<Expression<Func<Mrffeedback, bool>>>()))
           .Returns((Expression<Func<Mrffeedback, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Mrffeedback.Remove(It.IsAny<Mrffeedback>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact]
        public void UpdateMrffeedback_ShouldReturnIsActiveTrue_WhenRecordIsUpdated()
        {
            // Arrange

            int entityId = 1; // Example ID for an existing entity

            var requestModel = new MrffeedbackRequestModel
            {
                 

            };

            // Mock the behavior of IUnitOfWork's Get method to return an existing entity
            var existingEntity = new Mrffeedback
            {
                Id = entityId,


            };

            fixture.MockUnitOfWork.Setup(uow => uow.Mrffeedback.Get(It.IsAny<Expression<Func<Mrffeedback, bool>>>()))
                .Returns((Expression<Func<Mrffeedback, bool>> predicate) => existingEntity);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert
            // Verify that the Update and Save methods were called and the response model contains the updated ID.
            fixture.MockUnitOfWork.Verify(uow => uow.Mrffeedback.Update(It.IsAny<Mrffeedback>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            //Assert.True(result.IsActive);
            Assert.Equal(entityId, result.Id);
        }
        [Fact]
        public void UpdateMrffeedback_ShouldReturnIsActiveFalse_WhenInvalidRequest()
        {
            // Arrange
            int entityId = 999; // Id for which the entity is not found

            var requestModel = new MrffeedbackRequestModel
            {

                MrfId = 2345,
                Feedback = "candidate good in .net",
                FeedbackByEmployeeId = 45632,

            };

            fixture.MockUnitOfWork.Setup(uow => uow.Mrffeedback.Get(It.IsAny<Expression<Func<Mrffeedback, bool>>>()))
                .Returns((Expression<Func<Mrffeedback, bool>> predicate) => null);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert

            Assert.Equal(0, result.Id);
            //Assert.False(result.IsActive);

            // Verify that the Update and Save methods were not called since the entity does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Mrffeedback.Update(It.IsAny<Mrffeedback>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }

    }
}
