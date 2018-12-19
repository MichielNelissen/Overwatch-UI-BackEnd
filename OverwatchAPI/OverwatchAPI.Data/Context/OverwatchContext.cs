using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using OverwatchAPI.Domain.DomainClasses.Dashboard;
using OverwatchAPI.Domain.DomainClasses.Projects;
using OverwatchAPI.Domain.DomainClasses.Widgets;

namespace OverwatchAPI.Data.Context
{
    public class OverwatchContext : DbContext
    {
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Widget> Widgets { get; set; }

        public OverwatchContext()
        {
        }
        public OverwatchContext(DbContextOptions<OverwatchContext> options)
            : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<Dashboard> dashboardList = new List<Dashboard>();
            dashboardList.Add(new Dashboard() { Id = 1, Name = "First", Description = "dit is een disc", ProjectId = 1,Widgets = null});

            List<Widget> widgetList = new List<Widget>();
            for (int i = 1; i < 6; i++)
            {
                widgetList.Add(new Widget() { Id = i, Color = "Red" + i, Name = "Demo1" + i, DashboardId = 1 });
            }
            Project project = new Project() { Id = 1, Name = "Overwatch", Url = "an url" };

            modelBuilder.Entity<Project>().HasData(project);
            modelBuilder.Entity<Dashboard>().HasData(dashboardList);
            modelBuilder.Entity<Widget>().HasData(widgetList);

        }
    }
}
