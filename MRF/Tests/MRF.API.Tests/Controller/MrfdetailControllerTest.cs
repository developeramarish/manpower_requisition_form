using MRF.Models.DTO;
using MRF.Models.Models;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using MRF.API.Controllers;
using Azure.Core;
using MRF.Models.ViewModels;
using MRF.Utility;


namespace MRF.API.Test.Controllers
{
    public class MrfdetailControllerTest
    {
        private readonly TestFixture fixture;
        private MrfdetailController Controller;
       
        public MrfdetailControllerTest()
        {
            fixture = new TestFixture();
            Controller = new MrfdetailController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object, fixture.MockEmailService.Object,fixture.MockHostEnvironment.Object);

        }

        [Fact]
        public void MrfdetailControllerTest_Constructor_ShouldInitializeDependencies()
        {
            // Assert
            Assert.NotNull(fixture.Controller);

        }

        [Fact]
        public void MrfdetailController_ShouldReturnCount_WhenDataFound()
        {


            // Create a list of sample Mrfdetails for testing
            var SampleMrfDetails = new List<Mrfdetails>
            {
            new Mrfdetails { Id=1, ReferenceNo ="02/MUM/CFR/AUG/23/007",PositionTitle= "software engineer",
             DepartmentId= 2,SubDepartmentId= 1,ProjectId= 1,VacancyNo= 2,ReportsToEmployeeId=5,MinGradeId=5,MaxGradeId=6,
             EmploymentTypeId=1,MinExperience=0,MaxExperience=4,VacancyTypeId=3,IsReplacement=false,MrfStatusId=2,},
            new Mrfdetails { Id=2, ReferenceNo ="02/MUM/CFR/AUG/23/007",PositionTitle= "software engineer",
             DepartmentId= 2,SubDepartmentId= 1,ProjectId= 1,VacancyNo= 2,ReportsToEmployeeId=5,MinGradeId=5,MaxGradeId=6,
             EmploymentTypeId=1,MinExperience=0,MaxExperience=4,VacancyTypeId=3,IsReplacement=false,MrfStatusId=2,},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfdetail.GetAll()).Returns(SampleMrfDetails);


            // Act
            var result = Controller.Get();

            // Assert
            Assert.Equal(SampleMrfDetails.Count, result.Count);
            Assert.NotNull(result);
            result.Should().NotBeNull();
            fixture.MockUnitOfWork.Verify(x => x.Mrfdetail, Times.Once());
        }


        [Fact]
        public void MrfdetailController_ShouldReturnNotFound_WhenDataNotFound()
        {

            fixture.MockUnitOfWork.Setup(x => x.Mrfdetail.GetAll()).Returns(new List<Mrfdetails>());


            // Act
            var result = Controller.Get();

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
            fixture.MockUnitOfWork.Verify(x => x.Mrfdetail, Times.Once());
        }


