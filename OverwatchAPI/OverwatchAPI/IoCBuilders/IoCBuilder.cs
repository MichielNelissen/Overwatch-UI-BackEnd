﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OverwatchAPI.Data.Context;
using OverwatchAPI.Data.Repository.Dashboard;
using OverwatchAPI.Data.Repository.Project;
using OverwatchAPI.Data.Repository.Widget;
namespace OverwatchAPI.IoCBuilders
{
    public static class IoCBuilder
    {
        public static void RegesterDependencies(IServiceCollection services)
        {
            services.AddTransient<OverwatchContext, OverwatchContext>();
            services.AddTransient<IWidgetRepository, WidgetRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IDashboardRepository, DashboardRepository>();
        }
    }
}
