using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OverwatchAPI.Controllers;
using OverwatchAPI.Data.Repository.Dashboard;
using OverwatchAPI.Data.Repository.Project;
using OverwatchAPI.Domain.DomainClasses.Dashboard;
using OverwatchAPI.Test.Builders;
using Xunit;

namespace OverwatchAPI.Test.Controllers
{
    public class DashboardControllerTest
    {
        private DashboardsController _dashboardsController;
        private Mock<IDashboardRepository> _mockedRepository;
        private IEnumerable<Dashboard> _dashboards;

        public DashboardControllerTest()
        {
            _mockedRepository = new Mock<IDashboardRepository>();
            _dashboardsController = new DashboardsController(_mockedRepository.Object);
            _dashboards = DashboardBuilder.BuildWithId();
        }

        [Fact]
        public async void GetDashboardsWillReturnOkResult()
        {
            _mockedRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(_dashboards);
            var actionResult = await _dashboardsController.GetDashboards();
            var result = actionResult.Result as OkObjectResult;
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(_dashboards, result.Value);
        }

        [Fact]
        public async void GetDashboardsWillReturnNotFoundWhenNull()
        {
            _mockedRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(() => null);
            var myController = new DashboardsController(_mockedRepository.Object);
            var actionResult = await myController.GetDashboards();
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async void GetDashboardsByIdWillReturnOkResult()
        {
            _mockedRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_dashboards.First);
            var myController = new DashboardsController(_mockedRepository.Object);
            var actionResult = await myController.GetDashboards();
            OkObjectResult result = actionResult.Result as OkObjectResult;
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(_dashboards.First(), result.Value);
        }

        [Fact]
        public async void GetDashboardsByIdWillReturnNotFoundWhenDashboardsIsNull()
        {
            _mockedRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);
            var myController = new DashboardsController(_mockedRepository.Object);
            var actionResult = await myController.GetDashboard(It.IsAny<int>());
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async void PutDashboardsWillReturnBadRequestWhenIdsDoNotMatch()
        {
            var myController = new DashboardsController(_mockedRepository.Object);
            var actionResult = await myController.PutDashboard(_dashboards.First().Id + 1, _dashboards.First());
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void PutDashboardsWillReturnOkResultWhenEverythingIsOk()
        {
            _mockedRepository.Setup(x => x.PutAsync(It.IsAny<int>(), It.IsAny<Dashboard>())).ReturnsAsync(1);
            var myController = new DashboardsController(_mockedRepository.Object);
            var actionResult = await myController.PutDashboard(_dashboards.First().Id, _dashboards.First());
            Assert.NotNull(actionResult);
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async void PutDashboardsWillReturnNotFoundWhenThereAreNoChanges()
        {
            _mockedRepository.Setup(x => x.PutAsync(It.IsAny<int>(), It.IsAny<Dashboard>())).ReturnsAsync(0);
            var myController = new DashboardsController(_mockedRepository.Object);
            var actionResult = await myController.PutDashboard(_dashboards.First().Id, _dashboards.First());
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void PostDashboardsWillReturnBadRequestForEmptyDashboards()
        {
            var myController = new DashboardsController(_mockedRepository.Object);
            var actionResult = await myController.PostDashboard(null);
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void PostDashboardsWithWrongDataWillReturnNotFound()
        {
            _mockedRepository.Setup(x => x.AddAsync(It.IsAny<Dashboard>())).ReturnsAsync(0);
            var myController = new DashboardsController(_mockedRepository.Object);
            var actionResult = await myController.PostDashboard(_dashboards.First());
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async void PostDashboardsWillReturnOkResultWhenEverythingIsOk()
        {

            _mockedRepository.Setup(x => x.AddAsync(It.IsAny<Dashboard>())).ReturnsAsync(1);
            var myController = new DashboardsController(_mockedRepository.Object);
            var actionResult = await myController.PostDashboard(_dashboards.First());
            Assert.NotNull(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void DeleteDashboardsWithWrongIdWillReturnNotFound()
        {
            _mockedRepository.Setup(x => x.DeleteByIdAsync(It.IsAny<int>())).ReturnsAsync(0);
            var myController = new DashboardsController(_mockedRepository.Object);
            var actionResult = await myController.DeleteDashboard(It.IsAny<int>());
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
        [Fact]
        public async void DeleteDashboardsWithCorrectIdWillReturnOk()
        {
            _mockedRepository.Setup(x => x.DeleteByIdAsync(It.IsAny<int>())).ReturnsAsync(1);
            var myController = new DashboardsController(_mockedRepository.Object);
            var actionResult = await myController.DeleteDashboard(It.IsAny<int>());
            Assert.NotNull(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }
    }
}