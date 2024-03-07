using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;

namespace MRF.API.Test.Controllers
{
    public class CandidatedetailControllerTests
    {
        private readonly TestFixture fixture;

        public CandidatedetailControllerTests()
        {
            fixture = new TestFixture();

        }

        [Fact]
        public void CandidatedetailController_Constructor_ShouldInitializeDependencies()
        {
            // Assert
            Assert.NotNull(fixture.Controller);

        }

        [Fact]
        public void CandidatedetailController_ShouldReturnCount_WhenDataFound()
        {


            // Create a list of sample Candidatedetails for testing
            var sampleCandidateDetails = new List<Candidatedetails>
            {
            new Candidatedetails { Id = 1, Name = "John Doe" },
            new Candidatedetails { Id = 2, Name = "Jane Smith" },
                // Add more sample data as needed
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Candidatedetail.GetAll()).Returns(sampleCandidateDetails);


            // Act
            var result = fixture.Controller.Get();

            // Assert
            Assert.Equal(sampleCandidateDetails.Count, result.Count);
            Assert.NotNull(result);
            result.Should().NotBeNull();
            fixture.MockUnitOfWork.Verify(x => x.Candidatedetail, Times.Once());
        }


        [Fact]
        public void CandidatedetailController_ShouldReturnNotFound_WhenDataNotFound()
        {

            fixture.MockUnitOfWork.Setup(x => x.Candidatedetail.GetAll()).Returns(new List<Candidatedetails>());


            // Act
            var result = fixture.Controller.Get();

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
            fixture.MockUnitOfWork.Verify(x => x.Candidatedetail, Times.Once());
        }


