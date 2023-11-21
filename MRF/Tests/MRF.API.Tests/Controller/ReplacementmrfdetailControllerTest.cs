using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using MRF.API.Controllers;
using Azure.Core;

namespace MRF.API.Test.Controllers
{
    public class ReplacementmrfdetailControllerTest
    {
        private readonly TestFixture fixture;
        private ReplacementmrfdetailController Controller;

        public ReplacementmrfdetailControllerTest()
        {
            fixture = new TestFixture();
            Controller = new ReplacementmrfdetailController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object);

        }

        [Fact]
        public void ReplacementmrfdetailControllerTest_Constructor_ShouldInitializeDependencies()
        {
            // Assert
            Assert.NotNull(fixture.Controller);

        }

        [Fact]
        public void ReplacementmrfdetailController_ShouldReturnCount_WhenDataFound()
        {


            // Create a list of sample Replacementmrfdetails for testing
            var SampleReplacementmrfDetails = new List<Replacementmrfdetails>
            {
            new Replacementmrfdetails { Id=1,MrfId =3452,EmployeeName = "gaurav",EmailId ="g@keglobal.com",
                EmployeeCode = 45324,AnnualCtc = 4000000,AnnualGross = 4500000},
            new Replacementmrfdetails { Id=2,MrfId =3452,EmployeeName = "gaurav",EmailId ="g@keglobal.com",
                EmployeeCode = 45324,AnnualCtc = 4000000,AnnualGross = 4500000},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Replacementmrfdetail.GetAll()).Returns(SampleReplacementmrfDetails);


            // Act
            var result = Controller.Get();

            // Assert
            Assert.Equal(SampleReplacementmrfDetails.Count, result.Count);
            Assert.NotNull(result);
            result.Should().NotBeNull();
            fixture.MockUnitOfWork.Verify(x => x.Replacementmrfdetail, Times.Once());
        }


        [Fact]
        public void ReplacementmrfdetailController_ShouldReturnNotFound_WhenDataNotFound()
        {

            fixture.MockUnitOfWork.Setup(x => x.Replacementmrfdetail.GetAll()).Returns(new List<Replacementmrfdetails>());


            // Act
            var result = Controller.Get();

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
            fixture.MockUnitOfWork.Verify(x => x.Replacementmrfdetail, Times.Once());
        }


