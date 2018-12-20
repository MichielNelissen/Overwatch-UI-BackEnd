using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OverwatchAPI.Data.Context;
using OverwatchAPI.Data.Repository.Project;
using OverwatchAPI.Domain.DomainClasses.Projects;

namespace OverwatchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IProjectRepository repository)
        {
            _projectRepository = repository;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await _projectRepository.GetAllAsync();

            if (projects == null)
                return NotFound();

            return Ok(projects);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            var result = await _projectRepository.PutAsync(id, project);

            if (result == 0)
                return NotFound();

            return Ok(result);
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            if (project == null)
                return BadRequest();

            var addedProject = await _projectRepository.AddAsync(project);
            if (addedProject == 0)
                return NotFound();
            return Ok(addedProject);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> DeleteProject(int id)
        {
            var project = await _projectRepository.DeleteByIdAsync(id);

            if (project == 0)
                return NotFound();

            return Ok(project);
        }
    }
}
