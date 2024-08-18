using Business.MediatR.Queries;
using Business.Service;
using Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MediatR.Handlers
{
    public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, List<Employee>>
    {
        private readonly IEmployeeService _employeeService;
        public GetAllEmployeeQueryHandler(IEmployeeService employeeService)
        {
                _employeeService= employeeService;
        }

        public async Task<List<Employee>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
           return await _employeeService.GetAll();
        }
    }
}
