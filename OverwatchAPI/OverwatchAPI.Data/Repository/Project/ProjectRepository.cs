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


        public Domain.DomainClasses.Projects.Project GetById(int id)
        {
            return _context.Find<Domain.DomainClasses.Projects.Project>(id);
        }

        public void Put(int id, Domain.DomainClasses.Projects.Project item)
        {
            _context.Update(item);
        }

        public void Add(Domain.DomainClasses.Projects.Project item)
        {
            _context.Add(item);
            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var itemToDelete = _context.Find<Domain.DomainClasses.Projects.Project>(id);
            _context.Remove(itemToDelete);

            _context.SaveChanges();
        }
    }
}
