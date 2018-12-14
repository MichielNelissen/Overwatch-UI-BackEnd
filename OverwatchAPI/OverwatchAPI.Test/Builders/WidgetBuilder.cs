using System;
using System.Collections.Generic;
using System.Text;
using OverwatchAPI.Domain.DomainClasses.Widgets;

namespace OverwatchAPI.Test.Builders
{
    public static class WidgetBuilder
    {
        public static IEnumerable<Widget> BuildWithId()
        {
            var listOfWidgets = new List<Widget>();

            for (int i = 1; i <= 10; i++)
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
