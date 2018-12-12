using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OverwatchAPI.Data.Context;
using OverwatchAPI.Data.Repository.Widget;
using OverwatchAPI.Domain.DomainClasses.Widgets;

namespace OverwatchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WidgetsController : ControllerBase
    {
        private readonly OverwatchContext _context;
        private readonly WidgetRepository _widgetRepository;

        public WidgetsController()//OverwatchContext context)
        {
            _context = new OverwatchContext();
            _widgetRepository = new WidgetRepository(_context);
        }

        // GET: api/Widgets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Widget>>> GetWidgets()
        {
            var widgets = await _widgetRepository.GetAllAsync();

            if (widgets == null)
                return NotFound();

            return Ok(widgets.ToList());
        }

        // GET: api/Widgets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Widget>> GetWidget(int id)
        {
            var widget = await _widgetRepository.GetByIdAsync(id);

            if (widget == null)
                return NotFound();

            return Ok(widget);
        }

        // PUT: api/Widgets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWidget(int id, Widget widget)
        {
            if (widget == null)
                return BadRequest();

            var result = await _widgetRepository.PutAsync(id, widget);

            if (result == 0)
                return NotFound();

            return Ok(result);
        }

        // POST: api/Widgets
        [HttpPost]
        public async Task<ActionResult<Widget>> PostWidget(Widget widget)
        {
            if (widget == null)
                return BadRequest();

            var addedWidget = await _widgetRepository.AddAsync(widget);

            return Ok(addedWidget);
        }

        // DELETE: api/Widgets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Widget>> DeleteWidget(int id)
        {
            var widget = await _widgetRepository.DeleteByIdAsync(id);

            if (widget == 0)
               return NotFound();

            return Ok(widget);
        }
    }
}
