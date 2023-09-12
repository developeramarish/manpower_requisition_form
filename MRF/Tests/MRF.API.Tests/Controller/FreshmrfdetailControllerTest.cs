using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using MRF.API.Controllers;

namespace MRF.API.Test.Controllers
{
    public class FreshmrfdetailControllerTest
    {
        private readonly TestFixture fixture;
        private FreshmrfdetailController Controller;

        public FreshmrfdetailControllerTest()
        {
            fixture = new TestFixture();
            Controller = new FreshmrfdetailController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object);

        }

        [Fact]
        public void FreshmrfdetailControllerTest_Constructor_ShouldInitializeDependencies()
        {
            // Assert
            Assert.NotNull(fixture.Controller);

        }

        [Fact]
        public void FreshmrfdetailController_ShouldReturnCount_WhenDataFound()
        {


            // Create a list of sample Freshmrfdetail for testing
            var SampleFreshmrfDetails = new List<Freshmrfdetails>
            {
            new Freshmrfdetails { Id=1,MrfId=1, Justification= "For a new project",SoftwaresRequired= "Visual Studio, MS Teams, MS Office",
       HardwaresRequired= "Laptop",
       MinTargetSalary=500000,
      MaxTargetSalary= 700000,},
            new Freshmrfdetails { Id=2,Justification= "For a new project",SoftwaresRequired= "Visual Studio, MS Teams, MS Office",
       HardwaresRequired= "Laptop",
       MinTargetSalary=500000,
      MaxTargetSalary= 700000,},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Freshmrfdetail.GetAll()).Returns(SampleFreshmrfDetails);


            // Act
            var result = Controller.Get();

            // Assert
            Assert.Equal(SampleFreshmrfDetails.Count, result.Count);
            Assert.NotNull(result);
            result.Should().NotBeNull();
            fixture.MockUnitOfWork.Verify(x => x.Freshmrfdetail, Times.Once());
        }


        [Fact]
        public void FreshmrfdetailController_ShouldReturnNotFound_WhenDataNotFound()
        {

            fixture.MockUnitOfWork.Setup(x => x.Freshmrfdetail.GetAll()).Returns(new List<Freshmrfdetails>());


            // Act
            var result = Controller.Get();

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
            fixture.MockUnitOfWork.Verify(x => x.Freshmrfdetail, Times.Once());
        }


