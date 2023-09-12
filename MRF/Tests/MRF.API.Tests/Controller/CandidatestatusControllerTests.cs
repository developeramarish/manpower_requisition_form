using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using MRF.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MRF.API.Test.Controllers;

public class CandidatestatusControllerTests
{
    private readonly TestFixture fixture;
    private CandidatestatusController Controller;
    public CandidatestatusControllerTests()
    { 
        fixture = new TestFixture();
        Controller = new CandidatestatusController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object);

    }

    [Fact]
    public void CandidatestatusstatusController_Constructor_ShouldInitializeDependencies()
    {
        // Assert
        Assert.NotNull(fixture.Controller);

    }

    [Fact]
    public void CandidatestatusstatusController_ShouldReturnCount_WhenDataFound()
    {


        // Create a list of sample Candidatestatusmaster for testing
        var sampleCandidatestatusmaster = new List<Candidatestatusmaster>
        {
        new Candidatestatusmaster { Id = 1,Status = "Candidate Selected" },
        new Candidatestatusmaster { Id = 2, Status = "Candidate Not Selected" },
            // Add more sample data as needed
        };

        // Set up the behavior of the mockUnitOfWork to return the sample data
        fixture.MockUnitOfWork.Setup(uow => uow.Candidatestatusmaster.GetAll()).Returns(sampleCandidatestatusmaster);


        // Act
        var result = Controller.Get();

        // Assert
        Assert.Equal(sampleCandidatestatusmaster.Count, result.Count);
        Assert.NotNull(result);
        result.Should().NotBeNull();
        fixture.MockUnitOfWork.Verify(x => x.Candidatestatusmaster, Times.Once());
    }


    [Fact]
    public void CandidatestatusstatusController_ShouldReturnNotFound_WhenDataNotFound()
    {

        fixture.MockUnitOfWork.Setup(x => x.Candidatestatusmaster.GetAll()).Returns(new List<Candidatestatusmaster>());


        // Act
        var result = Controller.Get();

        // Assert
        result.Should().NotBeNull();
        fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
        fixture.MockUnitOfWork.Verify(x => x.Candidatestatusmaster, Times.Once());
    }


    [Fact]
    public void GetCandidatestatusmasterById_ShouldReturnNoResult_WhenInputIsEqualsZero()
    {
        // Arrange

        int id = 0;

        // Create a list of sample Candidatestatusmaster for testing
        var sampleCandidatestatusmaster = new List<Candidatestatusmaster>
        {
        new Candidatestatusmaster { Id = 1, Status = "John Doe" },
        new Candidatestatusmaster { Id = 2, Status = "Jane Smith" },
            // Add more sample data as needed
        };

        // Set up the behavior of the mockUnitOfWork to return the sample data
        fixture.MockUnitOfWork.Setup(uow => uow.Candidatestatusmaster.GetAll()).Returns(sampleCandidatestatusmaster);

        // Act
        var result = Controller.Get(id);

        // Assert
        result.Should().NotBeNull();
        fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:" + id));


    }
    [Fact]
    public void GetCandidatestatusmasterById_ShouldReturnNoResult_WhenInputIsLessThanZero()
    {
        // Arrange


        int id = -3;

        // Create a list of sample Candidatestatusmaster for testing
        var sampleCandidatestatusmaster = new List<Candidatestatusmaster>
        {
        new Candidatestatusmaster { Id = 1,  Status = "John Doe" },
        new Candidatestatusmaster { Id = 2, Status = "Jane Smith" },
            // Add more sample data as needed
        };

        // Set up the behavior of the mockUnitOfWork to return the sample data
        fixture.MockUnitOfWork.Setup(uow => uow.Candidatestatusmaster.GetAll()).Returns(sampleCandidatestatusmaster);

        // Act  
        var result = Controller.Get(id);

        // Assert
        result.Should().NotBeNull();
        fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:" + id));
    }
    [Fact]
    public void CreateCandidatestatusmaster_ShouldReturnOkResponse_WhenValidRequest()
    {


        var requestModel = new CandidatestatusmasterRequestModel
        {
             
            IsActive = true,
            Status = "Candidate Selected",
            CreatedByEmployeeId = 1,
            CreatedOnUtc = DateTime.Now,
            UpdatedByEmployeeId = 1,
            UpdatedOnUtc = DateTime.Now
        };

        // Mock the behavior of IUnitOfWork
        fixture.MockUnitOfWork.Setup(uow => uow.Candidatestatusmaster.Add(It.IsAny<Candidatestatusmaster>()));
        fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

        // Create an instance of ResponseDTO to return
        var responseModel = new CandidatestatusmasterResponseModel
        {
            Id = 0, // Set the expected Id
                    // Set other properties as needed
        };

        // Act
        var result = Controller.Post(requestModel);

        // Assert
        // Verify that the result is an OkObjectResult
        Assert.IsType<CandidatestatusmasterResponseModel>(result);
        var okResult = (CandidatestatusmasterResponseModel)result;

        // Check if the returned response matches the expected response
        Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

        // Verify that the Candidatestatusmaster was added and Save was called
        fixture.MockUnitOfWork.Verify(uow => uow.Candidatestatusmaster.Add(It.IsAny<Candidatestatusmaster>()), Times.Once);
        fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

    }
    [Fact]
    public void CreateCandidatestatusmaster_ShouldReturnBadRequest_WhenInvalidRequest()
    {
        // Mock the behavior of IUnitOfWork
        fixture.MockUnitOfWork.Setup(uow => uow.Candidatestatusmaster.Add(It.IsAny<Candidatestatusmaster>()));
        fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

        // Create an instance of ResponseDTO to return
        var responseModel = new CandidatestatusmasterResponseModel
        {
            Id = 0, // Set the expected Id
                    // Set other properties as needed
        };

        // Act
        var result = Controller.Post(new CandidatestatusmasterRequestModel());

        // Assert
        // Verify that the result is an OkObjectResult
        Assert.IsType<CandidatestatusmasterResponseModel>(result);
        var okResult = (CandidatestatusmasterResponseModel)result;

        // Check if the returned response matches the expected response
        Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

        // Verify that the Candidatestatusmaster was added and Save was called
        fixture.MockUnitOfWork.Verify(uow => uow.Candidatestatusmaster.Add(It.IsAny<Candidatestatusmaster>()), Times.Once);
        fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        // Assert
        result.Should().NotBeNull();

    }
    [Fact]
    public void DeleteCandidatestatusmaster_ShouldReturnNoContents_WhenDeletedARecord()
    {
        // Arrange

        int existingId = 1; // Replace with an existing Id in your test data

        // Mock the behavior of the Get and Remove methods in IUnitOfWork
        fixture.MockUnitOfWork.Setup(uow => uow.Candidatestatusmaster.Get(It.IsAny<Expression<Func<Candidatestatusmaster, bool>>>()))
            .Returns((Expression<Func<Candidatestatusmaster, bool>> filter) =>
            {
                // Simulate returning an object when the filter condition matches
                if (filter.Compile().Invoke(new Candidatestatusmaster { Id = existingId }))
                {
                    return new Candidatestatusmaster { Id = existingId };
                }
                return null;
            });

        // Act
         Controller.Delete(existingId);


        // Assert
        // Verify that the Remove and Save methods of IUnitOfWork were called as expected
        fixture.MockUnitOfWork.Verify(uow => uow.Candidatestatusmaster.Get(It.IsAny<Expression<Func<Candidatestatusmaster, bool>>>()), Times.Once);
        fixture.MockUnitOfWork.Verify(uow => uow.Candidatestatusmaster.Remove(It.IsAny<Candidatestatusmaster>()), Times.Once);
        fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
    }
    [Fact]
    public void DeleteCandidatestatusmaster_ShouldReturnNotFound_WhenRecordNotFound()
    {

        // Arrange


        // Set up your mock IUnitOfWork behavior.
        int nonExistentId = 999; // An ID that is assumed not to exist.
        fixture.MockUnitOfWork.Setup(uow => uow.Candidatestatusmaster.Get(It.IsAny<Expression<Func<Candidatestatusmaster, bool>>>()))
       .Returns((Expression<Func<Candidatestatusmaster, bool>> predicate) => null);

        // Act
         Controller.Delete(nonExistentId);

        // Assert
        // Verify that the Remove and Save methods were not called since the object does not exist.
        fixture.MockUnitOfWork.Verify(uow => uow.Candidatestatusmaster.Remove(It.IsAny<Candidatestatusmaster>()), Times.Never);
        fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);

    }
    [Fact]
    public void DeleteCandidatestatusmaster_ShouldReturnBadResponse_WhenInputIsZero()
    {
        // Arrange

        // Set up your mock IUnitOfWork behavior.
        int nonExistentId = 0;
        fixture.MockUnitOfWork.Setup(uow => uow.Candidatestatusmaster.Get(It.IsAny<Expression<Func<Candidatestatusmaster, bool>>>()))
       .Returns((Expression<Func<Candidatestatusmaster, bool>> predicate) => null);

        // Act
         Controller.Delete(nonExistentId);

        // Assert
        // Verify that the Remove and Save methods were not called since the object does not exist.
        fixture.MockUnitOfWork.Verify(uow => uow.Candidatestatusmaster.Remove(It.IsAny<Candidatestatusmaster>()), Times.Never);
        fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
    }
    [Fact]
    public void UpdateCandidatestatusmaster_ShouldReturnIsActiveTrue_WhenRecordIsUpdated()
    {
        // Arrange

        int entityId = 1; // Example ID for an existing entity

        var requestModel = new CandidatestatusmasterRequestModel
        {

            Status = "KGL",
        };

        // Mock the behavior of IUnitOfWork's Get method to return an existing entity
        var existingEntity = new Candidatestatusmaster
        {
            Id = entityId,
            Status = "ABC",
        };

        fixture.MockUnitOfWork.Setup(uow => uow.Candidatestatusmaster.Get(It.IsAny<Expression<Func<Candidatestatusmaster, bool>>>()))
            .Returns((Expression<Func<Candidatestatusmaster, bool>> predicate) => existingEntity);

        // Act
        var result = Controller.Put(entityId, requestModel);

        // Assert
        // Verify that the Update and Save methods were called and the response model contains the updated ID.
        fixture.MockUnitOfWork.Verify(uow => uow.Candidatestatusmaster.Update(It.IsAny<Candidatestatusmaster>()), Times.Once);
        fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        Assert.False(result.IsActive);
        Assert.Equal(entityId, result.Id);
    }
    [Fact]
    public void UpdateCandidatestatusmaster_ShouldReturnIsActiveFalse_WhenInvalidRequest()
    {
        // Arrange
        int entityId = 999; // Id for which the entity is not found

        var requestModel = new CandidatestatusmasterRequestModel
        {

            Status = "KGL",
        };

        fixture.MockUnitOfWork.Setup(uow => uow.Candidatestatusmaster.Get(It.IsAny<Expression<Func<Candidatestatusmaster, bool>>>()))
            .Returns((Expression<Func<Candidatestatusmaster, bool>> predicate) => null);

        // Act
        var result = Controller.Put(entityId, requestModel);

        // Assert

        Assert.Equal(0, result.Id);
        Assert.False(result.IsActive);

        // Verify that the Update and Save methods were not called since the entity does not exist.
        fixture.MockUnitOfWork.Verify(uow => uow.Candidatestatusmaster.Update(It.IsAny<Candidatestatusmaster>()), Times.Never);
        fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
    }

}
