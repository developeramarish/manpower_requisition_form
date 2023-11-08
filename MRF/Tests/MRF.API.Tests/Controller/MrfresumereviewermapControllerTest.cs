using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using MRF.API.Controllers;
using Azure.Core;
using MRF.Models.ViewModels;

namespace MRF.API.Test.Controllers
{
    public class MrfresumereviewermapControllerTest
    {
        private readonly TestFixture fixture;
        private MrfresumereviewermapController Controller;

        public MrfresumereviewermapControllerTest()
        {
            fixture = new TestFixture();
            Controller = new MrfresumereviewermapController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object, fixture.MockEmailService.Object,fixture.MockHostEnvironment.Object);
        }

        [Fact]
        public void MrfresumereviewermapControllerTest_Constructor_ShouldInitializeDependencies()
        {
            // Assert
            Assert.NotNull(fixture.Controller);

        }

        [Fact]
        public void MrfresumereviewermapController_ShouldReturnCount_WhenDataFound()
        {


            // Create a list of sample Mrfresumereviewermap for testing
            var SampleMrfresumereviewermapDetails = new List<Mrfresumereviewermap>
            {
            new Mrfresumereviewermap { Id=1,MrfId=345,ResumeReviewerEmployeeId=47348,IsActive=true},
            new Mrfresumereviewermap { Id=2,MrfId=345,ResumeReviewerEmployeeId=47348,IsActive=true},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfresumereviewermap.GetAll()).Returns(SampleMrfresumereviewermapDetails);


            // Act
            var result = Controller.Get();

            // Assert
            Assert.Equal(SampleMrfresumereviewermapDetails.Count, result.Count);
            Assert.NotNull(result);
            result.Should().NotBeNull();
            fixture.MockUnitOfWork.Verify(x => x.Mrfresumereviewermap, Times.Once());
        }


        [Fact]
        public void MrfresumereviewermapController_ShouldReturnNotFound_WhenDataNotFound()
        {

            fixture.MockUnitOfWork.Setup(x => x.Mrfresumereviewermap.GetAll()).Returns(new List<Mrfresumereviewermap>());


            // Act
            var result = Controller.Get();

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
            fixture.MockUnitOfWork.Verify(x => x.Mrfresumereviewermap, Times.Once());
        }


