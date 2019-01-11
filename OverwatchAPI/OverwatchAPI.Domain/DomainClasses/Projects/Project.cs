using System.Collections.Generic;

namespace OverwatchAPI.Domain.DomainClasses.Projects
{
    public class Project
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public List<Dashboard.Dashboard> Dashboards { get; set; }
    }
}
