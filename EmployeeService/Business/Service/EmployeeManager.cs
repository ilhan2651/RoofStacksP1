using Data_Access;
using Entities;
using FluentValidation;
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
 
            var allEmployeesCacheKey = "all_employees";
            await _employeeDal.Add(employee);
            var list =await _cacheManager.GetList(allEmployeesCacheKey);
            if (list == null)
            {
                list = new List<Employee>();
            }
            list.Add(employee);
            await _cacheManager.Add(allEmployeesCacheKey, list);

           
        }

        public async Task Delete(Employee employee)
        {
            var allEmployeesCacheKey = "all_employees";
            await _employeeDal.Delete(employee);
            var list = await _cacheManager.GetList(allEmployeesCacheKey);
            if (list != null)
            {
                list.RemoveAll(e => e.Id == employee.Id);
                await _cacheManager.Add(allEmployeesCacheKey, list);
            }





        }
     
        public async Task<List<Employee>> GetAll()
        {
            const string cacheKey = "all_employees";

            var cachedEmployees = await _cacheManager.GetList(cacheKey);

            if (cachedEmployees == null)
            {
                var employeesFromDb = await _employeeDal.GetAll();
                if (employeesFromDb != null)
                {
                    await _cacheManager.Add(cacheKey, employeesFromDb);
                    return employeesFromDb;
                }
            }
            else
            {
                var countInCache = cachedEmployees.Count;
                var countInDb = await _employeeDal.Count(); 

                if (countInCache != countInDb)
                {
                  
                    var employeesFromDb = await _employeeDal.GetAll();
                    if (employeesFromDb != null)
                    {
                        await _cacheManager.Add(cacheKey, employeesFromDb);
                        return employeesFromDb;
                    }
                }
                else
                {
                    return cachedEmployees;
                }
            }

            return new List<Employee>(); 
        }

        public async Task<Employee> GetById(int id)
        {
            const string allEmployeesCacheKey = "all_employees";
            var list = await _cacheManager.GetList(allEmployeesCacheKey);
            if (list!=null)
            {
                var employee = list.FirstOrDefault(item => item.Id == id);
                if (employee != null)
                {
                    return employee;
                }
            }
            var employeeFromDb = await _employeeDal.Get(e => e.Id == id);

            if (employeeFromDb != null)
            {
                if (list == null)
                {
                    list = new List<Employee>();
                }
                list.Add(employeeFromDb);
                await _cacheManager.Add(allEmployeesCacheKey, list);
            }
            return employeeFromDb;


        }

        public async Task Update(Employee employee)
        {

          var allEmployeesCacheKey = "all_employees";    
          await _employeeDal.Update(employee);
          var list = await _cacheManager.GetList(allEmployeesCacheKey);
            if (list != null)
            {
                var index = list.FindIndex(e => e.Id == employee.Id);
                if (index != -1)
                {
                    list[index] = employee;
                }
                else
                {
                    list.Add(employee);
                }
                await _cacheManager.Add(allEmployeesCacheKey, list);
            }
    
        }
    }
}
