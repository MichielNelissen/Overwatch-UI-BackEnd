namespace OverwatchAPI.Domain.DomainClasses.Widgets
{
    public class Widget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public int DashboardId { get; set; }
        public Dashboard.Dashboard Dashboard { get; set; }
    }
}
