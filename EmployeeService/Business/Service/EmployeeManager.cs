using Data_Access;
using Entities;
using FluentValidation;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IValidator<Employee> _employeeValidator;
        private readonly IEmployeeDal _employeeDal;
        public EmployeeManager(IEmployeeDal employeeDal, IValidator<Employee> employeeValidator)
        {
            _employeeDal = employeeDal;
            _employeeValidator = employeeValidator;
        }
        public async Task Add(Employee employee)
        {
            var validationResult = await _employeeValidator.ValidateAsync(employee);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException("Validation Failed: " + errorMessages);
            }
            await _employeeDal.Add(employee);
            
        }

        public async Task Delete(Employee employee)
        {
            await _employeeDal.Delete(employee);
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _employeeDal.GetAll();
        }

        public async Task<Employee> GetById(int id)
        {
            return await _employeeDal.Get(e => e.Id == id);
        }

        public async Task Update(Employee employee)
        {
            await _employeeDal.Update(employee);
        }
    }
}
