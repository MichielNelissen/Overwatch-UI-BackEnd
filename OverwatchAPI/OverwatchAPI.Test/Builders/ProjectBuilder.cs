using System;
using System.Collections.Generic;
using OverwatchAPI.Domain.DomainClasses.Projects;

namespace OverwatchAPI.Test.Builders
{
    public static class ProjectBuilder
    {
        public static IEnumerable<Project> BuildWithId()
        {
            var listOfProjects = new List<Project>();

            for (int i = 1; i <= 10; i++)
            {
                var project = new Project()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Dashboards = null,
                    Url = Guid.NewGuid().ToString()
                };

                listOfProjects.Add(project);
            }

            return listOfProjects;
        }
    }
}