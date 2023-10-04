using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using MRF.API.Controllers;

namespace MRF.API.Test.Controllers
{
    public class DepartmentControllerTest
    {
        private readonly TestFixture fixture;
        private DepartmentController Controller;

        public DepartmentControllerTest()
        {
            fixture = new TestFixture();
            Controller = new DepartmentController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object);

        }

        [Fact]
        public void DepartmentControllerTest_Constructor_ShouldInitializeDependencies()
        {
            // Assert
            Assert.NotNull(fixture.Controller);

        }

        [Fact]
        public void DepartmentController_ShouldReturnCount_WhenDataFound()
        {


            // Create a list of sample Departmentmaster for testing
            var sampleDepartmentDetails = new List<Departmentmaster>
            {
            new Departmentmaster { Id=1, Name = "John Doe" ,IsActive=true},
            new Departmentmaster { Id=2, Name = "Jane Smith",IsActive=true },
                
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Departmentmaster.GetAll()).Returns(sampleDepartmentDetails);


            // Act
            var result = Controller.Get();

            // Assert
            Assert.Equal(sampleDepartmentDetails.Count, result.Count);
            Assert.NotNull(result);
            result.Should().NotBeNull();
            fixture.MockUnitOfWork.Verify(x => x.Departmentmaster, Times.Once());
        }


        [Fact]
        public void DepartmentController_ShouldReturnNotFound_WhenDataNotFound()
        {

            fixture.MockUnitOfWork.Setup(x => x.Departmentmaster.GetAll()).Returns(new List<Departmentmaster>());


            // Act
            var result = Controller.Get();

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
            fixture.MockUnitOfWork.Verify(x => x.Departmentmaster, Times.Once());
        }


        [Fact]
        public void GetDepartmentDetailslById_ShouldReturnNoResult_WhenInputIsEqualsZero()
        {
            // Arrange

            int id = 0;

            // Create a list of sample Departmentmaster for testing
            var sampleDepartmentDetails = new List<Departmentmaster>
            {
            new Departmentmaster { Name = "John Doe" ,IsActive=true},
            new Departmentmaster { Name = "Jane Smith" ,IsActive=true},
                // Add more sample data as needed
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Departmentmaster.GetAll()).Returns(sampleDepartmentDetails);

            // Act
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));


        }
        [Fact]
        public void GetDepartmentDetailslById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange


            int id = 0;

            // Create a list of sample Departmentmaster for testing
            var sampleDepartmentDetails = new List<Departmentmaster>
            {
            new Departmentmaster { Name = "John Doe" ,IsActive=true},
            new Departmentmaster { Name = "Jane Smith",IsActive=true },
                // Add more sample data as needed
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Departmentmaster.GetAll()).Returns(sampleDepartmentDetails);

            // Act  
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));
        }
        [Fact]
        public void CreateDepartmentDetails_ShouldReturnOkResponse_WhenValidRequest()
        {


            var requestModel = new DepartmentmasterRequestModel
            { 
                Name = "ABc",
                IsActive=true,
                CreatedByEmployeeId = 1,
                CreatedOnUtc = DateTime.Now,
                UpdatedByEmployeeId = 1,
                UpdatedOnUtc = DateTime.Now
            };

            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Departmentmaster.Add(It.IsAny<Departmentmaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new DepartmentmasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(requestModel);

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<DepartmentmasterResponseModel>(result);
            var okResult = (DepartmentmasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Departmentmaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Departmentmaster.Add(It.IsAny<Departmentmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        }
        [Fact]
        public void CreateDepartmentDetailsl_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Departmentmaster.Add(It.IsAny<Departmentmaster>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new DepartmentmasterResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result  =Controller.Post(new DepartmentmasterRequestModel());

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<DepartmentmasterResponseModel>(result);
            var okResult = (DepartmentmasterResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Departmentmaster was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Departmentmaster.Add(It.IsAny<Departmentmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            // Assert
            result.Should().NotBeNull();

        }
        [Fact]
        public void DeleteDepartmentDetails_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange

            int existingId = 1; // Replace with an existing Id in your test data

            // Mock the behavior of the Get and Remove methods in IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Departmentmaster.Get(It.IsAny<Expression<Func<Departmentmaster, bool>>>()))
                .Returns((Expression<Func<Departmentmaster, bool>> filter) =>
                {
                    // Simulate returning an object when the filter condition matches
                    if (filter.Compile().Invoke(new Departmentmaster { Id = existingId }))
                    {
                        return new Departmentmaster { Id = existingId };
                    }
                    return null;
                });

            // Act
            Controller.Delete(existingId);


            // Assert
            // Verify that the Remove and Save methods of IUnitOfWork were called as expected
            fixture.MockUnitOfWork.Verify(uow => uow.Departmentmaster.Get(It.IsAny<Expression<Func<Departmentmaster, bool>>>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Departmentmaster.Remove(It.IsAny<Departmentmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }
        [Fact]
        public void DeleteDepartmentDetails_ShouldReturnNotFound_WhenRecordNotFound()
        {

            // Arrange


            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 999; // An ID that is assumed not to exist.
            fixture.MockUnitOfWork.Setup(uow => uow.Departmentmaster.Get(It.IsAny<Expression<Func<Departmentmaster, bool>>>()))
           .Returns((Expression<Func<Departmentmaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Departmentmaster.Remove(It.IsAny<Departmentmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);

        }
        [Fact]
        public void DeleteDepartmentDetails_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange

            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 0;
            fixture.MockUnitOfWork.Setup(uow => uow.Departmentmaster.Get(It.IsAny<Expression<Func<Departmentmaster, bool>>>()))
           .Returns((Expression<Func<Departmentmaster, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Departmentmaster.Remove(It.IsAny<Departmentmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact]
        public void UpdateDepartmentDetails_ShouldReturnIsActiveTrue_WhenRecordIsUpdated()
        {
            // Arrange

            int entityId = 1; // Example ID for an existing entity

            var requestModel = new DepartmentmasterRequestModel
            {
                 IsActive = true,
            };

            // Mock the behavior of IUnitOfWork's Get method to return an existing entity
            var existingEntity = new Departmentmaster
            {
                Id = entityId,
                IsActive = true,
            };

            fixture.MockUnitOfWork.Setup(uow => uow.Departmentmaster.Get(It.IsAny<Expression<Func<Departmentmaster, bool>>>()))
                .Returns((Expression<Func<Departmentmaster, bool>> predicate) => existingEntity);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert
            // Verify that the Update and Save methods were called and the response model contains the updated ID.
            fixture.MockUnitOfWork.Verify(uow => uow.Departmentmaster.Update(It.IsAny<Departmentmaster>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            Assert.True(result.IsActive);
            Assert.Equal(entityId, result.Id);
        }
        [Fact]
        public void UpdateDepartmentDetails_ShouldReturnIsActiveFalse_WhenInvalidRequest()
        {
            // Arrange
            int entityId = 999; // Id for which the entity is not found

            var requestModel = new DepartmentmasterRequestModel
            {
                
                Name = "KGL",
            };

            fixture.MockUnitOfWork.Setup(uow => uow.Departmentmaster.Get(It.IsAny<Expression<Func<Departmentmaster, bool>>>()))
                .Returns((Expression<Func<Departmentmaster, bool>> predicate) => null);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert

            Assert.Equal(0, result.Id);
            Assert.False(result.IsActive);

            // Verify that the Update and Save methods were not called since the entity does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Departmentmaster.Update(It.IsAny<Departmentmaster>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }

    }
}
