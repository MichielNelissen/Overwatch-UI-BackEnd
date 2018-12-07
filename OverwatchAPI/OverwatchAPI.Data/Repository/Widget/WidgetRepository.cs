using System;
using System.Collections.Generic;
using System.Text;
using OverwatchAPI.Data.Context;
using OverwatchAPI.Domain.DomainClasses.Widgets;

namespace OverwatchAPI.Data.Repository.Widget
{
    public class WidgetRepository : IGenericRepository<Domain.DomainClasses.Widgets.Widget>
    {
        private readonly OverwatchContext _context;

        public WidgetRepository(OverwatchContext context)
        {
            _context = context;
        }

        public void Add(Domain.DomainClasses.Widgets.Widget item)
        {
            _context.Add(item);
            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var widgetToDelete = _context.Find<Domain.DomainClasses.Widgets.Widget>(id);
            _context.Remove(widgetToDelete);
        }

        public Domain.DomainClasses.Widgets.Widget GetById(int id)
        {
           return _context.Find<Domain.DomainClasses.Widgets.Widget>(id);
        }

        public void Put(int id, Domain.DomainClasses.Widgets.Widget item)
        {
            var widgetToUpdate  =_context.Find<Domain.DomainClasses.Widgets.Widget>(id);
            _context.Update(widgetToUpdate);

            _context.SaveChanges();
        }
    }
}
