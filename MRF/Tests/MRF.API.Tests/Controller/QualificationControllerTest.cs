using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using MRF.API.Controllers;
using Azure.Core;

namespace MRF.API.Test.Controllers
{
    public class QualificationControllerTest
    {
        private readonly TestFixture fixture;
        private QualificationController Controller;

        public QualificationControllerTest()
        {
            fixture = new TestFixture();
            Controller = new QualificationController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object);

        }

        [Fact]
        public void QualificationControllerTest_Constructor_ShouldInitializeDependencies()
        {
            // Assert
            Assert.NotNull(fixture.Controller);

        }

        [Fact]
        public void QualificationController_ShouldReturnCount_WhenDataFound()
        {


            // Create a list of sample Qualificationmaster for testing
            var SampleQualificationDetails = new List<Qualificationmaster>
            {
            new Qualificationmaster { Id=1,  Type="BE",IsActive=true},
            new Qualificationmaster { Id=2,  Type="MCA",IsActive=true},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Qualificationmaster.GetAll()).Returns(SampleQualificationDetails);


            // Act
            var result = Controller.Get();

            // Assert
            Assert.Equal(SampleQualificationDetails.Count, result.Count);
            Assert.NotNull(result);
            result.Should().NotBeNull();
            fixture.MockUnitOfWork.Verify(x => x.Qualificationmaster, Times.Once());
        }


        [Fact]
        public void QualificationController_ShouldReturnNotFound_WhenDataNotFound()
        {

            fixture.MockUnitOfWork.Setup(x => x.Qualificationmaster.GetAll()).Returns(new List<Qualificationmaster>());


            // Act
            var result = Controller.Get();

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
            fixture.MockUnitOfWork.Verify(x => x.Qualificationmaster, Times.Once());
        }


        [Fact]
        public void GetQualificationById_ShouldReturnNoResult_WhenInputIsEqualsZero()
        {
            // Arrange

            int id = 0;

            // Create a list of sample Qualificationmaster for testing
            var SampleQualificationDetails = new List<Qualificationmaster>
            {
            new Qualificationmaster  {Type="MBA",IsActive=true},
            new Qualificationmaster {Type="BE",IsActive=true},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Qualificationmaster.GetAll()).Returns(SampleQualificationDetails);

            // Act
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));


        }
        [Fact]
        public void GetQualificationById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange


            int id = -2;

            // Create a list of sample Qualificationmaster for testing
            var SampleQualificationDetails = new List<Qualificationmaster>
            {
            new Qualificationmaster {Type="BCA",IsActive=true},
            new Qualificationmaster {Type = "BBA",IsActive=true},
                // Add more sample data as needed   
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Qualificationmaster.GetAll()).Returns(SampleQualificationDetails);

            // Act  
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:-2"));
        }
        [Fact]
        public void CreateQualification_ShouldReturnOkResponse_WhenValidRequest()
        {


            var requestModel = new QualificationmasterRequestModel
            {
                 Type = "MCA",
                IsActive = true,
                CreatedOnUtc = DateTime.Now,
                UpdatedByEmployeeId = 1,
                UpdatedOnUtc = DateTime.Now

            };

            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Qualificationmaster.Add(It.IsAny<Qualificationmaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new QualificationmasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(requestModel);

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<QualificationmasterResponseModel>(result);
            var okResult = (QualificationmasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Qualificationmaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Qualificationmaster.Add(It.IsAny<Qualificationmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        }
        [Fact]
        public void CreateQualification_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Qualificationmaster.Add(It.IsAny<Qualificationmaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new QualificationmasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(new QualificationmasterRequestModel());

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<QualificationmasterResponseModel>(result);
            var okResult = (QualificationmasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Qualificationmaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Qualificationmaster.Add(It.IsAny<Qualificationmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            // Assert
            result.Should().NotBeNull();

        }
        [Fact]
        public void DeleteQualification_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange

            int existingId = 1; // Replace with an existing Id in your test data

            // Mock the behavior of the Get and Remove methods in IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Qualificationmaster.Get(It.IsAny<Expression<Func<Qualificationmaster, bool>>>()))
                .Returns((Expression<Func<Qualificationmaster, bool>> filter) =>
                {
                    // Simulate returning an object when the filter condition matches
                    if (filter.Compile().Invoke(new Qualificationmaster { Id = existingId }))
                    {
                        return new Qualificationmaster { Id = existingId };
                    }
                    return null;
                });

            // Act
            Controller.Delete(existingId);


            // Assert
            // Verify that the Remove and Save methods of IUnitOfWork were called as expected
            fixture.MockUnitOfWork.Verify(uow => uow.Qualificationmaster.Get(It.IsAny<Expression<Func<Qualificationmaster, bool>>>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Qualificationmaster.Remove(It.IsAny<Qualificationmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }
        [Fact]
        public void DeleteQualification_ShouldReturnNotFound_WhenRecordNotFound()
        {

            // Arrange


            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 999; // An ID that is assumed not to exist.
            fixture.MockUnitOfWork.Setup(uow => uow.Qualificationmaster.Get(It.IsAny<Expression<Func<Qualificationmaster, bool>>>()))
           .Returns((Expression<Func<Qualificationmaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Qualificationmaster.Remove(It.IsAny<Qualificationmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);

        }
        [Fact]
        public void DeleteQualification_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange

            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 0;
            fixture.MockUnitOfWork.Setup(uow => uow.Qualificationmaster.Get(It.IsAny<Expression<Func<Qualificationmaster, bool>>>()))
           .Returns((Expression<Func<Qualificationmaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Qualificationmaster.Remove(It.IsAny<Qualificationmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact]
        public void UpdateQualification_ShouldReturnIsActiveTrue_WhenRecordIsUpdated()
        {
            // Arrange

            int entityId = 1; // Example ID for an existing entity

            var requestModel = new QualificationmasterRequestModel
            {
                 Type = "BE",
                IsActive = true

            };

            // Mock the behavior of IUnitOfWork's Get method to return an existing entity
            var existingEntity = new Qualificationmaster
            {
                Id = entityId,


            };

            fixture.MockUnitOfWork.Setup(uow => uow.Qualificationmaster.Get(It.IsAny<Expression<Func<Qualificationmaster, bool>>>()))
                .Returns((Expression<Func<Qualificationmaster, bool>> predicate) => existingEntity);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert
            // Verify that the Update and Save methods were called and the response model contains the updated ID.
            fixture.MockUnitOfWork.Verify(uow => uow.Qualificationmaster.Update(It.IsAny<Qualificationmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            Assert.True(result.IsActive);
            Assert.Equal(entityId, result.Id);
        }
        [Fact]
        public void UpdateQualification_ShouldReturnIsActiveFalse_WhenInvalidRequest()
        {
            // Arrange
            int entityId = 999; // Id for which the entity is not found

            var requestModel = new QualificationmasterRequestModel
            {

                 Type = "BCA",
                IsActive = true

            };

            fixture.MockUnitOfWork.Setup(uow => uow.Qualificationmaster.Get(It.IsAny<Expression<Func<Qualificationmaster, bool>>>()))
                .Returns((Expression<Func<Qualificationmaster, bool>> predicate) => null);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert

            Assert.Equal(0, result.Id);
            Assert.False(result.IsActive);

            // Verify that the Update and Save methods were not called since the entity does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Qualificationmaster.Update(It.IsAny<Qualificationmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }

    }
}
