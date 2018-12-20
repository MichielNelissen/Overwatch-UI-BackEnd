using System;
using System.Collections.Generic;
using OverwatchAPI.Domain.DomainClasses.Dashboard;

namespace OverwatchAPI.Test.Builders
{
    public static class DashboardBuilder
    {
        public static IEnumerable<Dashboard> BuildWithId()
        {
            var listOfDashboards = new List<Dashboard>();

            for (int i = 1; i <= 10; i++)
            {
                var project = new Dashboard()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Description = Guid.NewGuid().ToString(),
                    Project = null,
                    ProjectId = 0,
                    Widgets = null
                };

                listOfDashboards.Add(project);
            }

            return listOfDashboards;
        }
    }
}