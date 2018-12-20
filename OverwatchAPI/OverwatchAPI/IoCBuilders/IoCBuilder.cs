using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OverwatchAPI.Data.Context;
using OverwatchAPI.Data.Repository.Dashboard;
using OverwatchAPI.Data.Repository.Project;
using OverwatchAPI.Data.Repository.Widget;
namespace OverwatchAPI.IoCBuilders
{
    public static class IoCBuilder
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            string connectionString = @"Server=(localdb)\\MSSQLLocalDB;Database=OverwatchDb;Trusted_Connection=True;";
            services.AddDbContext<OverwatchContext>
                (options =>
            {
                options.UseSqlServer(connectionString);

            });

            services.AddTransient<OverwatchContext, OverwatchContext>();
            services.AddTransient<IWidgetRepository, WidgetRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IDashboardRepository, DashboardRepository>();
        }
    }
}
