using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service
{
    public interface IEmployeeService
    {
        Task Add(Employee employee);
        Task Delete(Employee employee);
        Task Update(Employee employee);
        Task<Employee> GetById(int id);
        Task<List<Employee>> GetAll();


    }
}
