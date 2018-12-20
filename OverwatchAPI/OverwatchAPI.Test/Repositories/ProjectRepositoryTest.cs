using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OverwatchAPI.Data.Context;
using OverwatchAPI.Data.Repository.Project;
using OverwatchAPI.Data.Repository.Widget;
using OverwatchAPI.Domain.DomainClasses.Projects;
using OverwatchAPI.Domain.DomainClasses.Widgets;
using OverwatchAPI.Test.Builders;
using Xunit;

namespace OverwatchAPI.Test.Repositories
{
    public class ProjectRepositoryTest
    {
        private IEnumerable<Project> _projects;
        public ProjectRepositoryTest()
        {
            _projects = ProjectBuilder.BuildWithId();
        }
        [Fact]
        public async void AddAsyncShouldAddProject()
        {
            var options = OverwatchOptionBuilder.CreateBuilderWithName("OverwatchDbAddProjectsAsync");
            using (var overwatchContext = new OverwatchContext(options))
            {
                var projectRepository = new ProjectRepository(overwatchContext);
                await ProjectRepositoryFillerAsync(projectRepository, _projects);
                var result = await projectRepository.GetAllAsync();
                Assert.Equal(result.Count(), _projects.Count());
            }
        }
        [Fact]
        public async void GetAllAsyncShouldReturnAllProjects()
        {
            var options = OverwatchOptionBuilder.CreateBuilderWithName("OverwatchDbGetAllProjectsAsync");
            using (var overwatchContext = new OverwatchContext(options))
            {
                var projectRepository = new ProjectRepository(overwatchContext);
                await ProjectRepositoryFillerAsync(projectRepository, _projects);
                var result = await projectRepository.GetAllAsync();
                Assert.Equal(result.Count(), _projects.Count());
                Assert.Equal(result, _projects);
            }
        }
        [Fact]
        public async void GetByIdAsyncShouldReturnCorrectProject()
        {
            var options = OverwatchOptionBuilder.CreateBuilderWithName("OverwatchDbGetProjectByIdAsync");
            using (var overwatchContext = new OverwatchContext(options))
            {
                var projectRepository = new ProjectRepository(overwatchContext);
                Project projectToFind = _projects.First();
                await projectRepository.AddAsync(projectToFind);
                var result = await projectRepository.GetByIdAsync(projectToFind.Id);
                Assert.Equal(projectToFind, result);
            }
        }

        [Fact]
        public async void DeleteByIdAsyncShouldDeleteCorrectProject()
        {
            var options = OverwatchOptionBuilder.CreateBuilderWithName("OverwatchDbDeleteProjectByIdAsync");
            using (var overwatchContext = new OverwatchContext(options))
            {
                var projectRepository = new ProjectRepository(overwatchContext);
                Project projectToDelete = _projects.First();
                await projectRepository.AddAsync(projectToDelete);
                var result = await projectRepository.DeleteByIdAsync(projectToDelete.Id);
                Assert.Equal(1, result);
            }
        }

        [Fact]
        public async void PutAsyncShouldEditCorrectProject()
        {
            var options = OverwatchOptionBuilder.CreateBuilderWithName("OverwatchDbPutProjectAsync");
            using (var overwatchContext = new OverwatchContext(options))
            {
                var projectRepository = new ProjectRepository(overwatchContext);
                await ProjectRepositoryFillerAsync(projectRepository, _projects);
                Project projectToEdit = _projects.First();
                projectToEdit.Url = "Green";
                var result = await projectRepository.PutAsync(projectToEdit.Id, projectToEdit);
                Assert.Equal(1, result);
                var returnedproject = projectRepository.GetByIdAsync(projectToEdit.Id).Result;
                Assert.Equal(returnedproject, projectToEdit);
            }
        }

        private async Task ProjectRepositoryFillerAsync(ProjectRepository projectRepository, IEnumerable<Project> projects)
        {
            foreach (var project in projects)
            {
                await projectRepository.AddAsync(project);
            }
        }
    }
}