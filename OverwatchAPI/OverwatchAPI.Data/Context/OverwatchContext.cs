using System;
using System.Collections.Generic;
using System.Text;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=OverwatchDb;Trusted_Connection=True;");
        }
    }
}
