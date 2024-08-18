using Entities;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MediatR.Commands
{
    public class PatchEmployeeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public JsonPatchDocument<Employee> PatchDoc { get; set; }
    
    public PatchEmployeeCommand(int id, JsonPatchDocument<Employee> patchDoc)
    {
        Id = id;
        PatchDoc = patchDoc;
    }
    }
}
