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
        private IEnumerable<Widget> _widgets;
        public WidgetRepositoryTest()
        {
            _widgets = WidgetBuilder.BuildWithId();
        }
        [Fact]
        public async void AddAsyncShouldAddWidget()
        {
            var options = OverwatchOptionBuilder.CreateBuilderWithName("OverwatchDbAddWidgetsAsync");
            using (var overwatchContext = new OverwatchContext(options))
            {
                var widgetRepository = new WidgetRepository(overwatchContext);
                await WidgetRepositoryFillerAsync(widgetRepository, _widgets);
                var result = await widgetRepository.GetAllAsync();
                Assert.Equal(result.Count(), _widgets.Count());
            }
        }
        [Fact]
        public async void GetAllAsyncShouldReturnAllWidgets()
        {
            var options = OverwatchOptionBuilder.CreateBuilderWithName("OverwatchDbGetAllWidgetsAsync");
            using (var overwatchContext = new OverwatchContext(options))
            {
                var widgetRepository = new WidgetRepository(overwatchContext);
                await WidgetRepositoryFillerAsync(widgetRepository, _widgets);
                var result = await widgetRepository.GetAllAsync();
                Assert.Equal(result.Count(), WidgetBuilder.BuildWithId().Count());
                Assert.Equal(result, _widgets);
            }
        }
        [Fact]
        public async void GetByIdAsyncShouldReturnCorrectWidget()
        {
            var options = OverwatchOptionBuilder.CreateBuilderWithName("OverwatchDbGetWidgetByIdAsync");
            using (var overwatchContext = new OverwatchContext(options))
            {
                var widgetRepository = new WidgetRepository(overwatchContext);
                Widget widgetToFind = _widgets.First();
                await widgetRepository.AddAsync(widgetToFind);
                var result = await widgetRepository.GetByIdAsync(widgetToFind.Id);
                Assert.Equal(widgetToFind, result);
            }
        }

        [Fact]
        public async void DeleteByIdAsyncShouldDeleteCorrectWidget()
        {
            var options = OverwatchOptionBuilder.CreateBuilderWithName("OverwatchDbDeleteByIdAsync");
            using (var overwatchContext = new OverwatchContext(options))
            {
                var widgetRepository = new WidgetRepository(overwatchContext);
                Widget widgetToDelete = _widgets.First();
                await widgetRepository.AddAsync(widgetToDelete);
                var result = await widgetRepository.DeleteByIdAsync(widgetToDelete.Id);
                Assert.Equal(1, result);
            }
        }

        [Fact]
        public async void PutAsyncShouldEditCorrectWidget()
        {
            var options = OverwatchOptionBuilder.CreateBuilderWithName("OverwatchDbPutWidgetAsync");
            using (var overwatchContext = new OverwatchContext(options))
            {
                var widgetRepository = new WidgetRepository(overwatchContext);
                await WidgetRepositoryFillerAsync(widgetRepository, _widgets);
                Widget widgetToEdit = _widgets.First();
                widgetToEdit.Color = "Green";
                var result = await widgetRepository.PutAsync(widgetToEdit.Id,widgetToEdit);
                Assert.Equal(1, result);
                var returnedWidget = widgetRepository.GetByIdAsync(widgetToEdit.Id).Result;
                Assert.Equal(returnedWidget,widgetToEdit);
            }
        }

        private async Task WidgetRepositoryFillerAsync(WidgetRepository widgetRepository, IEnumerable<Widget> widgets)
        {
            foreach (var widget in widgets)
            {
                await widgetRepository.AddAsync(widget);
            }
        }
    }
}
