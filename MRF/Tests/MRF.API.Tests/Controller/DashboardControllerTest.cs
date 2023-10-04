//using MRF.Models.DTO;
//using MRF.Models.Models;
//using Moq;
//using FluentAssertions;
//using System.Linq.Expressions;
//using MRF.API.Controllers;
//using Azure.Core;
//using MRF.Models.ViewModels;

//namespace MRF.API.Test.Controllers
//{
//    public class DashboardControllerTest
//    {
//        private readonly TestFixture fixture;
//        private DashboardController Controller;

//        public DashboardControllerTest()
//        {
//            fixture = new TestFixture();
//            Controller = new DashboardController(fixture.MockUnitOfWork.Object, fixture.MockLogger.Object);

//        }

//        [Fact]
//        public void DashboardControllerTest_Constructor_ShouldInitializeDependencies()
//        {
//            // Assert
//            Assert.NotNull(Controller);

//        }

//        [Fact]
//        public void GetMrfStatusSummary_ShouldReturnCount_WhenDataFound()
//        {
           

//            // Create a list of sample Projectmaster for testing
//            var SampleMrfStatusSummaryDetails = new List<MrfSummaryViewModel>
//            {
//            new MrfSummaryViewModel {Status="in progress",MrfStatusId=2},
//            new MrfSummaryViewModel { Status="in progress",MrfStatusId=2},

//            };
//            var sampleMrfDeshboardSummary = new List<MrfSummaryViewModel>(SampleMrfStatusSummaryDetails);

//            // Set up the behavior of the mockUnitOfWork to return the sample data
//            fixture.MockUnitOfWork.Setup(uow => uow.Dashboard.GetAll()).Returns((IEnumerable<MrDashboardViewModel>)sampleMrfDeshboardSummary);


//            // Act
//            var result = Controller.GetMrfStatusSummary();

//            // Assert
//            Assert.Equal(SampleMrfStatusSummaryDetails.Count, result.Count);
//            Assert.NotNull(result);
//            result.Should().NotBeNull();
//            fixture.MockUnitOfWork.Verify(x => x.Dashboard, Times.Once());
//        }


//        [Fact]
//        public void GetMrfStatusSummary_ShouldReturnNotFound_WhenDataNotFound()
//        {

//            fixture.MockUnitOfWork.Setup(x => x.Dashboard.GetAll()).Returns(new List<MrDashboardViewModel>());


//            // Act
//            var result = Controller.GetMrfStatusSummary();

//            // Assert
//            result.Should().NotBeNull();
//            fixture.MockLogger.Verify(logger => logger.LogError("No record is found"));
//            fixture.MockUnitOfWork.Verify(x => x.Dashboard, Times.Once());
//        }

//    }
//}
