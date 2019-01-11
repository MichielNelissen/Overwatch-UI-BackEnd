using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OverwatchAPI.Data.Repository
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<int> PutAsync(int id, T item);
        Task<int> AddAsync(T item);
        Task<int> DeleteByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
