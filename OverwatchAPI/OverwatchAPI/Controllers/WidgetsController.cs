﻿using System;
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
        private readonly IWidgetRepository _widgetRepository;

        public WidgetsController(IWidgetRepository widgetRepository)//OverwatchContext context)
        {
            _widgetRepository = widgetRepository;
        }

        // GET: api/Widgets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Widget>>> GetWidgets()
        {
            var widgets = await _widgetRepository.GetAllAsync();
            return widgets.ToList();
        }

        // GET: api/Widgets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Widget>> GetWidget(int id)
        {
            var widget = await _widgetRepository.GetByIdAsync(id);
            return widget;
        }

        // PUT: api/Widgets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWidget(int id, Widget widget)
        {
            var result = await _widgetRepository.PutAsync(id, widget);
            return NoContent();
        }

        // POST: api/Widgets
        [HttpPost]
        public async Task<ActionResult<Widget>> PostWidget(Widget widget)
        {
            var addedWidget = await _widgetRepository.AddAsync(widget);
            
            return Ok(addedWidget);
        }

        // DELETE: api/Widgets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Widget>> DeleteWidget(int id)
        {
            var widget = await _widgetRepository.DeleteByIdAsync(id);
            return Ok(widget);
        }
    }
}