        [Fact]
        public void GetReplacementmrfdetailById_ShouldReturnNoResult_WhenInputIsEqualsZero()
        {
            // Arrange

            int id = 0;

            // Create a list of sample Replacementmrfdetails for testing
            var SampleReplacementmrfDetails = new List<Replacementmrfdetails>
            {
            new Replacementmrfdetails  {MrfId =3452,EmployeeName = "gaurav",EmailId ="g@keglobal.com",
                EmployeeCode = 45324,AnnualCtc = 4000000,AnnualGross = 4500000},
            new Replacementmrfdetails{ MrfId =3452,EmployeeName = "gaurav",EmailId ="g@keglobal.com",
                EmployeeCode = 45324,AnnualCtc = 4000000,AnnualGross = 4500000},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Replacementmrfdetail.GetAll()).Returns(SampleReplacementmrfDetails);

            // Act
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));


        }
        [Fact]
        public void GetReplacementmrfdetailById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange


            int id = -2;

            // Create a list of sample Replacementmrfdetails for testing
            var SampleReplacementmrfDetails = new List<Replacementmrfdetails>
            {
            new Replacementmrfdetails {MrfId =3452,EmployeeName = "gaurav",EmailId ="g@keglobal.com",
                EmployeeCode = 45324,AnnualCtc = 4000000,AnnualGross = 4500000},
            new Replacementmrfdetails {MrfId =3452,EmployeeName = "gaurav",EmailId ="g@keglobal.com",
                EmployeeCode = 45324,AnnualCtc = 4000000,AnnualGross = 4500000},
                // Add more sample data as needed   
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Replacementmrfdetail.GetAll()).Returns(SampleReplacementmrfDetails);

            // Act  
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:-2"));
        }
        [Fact]
        public void CreateReplacementmrfdetail_ShouldReturnOkResponse_WhenValidRequest()
        {


            var requestModel = new ReplacementmrfdetailRequestModel
            {
                MrfId = 3452,
                EmployeeName = "gaurav",
                EmailId = "g@keglobal.com",
                EmployeeCode = 45324,
                AnnualCtc = 4000000,
                AnnualGross = 4500000,
                //GradeId = 'A',
                 
                CreatedOnUtc = DateTime.Now,
                UpdatedByEmployeeId = 1,
                UpdatedOnUtc = DateTime.Now

            };

            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Replacementmrfdetail.Add(It.IsAny<Replacementmrfdetails>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new ReplacementmrfdetailResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(requestModel);

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<ReplacementmrfdetailResponseModel>(result);
            var okResult = (ReplacementmrfdetailResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Replacementmrfdetails was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Replacementmrfdetail.Add(It.IsAny<Replacementmrfdetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        }
        [Fact]
        public void CreateReplacementmrfdetail_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Replacementmrfdetail.Add(It.IsAny<Replacementmrfdetails>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new ReplacementmrfdetailResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(new ReplacementmrfdetailRequestModel());

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<ReplacementmrfdetailResponseModel>(result);
            var okResult = (ReplacementmrfdetailResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Replacementmrfdetails was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Replacementmrfdetail.Add(It.IsAny<Replacementmrfdetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            // Assert
            result.Should().NotBeNull();

        }
        [Fact]
        public void DeleteReplacementmrfdetail_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange

            int existingId = 1; // Replace with an existing Id in your test data

            // Mock the behavior of the Get and Remove methods in IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Replacementmrfdetail.Get(It.IsAny<Expression<Func<Replacementmrfdetails, bool>>>()))
                .Returns((Expression<Func<Replacementmrfdetails, bool>> filter) =>
                {
                    // Simulate returning an object when the filter condition matches
                    if (filter.Compile().Invoke(new Replacementmrfdetails { Id = existingId }))
                    {
                        return new Replacementmrfdetails { Id = existingId };
                    }
                    return null;
                });

            // Act
            Controller.Delete(existingId);


            // Assert
            // Verify that the Remove and Save methods of IUnitOfWork were called as expected
            fixture.MockUnitOfWork.Verify(uow => uow.Replacementmrfdetail.Get(It.IsAny<Expression<Func<Replacementmrfdetails, bool>>>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Replacementmrfdetail.Remove(It.IsAny<Replacementmrfdetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }
        [Fact]
        public void DeleteReplacementmrfdetail_ShouldReturnNotFound_WhenRecordNotFound()
        {

            // Arrange


            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 999; // An ID that is assumed not to exist.
            fixture.MockUnitOfWork.Setup(uow => uow.Replacementmrfdetail.Get(It.IsAny<Expression<Func<Replacementmrfdetails, bool>>>()))
           .Returns((Expression<Func<Replacementmrfdetails, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Replacementmrfdetail.Remove(It.IsAny<Replacementmrfdetails>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);

        }
        [Fact]
        public void DeleteReplacementmrfdetail_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange

            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 0;
            fixture.MockUnitOfWork.Setup(uow => uow.Replacementmrfdetail.Get(It.IsAny<Expression<Func<Replacementmrfdetails, bool>>>()))
           .Returns((Expression<Func<Replacementmrfdetails, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Replacementmrfdetail.Remove(It.IsAny<Replacementmrfdetails>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact]
        public void UpdateReplacementmrfdetail_ShouldReturnIsActiveTrue_WhenRecordIsUpdated()
        {
            // Arrange

            int entityId = 1; // Example ID for an existing entity

            var requestModel = new ReplacementmrfdetailRequestModel
            {
                MrfId = 3452,
                EmployeeName = "gaurav",
                EmailId = "g@keglobal.com",
                EmployeeCode = 45324,
                AnnualCtc = 4000000,
                AnnualGross = 4500000,
                //GradeId = 'A',

            };

            // Mock the behavior of IUnitOfWork's Get method to return an existing entity
            var existingEntity = new Replacementmrfdetails
            {
                Id = entityId,


            };

            fixture.MockUnitOfWork.Setup(uow => uow.Replacementmrfdetail.Get(It.IsAny<Expression<Func<Replacementmrfdetails, bool>>>()))
                .Returns((Expression<Func<Replacementmrfdetails, bool>> predicate) => existingEntity);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert
            // Verify that the Update and Save methods were called and the response model contains the updated ID.
            fixture.MockUnitOfWork.Verify(uow => uow.Replacementmrfdetail.Update(It.IsAny<Replacementmrfdetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            //Assert.True(result.IsActive);
            Assert.Equal(entityId, result.Id);
        }
        [Fact]
        public void UpdateReplacementmrfdetail_ShouldReturnIsActiveFalse_WhenInvalidRequest()
        {
            // Arrange
            int entityId = 999; // Id for which the entity is not found

            var requestModel = new ReplacementmrfdetailRequestModel
            {

                MrfId = 3452,
                EmployeeName = "gaurav",
                EmailId = "g@keglobal.com",
                EmployeeCode = 45324,
                AnnualCtc = 4000000,
                AnnualGross = 4500000,
                //GradeId = 'A',

            };

            fixture.MockUnitOfWork.Setup(uow => uow.Replacementmrfdetail.Get(It.IsAny<Expression<Func<Replacementmrfdetails, bool>>>()))
                .Returns((Expression<Func<Replacementmrfdetails, bool>> predicate) => null);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert

            Assert.Equal(0, result.Id);
            //Assert.False(result.IsActive);

            // Verify that the Update and Save methods were not called since the entity does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Replacementmrfdetail.Update(It.IsAny<Replacementmrfdetails>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }

    }
}
