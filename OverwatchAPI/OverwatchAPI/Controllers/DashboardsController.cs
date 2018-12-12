using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OverwatchAPI.Data.Context;
using OverwatchAPI.Data.Repository.Dashboard;
using OverwatchAPI.Domain.DomainClasses.Dashboard;

namespace OverwatchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardsController(IDashboardRepository repository)
        {
            _dashboardRepository = repository;
        }

        // GET: api/Dashboards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dashboard>>> GetDashboards()
        {
            var dashboards = _dashboardRepository.GetAllAsync();

            if (dashboards == null)
                return NotFound();

            return Ok(dashboards);
        }

        // GET: api/Dashboards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dashboard>> GetDashboard(int id)
        {
            var dashboard = await _dashboardRepository.GetByIdAsync(id);

            if (dashboard == null)
            {
                return NotFound();
            }

            return dashboard;
        }

        // PUT: api/Dashboards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDashboard(int id, Dashboard dashboard)
        {
            if (id != dashboard.Id)
            {
                return BadRequest();
            }

            var result = await _dashboardRepository.PutAsync(id, dashboard);

            if (result == 0)
                return NotFound();

            return Ok(result);
        }

        // POST: api/Dashboards
        [HttpPost]
        public async Task<ActionResult<Dashboard>> PostDashboard(Dashboard dashboard)
        {
            if (dashboard == null)
                return BadRequest();

            var added = await _dashboardRepository.AddAsync(dashboard);

            return Ok(added);
        }

        // DELETE: api/Dashboards/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dashboard>> DeleteDashboard(int id)
        {
            var result = await _dashboardRepository.DeleteByIdAsync(id);

            if (result == 0)
                return NotFound();

            return Ok(result);
        }


    }
}
