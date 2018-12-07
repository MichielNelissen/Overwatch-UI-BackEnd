using System;
using System.Collections.Generic;
using System.Text;
using OverwatchAPI.Data.Context;

namespace OverwatchAPI.Data.Repository.Dashboard
{
  public  class DashboardRepository: IGenericRepository<Domain.DomainClasses.Dashboard.Dashboard>
    {
        private readonly OverwatchContext _context;

        public DashboardRepository(OverwatchContext context)
        {
            _context = context;
        }

        public Domain.DomainClasses.Dashboard.Dashboard GetById(int id)
        {
            return _context.Find<Domain.DomainClasses.Dashboard.Dashboard>(id);
        }

        public void Put(int id, Domain.DomainClasses.Dashboard.Dashboard item)
        {
            var itemToUpdate = _context.Find<Domain.DomainClasses.Dashboard.Dashboard>(id);
            _context.Update(itemToUpdate);

            _context.SaveChanges();
        }

        public void Add(Domain.DomainClasses.Dashboard.Dashboard item)
        {
            _context.Add(item);
            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var itemToDelete = _context.Find<Domain.DomainClasses.Dashboard.Dashboard>(id);
            _context.Remove(itemToDelete);

            _context.SaveChanges();
        }
    }
}
