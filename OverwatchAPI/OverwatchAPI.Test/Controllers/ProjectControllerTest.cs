using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OverwatchAPI.Controllers;
using OverwatchAPI.Data.Repository.Project;
using OverwatchAPI.Data.Repository.Widget;
using OverwatchAPI.Domain.DomainClasses.Projects;
using OverwatchAPI.Domain.DomainClasses.Widgets;
using OverwatchAPI.Test.Builders;
using Xunit;

namespace OverwatchAPI.Test.Controllers
{
    public class ProjectControllerTest
    {
        private ProjectsController _projectController;
        private Mock<IProjectRepository> _mockedRepository;
        private IEnumerable<Project> _projects;

        public ProjectControllerTest()
        {
            _mockedRepository = new Mock<IProjectRepository>();
            _projectController = new ProjectsController(_mockedRepository.Object);
            _projects = ProjectBuilder.BuildWithId();
        }

        [Fact]
        public async void GetProjectsWillReturnOkResult()
        {
            _mockedRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(_projects);
            var actionResult = await _projectController.GetProjects();
            var result = actionResult.Result as OkObjectResult;
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(_projects, result.Value);
        }

        [Fact]
        public async void GetProjectsWillReturnNotFoundWhenNull()
        {
            _mockedRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(() => null);
            var myController = new ProjectsController(_mockedRepository.Object);
            var actionResult = await myController.GetProjects();
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async void GetProjectsByIdWillReturnOkResult()
        {
            _mockedRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_projects.First);
            var myController = new ProjectsController(_mockedRepository.Object);
            var actionResult = await myController.GetProject(It.IsAny<int>());
            OkObjectResult result = actionResult.Result as OkObjectResult;
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(_projects.First(), result.Value);
        }

        [Fact]
        public async void GetProjectsByIdWillReturnNotFoundWhenProjectsIsNull()
        {
            _mockedRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);
            var myController = new ProjectsController(_mockedRepository.Object);
            var actionResult = await myController.GetProject(It.IsAny<int>());
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async void PutProjectsWillReturnBadRequestWhenIdsDoNotMatch()
        {
            var myController = new ProjectsController(_mockedRepository.Object);
            var actionResult = await myController.PutProject(_projects.First().Id + 1, _projects.First());
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void PutProjectsWillReturnOkResultWhenEverythingIsOk()
        {
            _mockedRepository.Setup(x => x.PutAsync(It.IsAny<int>(), It.IsAny<Project>())).ReturnsAsync(1);
            var myController = new ProjectsController(_mockedRepository.Object);
            var actionResult = await myController.PutProject(_projects.First().Id, _projects.First());
            Assert.NotNull(actionResult);
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async void PutProjectsWillReturnNotFoundWhenThereAreNoChanges()
        {
            _mockedRepository.Setup(x => x.PutAsync(It.IsAny<int>(), It.IsAny<Project>())).ReturnsAsync(0);
            var myController = new ProjectsController(_mockedRepository.Object);
            var actionResult = await myController.PutProject(_projects.First().Id, _projects.First());
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void PostProjectsWillReturnBadRequestForEmptyProjects()
        {
            var myController = new ProjectsController(_mockedRepository.Object);
            var actionResult = await myController.PostProject(null);
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void PostProjectsWithWrongDataWillReturnNotFound()
        {
            _mockedRepository.Setup(x => x.AddAsync(It.IsAny<Project>())).ReturnsAsync(0);
            var myController = new ProjectsController(_mockedRepository.Object);
            var actionResult = await myController.PostProject(_projects.First());
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async void PostProjectsWillReturnOkResultWhenEverythingIsOk()
        {

            _mockedRepository.Setup(x => x.AddAsync(It.IsAny<Project>())).ReturnsAsync(1);
            var myController = new ProjectsController(_mockedRepository.Object);
            var actionResult = await myController.PostProject(_projects.First());
            Assert.NotNull(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void DeleteProjectsWithWrongIdWillReturnNotFound()
        {
            _mockedRepository.Setup(x => x.DeleteByIdAsync(It.IsAny<int>())).ReturnsAsync(0);
            var myController = new ProjectsController(_mockedRepository.Object);
            var actionResult = await myController.DeleteProject(It.IsAny<int>());
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
        [Fact]
        public async void DeleteProjectsWithCorrectIdWillReturnOk()
        {
            _mockedRepository.Setup(x => x.DeleteByIdAsync(It.IsAny<int>())).ReturnsAsync(1);
            var myController = new ProjectsController(_mockedRepository.Object);
            var actionResult = await myController.DeleteProject(It.IsAny<int>());
            Assert.NotNull(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }
    }
}