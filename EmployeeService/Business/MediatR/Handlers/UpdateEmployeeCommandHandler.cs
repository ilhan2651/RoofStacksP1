using Business.MediatR.Commands;
using Business.Service;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MediatR.Handlers
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand,Unit>
    {
        IEmployeeService _employeeService;
        public UpdateEmployeeCommandHandler(IEmployeeService employeeService)
        {
            _employeeService= employeeService;
        }
        public async Task<Unit> Handle(UpdateEmployeeCommand request,CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetById(request.Id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with Id {request.Id} not found.");
            }
            employee.FirstName=request.FirstName;
            employee.LastName=request.LastName;
            employee.Email=request.Email;
            employee.Age=request.Age;
            employee.Id=request.Id ;
            employee.PhoneNumber=request.PhoneNumber;
            employee.Gender=request.Gender;
            await _employeeService.Update(employee);    
            return Unit.Value;
        }
    }
}
