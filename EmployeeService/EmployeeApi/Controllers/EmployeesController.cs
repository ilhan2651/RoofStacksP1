using Azure;
using Business.Service;
using Core;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Xml.XPath;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        IEmployeeService _employeeService;
         public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [Authorize(Policy ="ReadEmployee")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll() 
        {
           var employee= await _employeeService.GetAll();
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
            var employee = await _employeeService.GetById(id);
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
              await _employeeService.Add(employee);   
                return Ok(new { message = "Operation Succesed" });
            }
            return BadRequest(new { message = "Operation Failed" });
        }
        [Authorize (Policy ="AllEmployee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id )
        {
            var emp = await _employeeService.GetById(id);
            if (emp != null)
            {
                await _employeeService.Delete(emp);
                return Ok(new { message = "Operation Succesed" });
            }
            return BadRequest(new { message = "Operation Failed" });
        }
        [HttpPut("{id}")]
        [Authorize(Policy ="AllEmployee")]
        public async Task<IActionResult> Update( [FromRoute] int id,[FromBody] Employee employee)
        {
            var validEmployee=await _employeeService.GetById(id);
            if (validEmployee==null)
            {
                return BadRequest(new { message = "Operation Failed" });
            }
            await _employeeService.Update(validEmployee);
            return Ok(new { message = "Operation Succesed" });
        }

        [Authorize(Policy = "AllEmployee")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id,JsonPatchDocument<Employee> patchDoc)
        {
            if (patchDoc==null)
            {
                return BadRequest();
            }

            var validEmp= await _employeeService.GetById(id);
            if (validEmp == null)
            {
                return NotFound(new { message = "Not Found " });
            }
            patchDoc.ApplyTo(validEmp);
            await _employeeService.Update(validEmp);
            return Ok(new { message = "Operation Succesed" });
        }
    }
}