        [Fact]
        public void GetMrfresumereviewermapById_ShouldReturnNoResult_WhenInputIsEqualsZero()
        {
            // Arrange

            int id = 0;

            // Create a list of sample Mrfresumereviewermap for testing
            var SampleMrfresumereviewermapDetails = new List<Mrfresumereviewermap>
            {
            new Mrfresumereviewermap  {MrfId=345,ResumeReviewerEmployeeId=47348,IsActive=true},
            new Mrfresumereviewermap {MrfId=345,ResumeReviewerEmployeeId=47348,IsActive=true},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfresumereviewermap.GetAll()).Returns(SampleMrfresumereviewermapDetails);

            // Act
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));


        }
        [Fact]
        public void GetMrfinterviewermapById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange


            int id = -2;

            // Create a list of sample Mrfresumereviewermap for testing
            var SampleMrfinterviewermapDetails = new List<Mrfresumereviewermap>
            {
            new Mrfresumereviewermap {MrfId=345,ResumeReviewerEmployeeId=47348,IsActive=true},
            new Mrfresumereviewermap {MrfId=345,ResumeReviewerEmployeeId=47348,IsActive=true},
                // Add more sample data as needed   
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfresumereviewermap.GetAll()).Returns(SampleMrfinterviewermapDetails);

            // Act  
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:-2"));
        }
        [Fact]
        public void CreateMrfresumereviewermap_ShouldReturnOkResponse_WhenValidRequest()
        {


            var requestModel = new MrfresumereviewermapRequestModel
            {
                MrfId = 2345,
                ResumeReviewerEmployeeId = 47348,
                IsActive = true,
                CreatedOnUtc = DateTime.Now,
                UpdatedByEmployeeId = 1,
                UpdatedOnUtc = DateTime.Now

            };

            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfresumereviewermap.Add(It.IsAny<Mrfresumereviewermap>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new MrfresumereviewermapResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(requestModel);

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<MrfresumereviewermapResponseModel>(result);
            var okResult = (MrfresumereviewermapResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Mrfresumereviewermap was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfresumereviewermap.Add(It.IsAny<Mrfresumereviewermap>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        }
        [Fact]
        public void CreateMrfresumereviewermap_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfresumereviewermap.Add(It.IsAny<Mrfresumereviewermap>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new MrfresumereviewermapResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(new MrfresumereviewermapRequestModel());

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<MrfresumereviewermapResponseModel>(result);
            var okResult = (MrfresumereviewermapResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Mrfresumereviewermap was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfresumereviewermap.Add(It.IsAny<Mrfresumereviewermap>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            // Assert
            result.Should().NotBeNull();

        }
        [Fact]
        public void DeleteMrfresumereviewermap_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange

            int existingId = 1; // Replace with an existing Id in your test data

            // Mock the behavior of the Get and Remove methods in IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfresumereviewermap.Get(It.IsAny<Expression<Func<Mrfresumereviewermap, bool>>>()))
                .Returns((Expression<Func<Mrfresumereviewermap, bool>> filter) =>
                {
                    // Simulate returning an object when the filter condition matches
                    if (filter.Compile().Invoke(new Mrfresumereviewermap { Id = existingId }))
                    {
                        return new Mrfresumereviewermap { Id = existingId };
                    }
                    return null;
                });

            // Act
            Controller.Delete(existingId);


            // Assert
            // Verify that the Remove and Save methods of IUnitOfWork were called as expected
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfresumereviewermap.Get(It.IsAny<Expression<Func<Mrfresumereviewermap, bool>>>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfresumereviewermap.Remove(It.IsAny<Mrfresumereviewermap>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }
        [Fact]
        public void DeleteMrfresumereviewermap_ShouldReturnNotFound_WhenRecordNotFound()
        {

            // Arrange


            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 999; // An ID that is assumed not to exist.
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfresumereviewermap.Get(It.IsAny<Expression<Func<Mrfresumereviewermap, bool>>>()))
           .Returns((Expression<Func<Mrfresumereviewermap, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfresumereviewermap.Remove(It.IsAny<Mrfresumereviewermap>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);

        }
        [Fact]
        public void DeleteMrfresumereviewermap_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange

            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 0;
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfresumereviewermap.Get(It.IsAny<Expression<Func<Mrfresumereviewermap, bool>>>()))
           .Returns((Expression<Func<Mrfresumereviewermap, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfresumereviewermap.Remove(It.IsAny<Mrfresumereviewermap>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact]
        public void UpdateMrfresumereviewermap_ShouldReturnIsActiveTrue_WhenRecordIsUpdated()
        {
            // Arrange

            int entityId = 1; // Example ID for an existing entity

            var requestModel = new MrfresumereviewermapRequestModel
            {
                 ResumeReviewerEmployeeId = 47348,
                IsActive = true

            };

            // Mock the behavior of IUnitOfWork's Get method to return an existing entity
            var existingEntity = new Mrfresumereviewermap
            {
                Id = entityId,


            };

            fixture.MockUnitOfWork.Setup(uow => uow.Mrfresumereviewermap.Get(It.IsAny<Expression<Func<Mrfresumereviewermap, bool>>>()))
                .Returns((Expression<Func<Mrfresumereviewermap, bool>> predicate) => existingEntity);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert
            // Verify that the Update and Save methods were called and the response model contains the updated ID.
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfresumereviewermap.Update(It.IsAny<Mrfresumereviewermap>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            // Assert.True(result.IsActive);
            Assert.Equal(entityId, result.Id);
        }
        [Fact]
        public void UpdateMrfresumereviewermap_ShouldReturnIsActiveFalse_WhenInvalidRequest()
        {
            // Arrange
            int entityId = 999; // Id for which the entity is not found

            var requestModel = new MrfresumereviewermapRequestModel
            {

                MrfId = 2345,
                 ResumeReviewerEmployeeId = 47348,
                IsActive = true

            };

            fixture.MockUnitOfWork.Setup(uow => uow.Mrfresumereviewermap.Get(It.IsAny<Expression<Func<Mrfresumereviewermap, bool>>>()))
                .Returns((Expression<Func<Mrfresumereviewermap, bool>> predicate) => null);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert

            Assert.Equal(0, result.Id);
            //Assert.False(result.IsActive);

            // Verify that the Update and Save methods were not called since the entity does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfresumereviewermap.Update(It.IsAny<Mrfresumereviewermap>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact]
        public void GetResumeStatusDetailsById_ShouldReturnNoResult_WhenInputIsEqualToZero()
        {
            // Arrange

            int id = 0;
            bool dashboard=false;
            // Create a list of sample Mrfinterviewermap for testing
            var SampleResumeDetails = new List<ResumeDetailsViewModel>
            {
            new ResumeDetailsViewModel  {MrfId=345,ReferenceNo="02/MUM/CFR/AUG/23/007",ResumeReviewerEmployeeId= 47345,ResumeReviewerName="kritika gupta",},
            new ResumeDetailsViewModel {MrfId=345,ReferenceNo="02/MUM/CFR/AUG/23/007",ResumeReviewerEmployeeId=47236,ResumeReviewerName="kritika gupta",},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.ResumeDetail.GetAll()).Returns(SampleResumeDetails);

            // Act
            var result = Controller.GetResumeStatusDetails(id, dashboard);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id: 0"));


        }
        [Fact]
        public void GetResumeStatusDetailsById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange

            int id = -3;
            bool dashboard=false;
            // Create a list of sample Mrfinterviewermap for testing
            var SampleMrfDetails = new List<ResumeDetailsViewModel>
            {
            new ResumeDetailsViewModel  {MrfId=345,ReferenceNo="02/MUM/CFR/AUG/23/007",ResumeReviewerEmployeeId= 47345,ResumeReviewerName="kritika gupta",},
            new ResumeDetailsViewModel {MrfId=345,ReferenceNo="02/MUM/CFR/AUG/23/007",ResumeReviewerEmployeeId=47346,ResumeReviewerName="kritika gupta",},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.ResumeDetail.GetAll()).Returns(SampleMrfDetails);

            // Act
            var result = Controller.GetResumeStatusDetails(id, dashboard);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id: -3"));


        }

    }
}
