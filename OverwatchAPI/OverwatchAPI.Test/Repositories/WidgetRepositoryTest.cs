using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using OverwatchAPI.Data.Context;
using OverwatchAPI.Data.Repository.Widget;
using OverwatchAPI.Domain.DomainClasses.Widgets;
using Xunit;

namespace OverwatchAPI.Test.Repositories
{
    public class WidgetRepositoryTest
    {
        private Mock<OverwatchContext> _mockedContext;
        private Mock<IWidgetRepository> _mockedRepository;
        private WidgetRepository _widgetRepository;


        public WidgetRepositoryTest()
        {
            _mockedContext = new Mock<OverwatchContext>();
            _mockedRepository = new Mock<IWidgetRepository>();
            _widgetRepository = new WidgetRepository(_mockedContext.Object);
        }

        [Fact]
        public async Task GetByIdWillReturnAWidget()
        {
            //Arrange
            var random = new Random().Next();
            var widget = Build();

            _mockedRepository.Setup(x => x.GetByIdAsync(random)).ReturnsAsync(widget);

            //Act
            var result = await _widgetRepository.GetByIdAsync(random);

            //Assert
            Assert.NotNull(result);
           // _mockedRepository.Verify(repo => repo.GetByIdAsync(random), Times.Once);
        }

        public Widget Build()
        {
            return new Widget
            {
                Id = 1,
                Name = Guid.NewGuid().ToString(),
                Color = Guid.NewGuid().ToString(),
                Dashboard = null,
                DashboardId = new Random().Next()
            };
        }
    }
}
