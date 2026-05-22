using EmployeeManagement.Application.Employees.Dtos;
using EmployeeManagement.Application.Employees.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IReadOnlyCollection<EmployeeResponseDto>>> GetAll(
            CancellationToken cancellationToken)
        {
            var employees = await _employeeService.GetAllAsync(cancellationToken);

            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<EmployeeResponseDto>> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetByIdAsync(id, cancellationToken);

            if (employee is null)
                return NotFound(new { message = $"Employee with id {id} was not found." });

            return Ok(employee);
        }

        [HttpGet("by-department/{departmentId:int}/with-projects")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IReadOnlyCollection<EmployeeResponseDto>>> GetByDepartmentWithProjects(
            int departmentId,
            CancellationToken cancellationToken)
        {
            var employees = await _employeeService.GetByDepartmentWithProjectsAsync(
                departmentId,
                cancellationToken);

            return Ok(employees);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EmployeeResponseDto>> Create(
            [FromBody] CreateEmployeeRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeService.CreateAsync(request, cancellationToken);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = employee.Id },
                    employee);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EmployeeResponseDto>> Update(
            int id,
            [FromBody] UpdateEmployeeRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeService.UpdateAsync(id, request, cancellationToken);

                if (employee is null)
                    return NotFound(new { message = $"Employee with id {id} was not found." });

                return Ok(employee);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            var deleted = await _employeeService.DeleteAsync(id, cancellationToken);

            if (!deleted)
                return NotFound(new { message = $"Employee with id {id} was not found." });

            return NoContent();
        }
    }
}