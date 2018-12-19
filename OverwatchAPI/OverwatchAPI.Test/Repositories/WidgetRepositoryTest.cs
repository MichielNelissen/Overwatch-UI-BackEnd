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
        [Fact]
        public async void AddAsyncShouldAddWidget()
        {
            var options = new DbContextOptionsBuilder<OverwatchContext>()
                .UseInMemoryDatabase(databaseName: "OverwatchDbAddWidgetsAsync")
                .Options;
            using (var overwatchContext = new OverwatchContext(options))
            {
                var widgets = WidgetBuilder.BuildWithId();
                var widgetRepository = new WidgetRepository(overwatchContext);
                foreach (var widget in widgets)
                {
                    await widgetRepository.AddAsync(widget);
                }

                var result = await widgetRepository.GetAllAsync();
                Assert.Equal(result.Count(), WidgetBuilder.BuildWithId().Count());
            }
        }
        [Fact]
        public async void GetAllAsyncShouldReturnAllWidgets()
        {
            var options = new DbContextOptionsBuilder<OverwatchContext>()
                .UseInMemoryDatabase(databaseName: "OverwatchDbGetAllWidgetsAsync")
                .Options;
            using (var overwatchContext = new OverwatchContext(options))
            {
                var widgets = WidgetBuilder.BuildWithId();
                var widgetRepository = new WidgetRepository(overwatchContext);

                foreach (var widget in widgets)
                {
                    await widgetRepository.AddAsync(widget);
                }
                var result = await widgetRepository.GetAllAsync();
                Assert.Equal(result.Count(), WidgetBuilder.BuildWithId().Count());
                Assert.Equal(result, widgets);
            }
        }
        [Fact]
        public async void GetByIdAsyncShouldReturnCorrectWidget()
        {
            var options = new DbContextOptionsBuilder<OverwatchContext>()
                .UseInMemoryDatabase(databaseName: "OverwatchDbGetWidgetByIdAsync")
                .Options;
            using (var overwatchContext = new OverwatchContext(options))
            {
                var widgetRepository = new WidgetRepository(overwatchContext);
                Widget widgetToFind = new Widget()
                {
                    Color = "Red",
                    Dashboard = null,
                    DashboardId = 1,
                    Id = 2,
                    Name = "A name"
                };
                await widgetRepository.AddAsync(widgetToFind);
                var result = await widgetRepository.GetByIdAsync(widgetToFind.Id);
                Assert.Equal(widgetToFind, result);
            }
        }

        [Fact]
        public async void DeleteByIdAsyncShouldDeleteCorrectWidget()
        {
            var options = new DbContextOptionsBuilder<OverwatchContext>()
                .UseInMemoryDatabase(databaseName: "OverwatchDbDeleteByIdAsync")
                .Options;
            using (var overwatchContext = new OverwatchContext(options))
            {
                var widgetRepository = new WidgetRepository(overwatchContext);
                Widget widgetToDelete = new Widget()
                {
                    Color = "Red",
                    Dashboard = null,
                    DashboardId = 1,
                    Id = 1,
                    Name = "A name"
                };
                await widgetRepository.AddAsync(widgetToDelete);
                var result = await widgetRepository.DeleteByIdAsync(widgetToDelete.Id);
                Assert.Equal(1, result);
            }
        }
    }
}