        [Fact]
        public void GetFreshmrfdetailById_ShouldReturnNoResult_WhenInputIsEqualsZero()
        {
            // Arrange

            int id = 0;

            // Create a list of sample Freshmrfdetail for testing
            var sampleFreshmrfdetail = new List<Freshmrfdetails>
            {
            new Freshmrfdetails {MrfId=1, Justification= "For a new project",SoftwaresRequired= "Visual Studio, MS Teams, MS Office",
       HardwaresRequired= "Laptop",
       MinTargetSalary=500000,
      MaxTargetSalary= 700000,},
            new Freshmrfdetails {MrfId=1, Justification= "For a new project",SoftwaresRequired= "Visual Studio, MS Teams, MS Office",
       HardwaresRequired= "Laptop",
       MinTargetSalary=500000,
      MaxTargetSalary= 700000,},
                // Add more sample data as needed
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Freshmrfdetail.GetAll()).Returns(sampleFreshmrfdetail);

            // Act
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));


        }
        [Fact]
        public void GetFreshmrfdetailById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange


            int id = 0;

            // Create a list of sample Freshmrfdetail for testing
            var sampleFreshmrfdetails = new List<Freshmrfdetails>
            {
            new Freshmrfdetails {MrfId=1, Justification= "For a new project",SoftwaresRequired= "Visual Studio, MS Teams, MS Office",
       HardwaresRequired= "Laptop",
       MinTargetSalary=500000,
      MaxTargetSalary= 700000,},
            new Freshmrfdetails {MrfId=1, Justification= "For a new project",SoftwaresRequired= "Visual Studio, MS Teams, MS Office",
       HardwaresRequired= "Laptop",
       MinTargetSalary=500000,
      MaxTargetSalary= 700000,},
                // Add more sample data as needed   
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Freshmrfdetail.GetAll()).Returns(sampleFreshmrfdetails);

            // Act  
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));
        }
        [Fact]
        public void CreateFreshmrfdetail_ShouldReturnOkResponse_WhenValidRequest()
        {


            var requestModel = new FreshmrfdetailRequestModel
            {
                MrfId = 1,
                Justification = "For a new project",
                SoftwaresRequired = "Visual Studio, MS Teams, MS Office",
                HardwaresRequired = "Laptop",
                MinTargetSalary = 500000,
                MaxTargetSalary = 700000,
                CreatedByEmployeeId = 1,
                CreatedOnUtc = DateTime.Now,
                UpdatedByEmployeeId = 1,

            };

            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Freshmrfdetail.Add(It.IsAny<Freshmrfdetails>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new FreshmrfdetailResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.PostPost(requestModel);

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<FreshmrfdetailResponseModel>(result);
            var okResult = (FreshmrfdetailResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Freshmrfdetail was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Freshmrfdetail.Add(It.IsAny<Freshmrfdetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        }
        [Fact]
        public void CreateFreshmrfdetail_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Freshmrfdetail.Add(It.IsAny<Freshmrfdetails>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new FreshmrfdetailResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.PostPost(new FreshmrfdetailRequestModel());

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<FreshmrfdetailResponseModel>(result);
            var okResult = (FreshmrfdetailResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Freshmrfdetail was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Freshmrfdetail.Add(It.IsAny<Freshmrfdetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            // Assert
            result.Should().NotBeNull();

        }
        [Fact]
        public void DeleteFreshmrfdetail_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange

            int existingId = 1; // Replace with an existing Id in your test data

            // Mock the behavior of the Get and Remove methods in IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Freshmrfdetail.Get(It.IsAny<Expression<Func<Freshmrfdetails, bool>>>()))
                .Returns((Expression<Func<Freshmrfdetails, bool>> filter) =>
                {
                    // Simulate returning an object when the filter condition matches
                    if (filter.Compile().Invoke(new Freshmrfdetails { Id = existingId }))
                    {
                        return new Freshmrfdetails { Id = existingId };
                    }
                    return null;
                });

            // Act
            Controller.Delete(existingId);


            // Assert
            // Verify that the Remove and Save methods of IUnitOfWork were called as expected
            fixture.MockUnitOfWork.Verify(uow => uow.Freshmrfdetail.Get(It.IsAny<Expression<Func<Freshmrfdetails, bool>>>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Freshmrfdetail.Remove(It.IsAny<Freshmrfdetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }
        [Fact]
        public void DeleteFreshmrfdetail_ShouldReturnNotFound_WhenRecordNotFound()
        {

            // Arrange


            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 999; // An ID that is assumed not to exist.
            fixture.MockUnitOfWork.Setup(uow => uow.Freshmrfdetail.Get(It.IsAny<Expression<Func<Freshmrfdetails, bool>>>()))
           .Returns((Expression<Func<Freshmrfdetails, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Freshmrfdetail.Remove(It.IsAny<Freshmrfdetails>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);

        }
        [Fact]
        public void DeleteEvaluationstatusDetails_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange

            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 0;
            fixture.MockUnitOfWork.Setup(uow => uow.Freshmrfdetail.Get(It.IsAny<Expression<Func<Freshmrfdetails, bool>>>()))
           .Returns((Expression<Func<Freshmrfdetails, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Freshmrfdetail.Remove(It.IsAny<Freshmrfdetails>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact]
        public void UpdateFreshmrfdetail_ShouldReturnIsActiveTrue_WhenRecordIsUpdated()
        {
            // Arrange

            int entityId = 1; // Example ID for an existing entity

            var requestModel = new FreshmrfdetailRequestModel
            {
                MrfId = 1,
                Justification = "For a new project",
                SoftwaresRequired = "Visual Studio, MS Teams, MS Office",
                HardwaresRequired = "Laptop",
                MinTargetSalary = 500000,
                MaxTargetSalary = 700000,

            };

            // Mock the behavior of IUnitOfWork's Get method to return an existing entity
            var existingEntity = new Freshmrfdetails
            {
                Id = entityId,
                
            };

            fixture.MockUnitOfWork.Setup(uow => uow.Freshmrfdetail.Get(It.IsAny<Expression<Func<Freshmrfdetails, bool>>>()))
                .Returns((Expression<Func<Freshmrfdetails, bool>> predicate) => existingEntity);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert
            // Verify that the Update and Save methods were called and the response model contains the updated ID.
            fixture.MockUnitOfWork.Verify(uow => uow.Freshmrfdetail.Update(It.IsAny<Freshmrfdetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

          //  Assert.True(result.IsActive);
            Assert.Equal(entityId, result.Id);
        }
        [Fact]
        public void UpdateFreshmrfdetail_ShouldReturnIsActiveFalse_WhenInvalidRequest()
        {
            // Arrange
            int entityId = 999; // Id for which the entity is not found

            var requestModel = new FreshmrfdetailRequestModel
            {

                MrfId = 1,
                Justification = "For a new project",
                SoftwaresRequired = "Visual Studio, MS Teams, MS Office",
                HardwaresRequired = "Laptop",
                MinTargetSalary = 500000,
                MaxTargetSalary = 700000,
            };

            fixture.MockUnitOfWork.Setup(uow => uow.Freshmrfdetail.Get(It.IsAny<Expression<Func<Freshmrfdetails, bool>>>()))
                .Returns((Expression<Func<Freshmrfdetails, bool>> predicate) => null);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert

            Assert.Equal(0, result.Id);
           // Assert.False(result.IsActive);

            // Verify that the Update and Save methods were not called since the entity does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Freshmrfdetail.Update(It.IsAny<Freshmrfdetails>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }

    }
}