        [Fact]
        public void GetCandidatedetailById_ShouldReturnNoResult_WhenInputIsEqualsZero()
        {
            // Arrange

            int id = 0;

            // Create a list of sample Candidatedetails for testing
            var sampleCandidateDetails = new List<Candidatedetails>
            {
            new Candidatedetails { Id = 1, Name = "John Doe" },
            new Candidatedetails { Id = 2, Name = "Jane Smith" },
                // Add more sample data as needed
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Candidatedetail.GetAll()).Returns(sampleCandidateDetails);

            // Act
            var result = fixture.Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));


        }
        [Fact]
        public void GetCandidatedetailById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange


            int id = 0;

            // Create a list of sample Candidatedetails for testing
            var sampleCandidateDetails = new List<Candidatedetails>
            {
            new Candidatedetails { Id = 1, Name = "John Doe" },
            new Candidatedetails { Id = 2, Name = "Jane Smith" },
                // Add more sample data as needed
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Candidatedetail.GetAll()).Returns(sampleCandidateDetails);

            // Act  
            var result = fixture.Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));
        }
        [Fact(Skip = "check later")]
        public void CreateCandidatedetail_ShouldReturnOkResponse_WhenValidRequest()
        {


            var requestModel = new CandidatedetailRequestModel
            {
                Id = 1,
                Name = "ABc",
                MrfId = 1,
                EmailId = "amita@kwglobal.com",
                ContactNo = "1234567890",
                ResumePath = "cdd",
                ReviewedByEmployeeId = 1,
                CandidateStatusId = 1,
                CreatedByEmployeeId = 1,
                CreatedOnUtc = DateTime.Now,
                UpdatedByEmployeeId = 1,
                UpdatedOnUtc = DateTime.Now
            };

            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Candidatedetail.Add(It.IsAny<Candidatedetails>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new CandidatedetailResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = fixture.Controller.Post(requestModel);

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<CandidatedetailResponseModel>(result);
            var okResult = (CandidatedetailResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Candidatedetail was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Candidatedetail.Add(It.IsAny<Candidatedetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        }
        [Fact(Skip = "check later")]
        public void CreateCandidatedetail_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Candidatedetail.Add(It.IsAny<Candidatedetails>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new CandidatedetailResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = fixture.Controller.Post(new CandidatedetailRequestModel());

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<CandidatedetailResponseModel>(result);
            var okResult = (CandidatedetailResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Candidatedetail was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Candidatedetail.Add(It.IsAny<Candidatedetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            // Assert
            result.Should().NotBeNull();

        }
        [Fact]
        public void DeleteCandidatedetail_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange

            int existingId = 1; // Replace with an existing Id in your test data

            // Mock the behavior of the Get and Remove methods in IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Candidatedetail.Get(It.IsAny<Expression<Func<Candidatedetails, bool>>>()))
                .Returns((Expression<Func<Candidatedetails, bool>> filter) =>
                {
                    // Simulate returning an object when the filter condition matches
                    if (filter.Compile().Invoke(new Candidatedetails { Id = existingId }))
                    {
                        return new Candidatedetails { Id = existingId };
                    }
                    return null;
                });

            // Act
            fixture.Controller.Delete(existingId);


            // Assert
            // Verify that the Remove and Save methods of IUnitOfWork were called as expected
            fixture.MockUnitOfWork.Verify(uow => uow.Candidatedetail.Get(It.IsAny<Expression<Func<Candidatedetails, bool>>>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Candidatedetail.Remove(It.IsAny<Candidatedetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }
        [Fact]
        public void DeleteCandidatedetail_ShouldReturnNotFound_WhenRecordNotFound()
        {

            // Arrange


            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 999; // An ID that is assumed not to exist.
            fixture.MockUnitOfWork.Setup(uow => uow.Candidatedetail.Get(It.IsAny<Expression<Func<Candidatedetails, bool>>>()))
           .Returns((Expression<Func<Candidatedetails, bool>> predicate) => null);

            // Act
            fixture.Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Candidatedetail.Remove(It.IsAny<Candidatedetails>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);

        }
        [Fact]
        public void DeleteCandidatedetail_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange

            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 0;
            fixture.MockUnitOfWork.Setup(uow => uow.Candidatedetail.Get(It.IsAny<Expression<Func<Candidatedetails, bool>>>()))
           .Returns((Expression<Func<Candidatedetails, bool>> predicate) => null);

            // Act
            fixture.Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Candidatedetail.Remove(It.IsAny<Candidatedetails>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact(Skip = "check later")]
        public void UpdateCandidatedetail_ShouldReturnIsActiveTrue_WhenRecordIsUpdated()
        {
            // Arrange

            int entityId = 1; // Example ID for an existing entity

            var requestModel = new CandidatedetailRequestModel
            {
                Id = entityId,
                Name = "KGL",
            };

            // Mock the behavior of IUnitOfWork's Get method to return an existing entity
            var existingEntity = new Candidatedetails
            {
                Id = entityId,
                Name = "ABC",
            };

            fixture.MockUnitOfWork.Setup(uow => uow.Candidatedetail.Get(It.IsAny<Expression<Func<Candidatedetails, bool>>>()))
                .Returns((Expression<Func<Candidatedetails, bool>> predicate) => existingEntity);

            // Act
            var result = fixture.Controller.Put(entityId, requestModel);

            // Assert
            // Verify that the Update and Save methods were called and the response model contains the updated ID.
            fixture.MockUnitOfWork.Verify(uow => uow.Candidatedetail.Update(It.IsAny<Candidatedetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            Assert.True(result.IsActive);
            Assert.Equal(entityId, result.Id);
        }
        
        [Fact(Skip = "check later")]
        public void UpdateCandidatedetail_ShouldReturnIsActiveFalse_WhenInvalidRequest()
        {
            // Arrange
            int entityId = 999; // Id for which the entity is not found

            var requestModel = new CandidatedetailRequestModel
            {
                Id = entityId,
                Name = "KGL",
            };

            fixture.MockUnitOfWork.Setup(uow => uow.Candidatedetail.Get(It.IsAny<Expression<Func<Candidatedetails, bool>>>()))
                .Returns((Expression<Func<Candidatedetails, bool>> predicate) => null);

            // Act
            var result = fixture.Controller.Put(entityId, requestModel);

            // Assert

            Assert.Equal(0, result.Id);
            Assert.False(result.IsActive);

            // Verify that the Update and Save methods were not called since the entity does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Candidatedetail.Update(It.IsAny<Candidatedetails>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }

    }
}
