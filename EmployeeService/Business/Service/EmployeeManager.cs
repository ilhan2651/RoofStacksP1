using Data_Access;
using Entities;
using FluentValidation;
using Core.Aspect.Autofac.Caching;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Caching;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;

namespace Business.Service
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeDal _employeeDal;
        ICacheManager _cacheManager;
        public EmployeeManager(IEmployeeDal employeeDal ,ICacheManager cacheManager)
        {
            _employeeDal = employeeDal;
           
            _cacheManager = cacheManager;
        }
        public async Task Add(Employee employee)
        {
           

            var cacheKeyById = $"employee_{employee.Id}";
            var allEmployeesCacheKey = "all_employees";
            await _employeeDal.Add(employee);
            await _cacheManager.Add(cacheKeyById, employee, 10);

            
            var updatedEmployees = await _employeeDal.GetAll();
            await _cacheManager.Remove(allEmployeesCacheKey);
        }

        public async Task Delete(Employee employee)
        {
            var cacheKeyById = $"employee_{employee.Id}";
            var allEmployeesCacheKey = "all_employees";
            await _employeeDal.Delete(employee);
            if (await _cacheManager.IsAdd(cacheKeyById))
            {
                await _cacheManager.Remove(cacheKeyById);
            }
            var updatedEmployees = await _employeeDal.GetAll();
            await _cacheManager.Remove(allEmployeesCacheKey);

        }
     
        public async Task<List<Employee>> GetAll()
        {
            const string cacheKey = "all_employees";
            if ( await _cacheManager.IsAdd(cacheKey))
            {
                return await _cacheManager.GetList<Employee>(cacheKey);
            }

           var employees =  await _employeeDal.GetAll();
            await _cacheManager.Add(cacheKey, employees, 10);
            return employees;
        }

        public async Task<Employee> GetById(int id)
        {
            var cacheKey = $"employee_{id}";
            if (await _cacheManager.IsAdd(cacheKey))
            {
                return await _cacheManager.Get(cacheKey);
            }

            return await _employeeDal.Get(e => e.Id == id);
        }

        public async Task Update(Employee employee)
        {  
            var allEmployeesCacheKey = "all_employees";    
            var cacheKeyById = $"employee_{employee.Id}";
            await _employeeDal.Update(employee);

            if (await _cacheManager.IsAdd(cacheKeyById))
            {
                await _cacheManager.Remove(cacheKeyById);
            }
            await _cacheManager.Add(cacheKeyById, employee,10);

            var updatedEmployees = await _employeeDal.GetAll();
            await _cacheManager.Remove(allEmployeesCacheKey);
 
    
        }
    }
}
