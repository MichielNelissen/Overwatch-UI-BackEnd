using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OverwatchAPI.Controllers;
using OverwatchAPI.Data.Repository.Widget;
using OverwatchAPI.Domain.DomainClasses.Widgets;
using OverwatchAPI.Test.Builders;
using Xunit;

namespace OverwatchAPI.Test.Controllers
{
    public class WidgetsControllerTest
    {
        private WidgetsController _widgetsController;
        private Mock<IWidgetRepository> _mockedRepository;
        private IEnumerable<Widget> _widgets;

        public WidgetsControllerTest()
        {
            _mockedRepository = new Mock<IWidgetRepository>();
            _widgetsController = new WidgetsController(_mockedRepository.Object);
            _widgets = WidgetBuilder.BuildWithId();
        }

        [Fact]
        public async void GetWidgetsWillReturnOkResult()
        {
            _mockedRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(_widgets);
            var actionResult = await _widgetsController.GetWidgets();
            var result = actionResult.Result as OkObjectResult;
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(_widgets, result.Value);
        }

        [Fact]
        public async void GetWidgetWillReturnNotFoundWhenNull()
        {
            _mockedRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(() => null);
            var myController = new WidgetsController(_mockedRepository.Object);
            var actionResult = await myController.GetWidgets();
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async void GetWidgetByIdWillReturnOkResult()
        {
            _mockedRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_widgets.First);
            var myController = new WidgetsController(_mockedRepository.Object);
            var actionResult = await myController.GetWidget(It.IsAny<int>());
            OkObjectResult result = actionResult.Result as OkObjectResult;
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(_widgets.First(), result.Value);
        }

        [Fact]
        public async void GetWidgetByIdWillReturnNotFoundWhenWidgetIsNull()
        {
            _mockedRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);
            var myController = new WidgetsController(_mockedRepository.Object);
            var actionResult = await myController.GetWidget(It.IsAny<int>());
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async void PutWidgetWillReturnBadRequestWhenIdsDoNotMatch()
        {
            var myController = new WidgetsController(_mockedRepository.Object);
            var actionResult = await myController.PutWidget(_widgets.First().Id + 1, _widgets.First());
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void PutWidgetWillReturnOkResultWhenEverythingIsOk()
        {
            _mockedRepository.Setup(x => x.PutAsync(It.IsAny<int>(), It.IsAny<Widget>())).ReturnsAsync(1);
            var myController = new WidgetsController(_mockedRepository.Object);
            var actionResult = await myController.PutWidget(_widgets.First().Id, _widgets.First());
            Assert.NotNull(actionResult);
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async void PutWidgetWillReturnNotFoundWhenThereAreNoChanges()
        {
            _mockedRepository.Setup(x => x.PutAsync(It.IsAny<int>(), It.IsAny<Widget>())).ReturnsAsync(0);
            var myController = new WidgetsController(_mockedRepository.Object);
            var actionResult = await myController.PutWidget(_widgets.First().Id, _widgets.First());
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void PostWidgetWillReturnBadRequestForEmptyWidget()
        {
            var myController = new WidgetsController(_mockedRepository.Object);
            var actionResult = await myController.PostWidget(null);
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void PostWidgetWithWrongDataWillReturnNotFound()
        {
            _mockedRepository.Setup(x => x.AddAsync(It.IsAny<Widget>())).ReturnsAsync(0);
            var myController = new WidgetsController(_mockedRepository.Object);
            var actionResult = await myController.PostWidget(_widgets.First());
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async void PostWidgetWillReturnOkResultWhenEverythingIsOk()
        {

            _mockedRepository.Setup(x => x.AddAsync(It.IsAny<Widget>())).ReturnsAsync(1);
            var myController = new WidgetsController(_mockedRepository.Object);
            var actionResult = await myController.PostWidget(_widgets.First());
            Assert.NotNull(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void DeleteWidgetWithWrongIdWillReturnNotFound()
        {
            _mockedRepository.Setup(x => x.DeleteByIdAsync(It.IsAny<int>())).ReturnsAsync(0);
            var myController = new WidgetsController(_mockedRepository.Object);
            var actionResult = await myController.DeleteWidget(It.IsAny<int>());
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
        [Fact]
        public async void DeleteWidgetWithCorrectIdWillReturnOk()
        {
            _mockedRepository.Setup(x => x.DeleteByIdAsync(It.IsAny<int>())).ReturnsAsync(1);
            var myController = new WidgetsController(_mockedRepository.Object);
            var actionResult = await myController.DeleteWidget(It.IsAny<int>());
            Assert.NotNull(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }
    }
}
