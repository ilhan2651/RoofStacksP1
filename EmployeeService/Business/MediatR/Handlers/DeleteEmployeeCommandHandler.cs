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
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand,bool>
    {
        IEmployeeService _employeeService;
        public DeleteEmployeeCommandHandler(IEmployeeService employeeService)
        {
                _employeeService= employeeService;
        }
        public async Task<bool> Handle(DeleteEmployeeCommand request,CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetById(request.Id);
            if (employee != null)
            {
                await _employeeService.Delete(employee);
                return true;
            }
            return false;
        }

    }
}
