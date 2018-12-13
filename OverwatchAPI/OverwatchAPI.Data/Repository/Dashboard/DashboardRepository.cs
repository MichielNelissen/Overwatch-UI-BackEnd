using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OverwatchAPI.Data.Context;

namespace OverwatchAPI.Data.Repository.Dashboard
{
  public  class DashboardRepository: IDashboardRepository
    {
        private readonly OverwatchContext _context;

        public DashboardRepository(OverwatchContext context)
        {
            _context = context;
        }
        public async Task<Domain.DomainClasses.Dashboard.Dashboard> GetByIdAsync(int id)
        {
            return await _context.Dashboards.FindAsync(id);
        }
        public async Task<int> DeleteByIdAsync(int id)
        {
            _context.Remove(GetByIdAsync(id));
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.DomainClasses.Dashboard.Dashboard>> GetAllAsync()
        {
            return await _context.Dashboards.ToListAsync();
        }

        public async Task<IEnumerable<Domain.DomainClasses.Dashboard.Dashboard>> GetDashboardByProjectId(int projectId)
        {
            var dashboard = await _context.Dashboards.Where(proj => proj.ProjectId == projectId).ToListAsync();

            return dashboard;
        }

        public async Task<int> AddAsync(Domain.DomainClasses.Dashboard.Dashboard item)
        {
            await _context.Dashboards.AddAsync(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> PutAsync(int id, Domain.DomainClasses.Dashboard.Dashboard item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
