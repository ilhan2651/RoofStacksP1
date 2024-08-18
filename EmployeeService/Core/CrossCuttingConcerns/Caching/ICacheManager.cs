using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        Task<Employee> Get(string key);
        Task Add(string key, object value);
        Task<bool> IsAdd(string key);
        Task Remove(string key);
        Task<List<Employee>> GetList(string key);

    }
}