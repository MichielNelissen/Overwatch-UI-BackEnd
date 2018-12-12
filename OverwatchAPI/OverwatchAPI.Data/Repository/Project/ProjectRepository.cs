using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OverwatchAPI.Data.Context;

namespace OverwatchAPI.Data.Repository.Project
{
    public class ProjectRepository : IGenericRepository<Domain.DomainClasses.Projects.Project>
    {
        private readonly OverwatchContext _context;

        public ProjectRepository(OverwatchContext context)
        {
            _context = context;
        }

        public async Task<Domain.DomainClasses.Projects.Project> GetByIdAsync(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<int> DeleteByIdAsync(int id)
        {
            _context.Remove(GetByIdAsync(id));
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddAsync(Domain.DomainClasses.Projects.Project item)
        {
            await _context.Projects.AddAsync(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.DomainClasses.Projects.Project>> GetAllAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<int> PutAsync(int id, Domain.DomainClasses.Projects.Project item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
