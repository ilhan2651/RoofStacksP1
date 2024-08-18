using Azure;
using Business.MediatR.Commands;
using Business.MediatR.Queries;
using Business.Service;
using Core;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Xml.XPath;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        IEmployeeService _employeeService;
        private readonly IMediator _mediator;
         public EmployeesController(IEmployeeService employeeService,IMediator mediator)
        {
            _employeeService = employeeService;
            _mediator = mediator;   
        }
        [Authorize(Policy ="ReadEmployee")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll() 
        {
            var query = new GetAllEmployeeQuery();
           var employee= await _mediator.Send(query);
            if (employee.Any())
            {
                  return Ok(employee);
            }
          return NoContent();
        }
        [Authorize(Policy ="ReadEmployee")]
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetEmployeeQuery { Id = id};
            var employee = await _mediator.Send(query);
            if (employee==null)
            {
                return BadRequest(new { message = "Operation Failed" });
            }
            return Ok(employee);
        }


        [Authorize(Policy ="WriteEmployee")]
        [HttpPost]
        public async Task<IActionResult> Add(Employee employee)
        {
            
            if (employee!=null)
            {
                var command = new CreateEmployeeCommand { Employee = employee };

                await _mediator.Send(command);
                return Ok(new { message = "Operation Succesed" });
            }
            return BadRequest(new { message = "Operation Failed" });
        }
        [Authorize (Policy ="AllEmployee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id )
        {
            var command = new DeleteEmployeeCommand { Id = id };
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok(new { message = "Operation Succeeded" });
            }
            return BadRequest(new { message = "Operation Failed" });
        }
        [HttpPut("{id}")]
        [Authorize(Policy ="AllEmployee")]
        public async Task<IActionResult> Update( [FromRoute] int id,[FromBody]  UpdateEmployeeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(new { message = "Invalid request" });
            }

            await _mediator.Send(command);
            return Ok(new { message = "Operation Succeeded" });
        }

        [Authorize(Policy = "AllEmployee")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id,JsonPatchDocument<Employee> patchDoc)
        {
            if (patchDoc==null)
            {
                return BadRequest();
            }
            var command = new PatchEmployeeCommand(id, patchDoc);

            try
            {
                await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }

            return Ok(new { message = "Operation succeeded" });
        }
    }
}
