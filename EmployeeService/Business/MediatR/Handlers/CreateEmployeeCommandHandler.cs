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
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Unit>
    {
        IEmployeeService _employeeService;
        public CreateEmployeeCommandHandler(IEmployeeService employeService)
        {
                _employeeService= employeService;
        }
        public async Task<Unit> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeService.Add(request.Employee);
            return Unit.Value;
        }
    }
}
