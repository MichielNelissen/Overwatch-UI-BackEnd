using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using OverwatchAPI.Data.Context;
using OverwatchAPI.Data.Repository.Widget;
using OverwatchAPI.Domain.DomainClasses.Widgets;
using OverwatchAPI.Test.Builders;
using Xunit;

namespace OverwatchAPI.Test.Repositories
{
    public class WidgetRepositoryTest
    {
        private IWidgetRepository _widgetRepository;
        private OverwatchContext _overwatchContext;
        private IEnumerable<Widget> _widgets;
        
        public WidgetRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<OverwatchContext>()
                .UseInMemoryDatabase(databaseName: "OverwatchDb")
                .Options;
            _overwatchContext = new OverwatchContext(options);
            _widgets = WidgetBuilder.BuildWithId();
            _widgetRepository = new WidgetRepository(_overwatchContext);
        }

        [Fact]
        public async void GetAllAsyncShouldReturnAllWidgets()
        {
            foreach (var widget in _widgets)
            {
                await _widgetRepository.AddAsync(widget);
            }
            var result = await _widgetRepository.GetAllAsync();
            Assert.Equal(result.Count(), WidgetBuilder.BuildWithId().Count());
            Assert.Equal(result, _widgets);
        }
        [Fact]
        public async void GetByIdAsyncShouldReturnCorrectWidget()
        {
            Widget widgetToFind = new Widget()
            {
                Color = "Red",
                Dashboard = null,
                DashboardId = 1,
                Id = 2,
                Name = "A name"
            };
            await _widgetRepository.AddAsync(widgetToFind);
            var result = await _widgetRepository.GetByIdAsync(widgetToFind.Id);
            Assert.Equal(widgetToFind,result);
        }

    }
}
