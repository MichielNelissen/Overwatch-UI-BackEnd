using System.Threading.Tasks;

namespace OverwatchAPI.Data.Repository.Dashboard
{
    public interface IDashboardRepository : IGenericRepository<Domain.DomainClasses.Dashboard.Dashboard>
    {
       Task<Domain.DomainClasses.Dashboard.Dashboard> GetDashboardByProjectId(int projectId);
    }
}