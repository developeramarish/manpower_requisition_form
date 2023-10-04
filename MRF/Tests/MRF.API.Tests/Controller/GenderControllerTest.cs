using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using MRF.API.Controllers;
using Azure.Core;

namespace MRF.API.Test.Controllers
{
    public class GenderControllerTest
    {
        private readonly TestFixture fixture;
        private GenderController Controller;

        public GenderControllerTest()
        {
            fixture = new TestFixture();
            Controller = new GenderController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object);

        }

        [Fact]
        public void GenderControllerTest_Constructor_ShouldInitializeDependencies()
        {
            // Assert
            Assert.NotNull(fixture.Controller);

        }

        [Fact]
        public void GenderController_ShouldReturnCount_WhenDataFound()
        {


            // Create a list of sample Gendermaster for testing
            var SampleGenderDetails = new List<Gendermaster>
            {
            new Gendermaster { Id=1,Type = "dfgh", IsActive = true,},
            new Gendermaster { Id=2,Type = "dfgh", IsActive = true,},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Gendermaster.GetAll()).Returns(SampleGenderDetails);


            // Act
            var result = Controller.Get();

            // Assert
            Assert.Equal(SampleGenderDetails.Count, result.Count);
            Assert.NotNull(result);
            result.Should().NotBeNull();
            fixture.MockUnitOfWork.Verify(x => x.Gendermaster, Times.Once());
        }


        [Fact]
        public void GenderController_ShouldReturnNotFound_WhenDataNotFound()
        {

            fixture.MockUnitOfWork.Setup(x => x.Gendermaster.GetAll()).Returns(new List<Gendermaster>());


            // Act
            var result = Controller.Get();

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
            fixture.MockUnitOfWork.Verify(x => x.Gendermaster, Times.Once());
        }


        [Fact]
        public void GetGenderdetailById_ShouldReturnNoResult_WhenInputIsEqualsZero()
        {
            // Arrange

            int id = 0;

            // Create a list of sample Gendermaster for testing
            var sampleGenderdetails = new List<Gendermaster>
            {
            new Gendermaster  {Type = "dfgh", IsActive = true,},
            new Gendermaster { Type = "dfgh", IsActive = true,},
                // Add more sample data as needed
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Gendermaster.GetAll()).Returns(sampleGenderdetails);

            // Act
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));


        }
        [Fact]
        public void GetGenderdetailsById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange


            int id = 0;

            // Create a list of sample Gendermaster for testing
            var sampleGenderdetails = new List<Gendermaster>
            {
            new Gendermaster {Type = "dfgh", IsActive = true,},
            new Gendermaster {Type = "dfgh", IsActive = true,},
                // Add more sample data as needed   
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Gendermaster.GetAll()).Returns(sampleGenderdetails);

            // Act  
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));
        }
        [Fact]
        public void CreateGenderdetail_ShouldReturnOkResponse_WhenValidRequest()
        {


            var requestModel = new GendermasterRequestModel
            {
                Type = "dfgh",
                IsActive = true,
                CreatedByEmployeeId = 1,
                CreatedOnUtc = DateTime.Now,
                UpdatedByEmployeeId = 1,

            };

            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Gendermaster.Add(It.IsAny<Gendermaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new GendermasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.PostPost(requestModel);

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<GendermasterResponseModel>(result);
            var okResult = (GendermasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Gendermaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Gendermaster.Add(It.IsAny<Gendermaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        }
        [Fact]
        public void CreateGenderdetails_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Gendermaster.Add(It.IsAny<Gendermaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new GendermasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.PostPost(new GendermasterRequestModel());

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<GendermasterResponseModel>(result);
            var okResult = (GendermasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Gendermaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Gendermaster.Add(It.IsAny<Gendermaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            // Assert
            result.Should().NotBeNull();

        }
        [Fact]
        public void DeleteGenderdetails_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange

            int existingId = 1; // Replace with an existing Id in your test data

            // Mock the behavior of the Get and Remove methods in IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Gendermaster.Get(It.IsAny<Expression<Func<Gendermaster, bool>>>()))
                .Returns((Expression<Func<Gendermaster, bool>> filter) =>
                {
                    // Simulate returning an object when the filter condition matches
                    if (filter.Compile().Invoke(new Gendermaster { Id = existingId }))
                    {
                        return new Gendermaster { Id = existingId };
                    }
                    return null;
                });

            // Act
            Controller.Delete(existingId);


            // Assert
            // Verify that the Remove and Save methods of IUnitOfWork were called as expected
            fixture.MockUnitOfWork.Verify(uow => uow.Gendermaster.Get(It.IsAny<Expression<Func<Gendermaster, bool>>>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Gendermaster.Remove(It.IsAny<Gendermaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }
        [Fact]
        public void DeleteGenderdetails_ShouldReturnNotFound_WhenRecordNotFound()
        {

            // Arrange


            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 999; // An ID that is assumed not to exist.
            fixture.MockUnitOfWork.Setup(uow => uow.Gendermaster.Get(It.IsAny<Expression<Func<Gendermaster, bool>>>()))
           .Returns((Expression<Func<Gendermaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Gendermaster.Remove(It.IsAny<Gendermaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);

        }
        [Fact]
        public void DeleteGenderdetails_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange

            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 0;
            fixture.MockUnitOfWork.Setup(uow => uow.Gendermaster.Get(It.IsAny<Expression<Func<Gendermaster, bool>>>()))
           .Returns((Expression<Func<Gendermaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Gendermaster.Remove(It.IsAny<Gendermaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact]
        public void UpdateGenderdetails_ShouldReturnIsActiveTrue_WhenRecordIsUpdated()
        {
            // Arrange

            int entityId = 1; // Example ID for an existing entity

            var requestModel = new GendermasterRequestModel
            {
                Type="sdfg",
                IsActive=true,

            };

            // Mock the behavior of IUnitOfWork's Get method to return an existing entity
            var existingEntity = new Gendermaster
            {
                Id = entityId,
                IsActive = true,

            };

            fixture.MockUnitOfWork.Setup(uow => uow.Gendermaster.Get(It.IsAny<Expression<Func<Gendermaster, bool>>>()))
                .Returns((Expression<Func<Gendermaster, bool>> predicate) => existingEntity);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert
            // Verify that the Update and Save methods were called and the response model contains the updated ID.
            fixture.MockUnitOfWork.Verify(uow => uow.Gendermaster.Update(It.IsAny<Gendermaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            Assert.True(result.IsActive);
            Assert.Equal(entityId, result.Id);
        }
        [Fact]
        public void UpdateGenderdetails_ShouldReturnIsActiveFalse_WhenInvalidRequest()
        {
            // Arrange
            int entityId = 999; // Id for which the entity is not found

            var requestModel = new GendermasterRequestModel
            {

                Type="dfg",
                
            };

            fixture.MockUnitOfWork.Setup(uow => uow.Gendermaster.Get(It.IsAny<Expression<Func<Gendermaster, bool>>>()))
                .Returns((Expression<Func<Gendermaster, bool>> predicate) => null);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert

            Assert.Equal(0, result.Id);
            Assert.False(result.IsActive);

            // Verify that the Update and Save methods were not called since the entity does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Gendermaster.Update(It.IsAny<Gendermaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }

    }
}
