using System;
using System.Collections.Generic;
using System.Text;
using OverwatchAPI.Domain.DomainClasses.Projects;
using OverwatchAPI.Domain.DomainClasses.Widgets;

namespace OverwatchAPI.Domain.DomainClasses.Dashboard
{
    public class Dashboard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Widget> Widgets { get; set; }
        public string Description { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
