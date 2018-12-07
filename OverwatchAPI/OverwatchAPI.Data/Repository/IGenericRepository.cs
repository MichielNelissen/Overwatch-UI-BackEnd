using System;
using System.Collections.Generic;
using System.Text;

namespace OverwatchAPI.Data.Repository
{
    public interface IGenericRepository<T>
    {
        T GetById(int id);
        void Put(int id, T item);
        void Add(T item);
        void DeleteById(int id);
    }
}
