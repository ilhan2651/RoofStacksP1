using Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MediatR.Queries
{
    public class GetEmployeeQuery : IRequest<Employee>
    {
        public int Id { get; set; }
    }
}
