using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OverwatchAPI.Data.Context;
using OverwatchAPI.Data.Repository.Dashboard;
using OverwatchAPI.Data.Repository.Project;
using OverwatchAPI.Domain.DomainClasses.Dashboard;
using OverwatchAPI.Domain.DomainClasses.Projects;
using OverwatchAPI.Test.Builders;
using Xunit;

namespace OverwatchAPI.Test.Repositories
{
    public class DashboardRepositoryTest
    {
        private IEnumerable<Dashboard> _dashboards;
        public DashboardRepositoryTest()
        {
            _dashboards = DashboardBuilder.BuildWithId();
        }
        [Fact]
        public async void AddAsyncShouldAddDashboard()
        {
            var options = OverwatchOptionBuilder.CreateBuilderWithName("OverwatchDbAddDashboardsAsync");
            using (var overwatchContext = new OverwatchContext(options))
            {
                var dashboardRepository = new DashboardRepository(overwatchContext);
                await DashboardRepositoryFillerAsync(dashboardRepository, _dashboards);
                var result = await dashboardRepository.GetAllAsync();
                Assert.Equal(result.Count(), _dashboards.Count());
            }
        }
        [Fact]
        public async void GetAllAsyncShouldReturnAllDashboards()
        {
            var options = OverwatchOptionBuilder.CreateBuilderWithName("OverwatchDbGetAllDashboardsAsync");
            using (var overwatchContext = new OverwatchContext(options))
            {
                var dashboardRepository = new DashboardRepository(overwatchContext);
                await DashboardRepositoryFillerAsync(dashboardRepository, _dashboards);
                var result = await dashboardRepository.GetAllAsync();
                Assert.Equal(result.Count(), _dashboards.Count());
                Assert.Equal(result, _dashboards);
            }
        }
        [Fact]
        public async void GetByIdAsyncShouldReturnCorrectDashboard()
        {
            var options = OverwatchOptionBuilder.CreateBuilderWithName("OverwatchDbGetDashboardByIdAsync");
            using (var overwatchContext = new OverwatchContext(options))
            {
                var dashboardRepository = new DashboardRepository(overwatchContext);
                Dashboard dashboardToFind = _dashboards.First();
                await dashboardRepository.AddAsync(dashboardToFind);
                var result = await dashboardRepository.GetByIdAsync(dashboardToFind.Id);
                Assert.Equal(dashboardToFind, result);
            }
        }

        [Fact]
        public async void DeleteByIdAsyncShouldDeleteCorrectDashboard()
        {
            var options = OverwatchOptionBuilder.CreateBuilderWithName("OverwatchDbDeleteDashboardByIdAsync");
            using (var overwatchContext = new OverwatchContext(options))
            {
                var dashboardRepository = new DashboardRepository(overwatchContext);
                Dashboard dashboardToDelete = _dashboards.First();
                await dashboardRepository.AddAsync(dashboardToDelete);
                var result = await dashboardRepository.DeleteByIdAsync(dashboardToDelete.Id);
                Assert.Equal(1, result);
            }
        }

        [Fact]
        public async void PutAsyncShouldEditCorrectDashboard()
        {
            var options = OverwatchOptionBuilder.CreateBuilderWithName("OverwatchDbPutDashboardAsync");
            using (var overwatchContext = new OverwatchContext(options))
            {
                var dashboardRepository = new DashboardRepository(overwatchContext);
                await DashboardRepositoryFillerAsync(dashboardRepository, _dashboards);
                Dashboard dashboardToEdit = _dashboards.First();
                dashboardToEdit.Description = "Green";
                var result = await dashboardRepository.PutAsync(dashboardToEdit.Id, dashboardToEdit);
                Assert.Equal(1, result);
                var returneddashboard = dashboardRepository.GetByIdAsync(dashboardToEdit.Id).Result;
                Assert.Equal(returneddashboard, dashboardToEdit);
            }
        }

        [Fact]
        public async void GetDashboardByProjectIdShouldReturnCorrectDashboard()
        {
            var options = OverwatchOptionBuilder.CreateBuilderWithName("OverwatchDbGetDashboardByProjectIdAsync");
            using (var overwatchContext = new OverwatchContext(options))
            {
                var dashboardRepository = new DashboardRepository(overwatchContext);
                await DashboardRepositoryFillerAsync(dashboardRepository, _dashboards);
                var result = await dashboardRepository.GetDashboardByProjectId(_dashboards.First().ProjectId);
                Assert.Equal(_dashboards, result);
            }
        }

        private async Task DashboardRepositoryFillerAsync(DashboardRepository dashboardRepository, IEnumerable<Dashboard> dashboards)
        {
            foreach (var dashboard in dashboards)
            {
                await dashboardRepository.AddAsync(dashboard);
            }
        }
    }
}