        [Fact]
        public void GetMrfdetailById_ShouldReturnNoResult_WhenInputIsEqualsZero()
        {
            // Arrange

            int id = 0;

            // Create a list of sample Mrfdetails for testing
            var sampleMrfdetails = new List<Mrfdetails>
            {
            new Mrfdetails  {ReferenceNo ="02/MUM/CFR/AUG/23/007",PositionTitle= "software engineer",
             DepartmentId= 2,SubDepartmentId= 1,ProjectId= 1,VacancyNo= 2,ReportsToEmployeeId=5,MinGradeId=5,MaxGradeId=6,
             EmploymentTypeId=1,MinExperience=0,MaxExperience=4,VacancyTypeId=3,IsReplacement=false,MrfStatusId=2,},
            new Mrfdetails {ReferenceNo ="02/MUM/CFR/AUG/23/007",PositionTitle= "software engineer",
             DepartmentId= 2,SubDepartmentId= 1,ProjectId= 1,VacancyNo= 2,ReportsToEmployeeId=5,MinGradeId=5,MaxGradeId=6,
             EmploymentTypeId=1,MinExperience=0,MaxExperience=4,VacancyTypeId=3,IsReplacement=false,MrfStatusId=2,},
                
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfdetail.GetAll()).Returns(sampleMrfdetails);

            // Act
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));


        }
        [Fact]
        public void GetMrfdetailsById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange


            int id = -2;

            // Create a list of sample Mrfdetails for testing
            var sampleMrfdetails = new List<Mrfdetails>
            {
            new Mrfdetails {ReferenceNo ="02/MUM/CFR/AUG/23/007",PositionTitle= "software engineer",
             DepartmentId= 2,SubDepartmentId= 1,ProjectId= 1,VacancyNo= 2,ReportsToEmployeeId=5,MinGradeId=5,MaxGradeId=6,
             EmploymentTypeId=1,MinExperience=0,MaxExperience=4,VacancyTypeId=3,IsReplacement=false,MrfStatusId=2,},
            new Mrfdetails {ReferenceNo ="02/MUM/CFR/AUG/23/007",PositionTitle= "software engineer",
             DepartmentId= 2,SubDepartmentId= 1,ProjectId= 1,VacancyNo= 2,ReportsToEmployeeId=5,MinGradeId=5,MaxGradeId=6,
             EmploymentTypeId=1,MinExperience=0,MaxExperience=4,VacancyTypeId=3,IsReplacement=false,MrfStatusId=2,},
                // Add more sample data as needed   
            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfdetail.GetAll()).Returns(sampleMrfdetails);

            // Act  
            var result = Controller.Get(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:-2"));
        }

        [Fact(Skip = "check later")]
        public void CreateMrfdetail_ShouldReturnOkResponse_WhenValidRequest()
        {


            var requestModel = new MrfdetailRequestModel
            {
                ReferenceNo = "02/MUM/CFR/AUG/23/007",
                PositionTitle = "software engineer",
                DepartmentId = 2,
                SubDepartmentId = 1,
                ProjectId = 1,
                VacancyNo = 2,
                ReportsToEmployeeId = 5,
                MinGradeId = 1,
                MaxGradeId = 5,
                EmploymentTypeId = 1,
                MinExperience = 0,
                MaxExperience = 4,
                VacancyTypeId = 3,
                IsReplacement = false,
                MrfStatusId = 2,

            };

            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfdetail.Add(It.IsAny<Mrfdetails>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new MrfdetaiResponseModel
            {
                Id = 0, // Set the expected Id
                        // Set other properties as needed
            };

            // Act
            var result = Controller.Post(requestModel);

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<MrfdetaiResponseModel>(result);
            var okResult = (MrfdetaiResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Mrfdetails was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfdetail.Add(It.IsAny<Mrfdetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

        }
        [Fact(Skip = "check later")]
        public void CreateMrfdetails_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Mock the behavior of IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfdetail.Add(It.IsAny<Mrfdetails>()));
            fixture.MockUnitOfWork.Setup(uow => uow.Save()).Verifiable();

            // Create an instance of ResponseDTO to return
            var responseModel = new MrfdetaiResponseModel
            {
                Id = 0, // Set the expected Id
                StatusCode = 0,
                message = null  ,     // Set other properties as needed
            };

            // Act
            var result = Controller.Post(new MrfdetailRequestModel { ReferenceNo= "02/MUM/CFR/AUG/23/007" });
             

            // Assert
            // Verify that the result is an OkObjectResult
            Assert.IsType<MrfdetaiResponseModel>(result);
            var okResult = (MrfdetaiResponseModel)result;

            // Check if the returned response matches the expected response
            Assert.Equal(responseModel.Id, okResult.Id); // Adjust the assertions for other properties

            // Verify that the Mrfdetails was added and Save was called
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfdetail.Add(It.IsAny<Mrfdetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            // Assert
            result.Should().NotBeNull();

        }
        [Fact]
        public void DeleteMrfdetails_ShouldReturnNoContents_WhenDeletedARecord()
        {
            // Arrange

            int existingId = 1; // Replace with an existing Id in your test data

            // Mock the behavior of the Get and Remove methods in IUnitOfWork
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfdetail.Get(It.IsAny<Expression<Func<Mrfdetails, bool>>>()))
                .Returns((Expression<Func<Mrfdetails, bool>> filter) =>
                {
                    // Simulate returning an object when the filter condition matches
                    if (filter.Compile().Invoke(new Mrfdetails { Id = existingId }))
                    {
                        return new Mrfdetails { Id = existingId };
                    }
                    return null;
                });

            // Act
            Controller.Delete(existingId);


            // Assert
            // Verify that the Remove and Save methods of IUnitOfWork were called as expected
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfdetail.Get(It.IsAny<Expression<Func<Mrfdetails, bool>>>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfdetail.Remove(It.IsAny<Mrfdetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }
        [Fact]
        public void DeleteMrfdetails_ShouldReturnNotFound_WhenRecordNotFound()
        {

            // Arrange


            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 999; // An ID that is assumed not to exist.
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfdetail.Get(It.IsAny<Expression<Func<Mrfdetails, bool>>>()))
           .Returns((Expression<Func<Mrfdetails, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfdetail.Remove(It.IsAny<Mrfdetails>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);

        }
        [Fact]
        public void DeleteMrfdetails_ShouldReturnBadResponse_WhenInputIsZero()
        {
            // Arrange

            // Set up your mock IUnitOfWork behavior.
            int nonExistentId = 0;
            fixture.MockUnitOfWork.Setup(uow => uow.Mrfdetail.Get(It.IsAny<Expression<Func<Mrfdetails, bool>>>()))
           .Returns((Expression<Func<Mrfdetails, bool>> predicate) => null);

            // Act
            Controller.Delete(nonExistentId);

            // Assert
            // Verify that the Remove and Save methods were not called since the object does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfdetail.Remove(It.IsAny<Mrfdetails>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact(Skip = "check later")]
        public void UpdateMrfdetails_ShouldReturnIsActiveTrue_WhenRecordIsUpdated()
        {
            // Arrange

            int entityId = 1; // Example ID for an existing entity

            var requestModel = new MrfdetailRequestModel
            {
                ReferenceNo = "02/MUM/CFR/AUG/23/007",
                PositionTitle = "software engineer",
                DepartmentId = 2,
                SubDepartmentId = 1,
                ProjectId = 1,
                VacancyNo = 2,
                ReportsToEmployeeId = 5,
                MinGradeId = 1,
                MaxGradeId = 5,
                EmploymentTypeId = 1,
                MinExperience = 0,
                MaxExperience = 4,
                VacancyTypeId = 3,
                IsReplacement = false,
                MrfStatusId = 2,

            };

            // Mock the behavior of IUnitOfWork's Get method to return an existing entity
            var existingEntity = new Mrfdetails
            {
                Id = entityId,
                

            };

            fixture.MockUnitOfWork.Setup(uow => uow.Mrfdetail.Get(It.IsAny<Expression<Func<Mrfdetails, bool>>>()))
                .Returns((Expression<Func<Mrfdetails, bool>> predicate) => existingEntity);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert
            // Verify that the Update and Save methods were called and the response model contains the updated ID.
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfdetail.Update(It.IsAny<Mrfdetails>()), Times.Once);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Once);

            //Assert.True(result.IsActive);
            Assert.Equal(entityId, result.Id);
        }
        [Fact]
        public void UpdateMrfdetails_ShouldReturnIsActiveFalse_WhenInvalidRequest()
        {
            // Arrange
            int entityId = 999; // Id for which the entity is not found

            var requestModel = new MrfdetailRequestModel
            {

                ReferenceNo = "02/MUM/CFR/AUG/23/007",
                PositionTitle = "software engineer",
                DepartmentId = 2,
                SubDepartmentId = 1,
                ProjectId = 1,
                VacancyNo = 2,
                ReportsToEmployeeId = 5,
                MinGradeId = 5,
                EmploymentTypeId = 1,
                MinExperience = 0,
                MaxExperience = 4,
                VacancyTypeId = 3,
                IsReplacement = false,
                MrfStatusId = 2,

            };

            fixture.MockUnitOfWork.Setup(uow => uow.Mrfdetail.Get(It.IsAny<Expression<Func<Mrfdetails, bool>>>()))
                .Returns((Expression<Func<Mrfdetails, bool>> predicate) => null);

            // Act
            var result = Controller.Put(entityId, requestModel);

            // Assert

            Assert.Equal(0, result.Id);
            //Assert.False(result.IsActive);

            // Verify that the Update and Save methods were not called since the entity does not exist.
            fixture.MockUnitOfWork.Verify(uow => uow.Mrfdetail.Update(It.IsAny<Mrfdetails>()), Times.Never);
            fixture.MockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
        [Fact]
        public void GetMrfDetailsById_ShouldReturnNoResult_WhenInputIsEqualToZero()
        {
            // Arrange

            int id = 0;

            // Create a list of sample Mrfinterviewermap for testing
            var SampleMrfDetails = new List<MrfDetailsViewModel>
            {
            new MrfDetailsViewModel  {MrfId=345,ReferenceNo="02/MUM/CFR/AUG/23/007",MrfStatus= "completed",Name="kritika gupta",},
            new MrfDetailsViewModel {MrfId=345,ReferenceNo="02/MUM/CFR/AUG/23/007",MrfStatus="completed",Name="kritika gupta",},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.MrfStatusDetail.GetAll()).Returns(SampleMrfDetails);

            // Act
            var result = Controller.GetMrfDetails(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:0"));


        }
        [Fact]
        public void GetMrfDetailsById_ShouldReturnNoResult_WhenInputIsLessThanZero()
        {
            // Arrange

            int id = -3;

            // Create a list of sample Mrfinterviewermap for testing
            var SampleMrfDetails = new List<MrfDetailsViewModel>
            {
            new MrfDetailsViewModel  {MrfId=345,ReferenceNo="02/MUM/CFR/AUG/23/007",MrfStatus= "completed",Name="kritika gupta",},
            new MrfDetailsViewModel {MrfId=345,ReferenceNo="02/MUM/CFR/AUG/23/007",MrfStatus="completed",Name="kritika gupta",},

            };

            // Set up the behavior of the mockUnitOfWork to return the sample data
            fixture.MockUnitOfWork.Setup(uow => uow.MrfStatusDetail.GetAll()).Returns(SampleMrfDetails);

            // Act
            var result = Controller.GetMrfDetails(id);

            // Assert
            result.Should().NotBeNull();
            fixture.MockLogger.Verify(logger => logger.LogError("No result found by this Id:-3"));


        }
        

    }
}
