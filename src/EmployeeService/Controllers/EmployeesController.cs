using Microsoft.AspNetCore.Mvc;
using EmployeeService.Models;
using EmployeeService.Repositories;
using Microsoft.AspNetCore.Authorization;
using EmployeeService.Services;

namespace EmployeeService.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        => Ok(await _employeeService.GetAllEmployeesAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> Get(int id)
    {
        var emp = await _employeeService.GetByIdAsync(id);
        return emp == null ? NotFound() : Ok(emp);
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> Create(Employee emp)
    {
        var created = await _employeeService.AddEmployeeAsync(emp);
        return CreatedAtAction(nameof(Get), new { id = created }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Employee emp)
    {
        if (id != emp.Id) return BadRequest();
        await _employeeService.UpdateAsync(emp);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _employeeService.RemoveAsync(id);
        return NoContent();
    }
}
