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
using Xunit;

namespace OverwatchAPI.Test.Controllers
{
    public class WidgetsControllerTest
    {
        private WidgetsController _widgetsController;
        private Mock<IWidgetRepository> _mockedRepository;


        public WidgetsControllerTest()
        {
            _mockedRepository = new Mock<IWidgetRepository>();
            _widgetsController = new WidgetsController(_mockedRepository.Object);
        }

        [Fact]
        public async Task GetWidgetsWillReturnOkResult()
        {
            //Act
            var result = await _widgetsController.GetWidgets();
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }




        public IEnumerable<Widget> Build()
        {
           var listOfWidgets = new List<Widget>();

            for (int i=1; i <= 10; i++)
            {
                var widget = new Widget
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Color = Guid.NewGuid().ToString(),
                    Dashboard = null,
                    DashboardId = new Random().Next()
                };

                listOfWidgets.Add(widget);
            }

            return listOfWidgets;
        }
    }
}
