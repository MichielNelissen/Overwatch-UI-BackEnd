using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OverwatchAPI.Data.Context;
using OverwatchAPI.Domain.DomainClasses.Widgets;

namespace OverwatchAPI.Data.Repository.Widget
{
    public class WidgetRepository : IWidgetRepository
    {
        private readonly OverwatchContext _context;

        public WidgetRepository(OverwatchContext context)
        {
            _context = context;
        }
        public async Task<Domain.DomainClasses.Widgets.Widget> GetByIdAsync(int id)
        {
            return await _context.Widgets.FindAsync(id);
        }
        public async Task<int> DeleteByIdAsync(int id)
        {
            _context.Remove(GetByIdAsync(id).Result);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> AddAsync(Domain.DomainClasses.Widgets.Widget item)
        {
            await _context.Widgets.AddAsync(item);
            return await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Domain.DomainClasses.Widgets.Widget>> GetAllAsync()
        {
            return await _context.Widgets.ToListAsync();
        }
        public async Task<int> PutAsync(int id, Domain.DomainClasses.Widgets.Widget item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
