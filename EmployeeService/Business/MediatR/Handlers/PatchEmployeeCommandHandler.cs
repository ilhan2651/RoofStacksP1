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
    public class PatchEmployeeCommandHandler : IRequestHandler<PatchEmployeeCommand, Unit>
    {
        private readonly IEmployeeService _employeeService;

        public PatchEmployeeCommandHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<Unit> Handle(PatchEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetById(request.Id);
            if (employee == null)
            {
                throw new Exception("Employee not found");
            }

            request.PatchDoc.ApplyTo(employee);

            await _employeeService.Update(employee);

            return Unit.Value;
        }
    }
}