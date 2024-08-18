using Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MediatR.Commands
{
    public class CreateEmployeeCommand : IRequest<Unit>
    { 
        public Employee Employee { get; set; }
    }
}
