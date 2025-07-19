using Microsoft.AspNetCore.Mvc;
using EmployeeService.Models;
using EmployeeService.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeService.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _repo;

    public EmployeesController(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        => Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> Get(int id)
    {
        var emp = await _repo.GetByIdAsync(id);
        return emp == null ? NotFound() : Ok(emp);
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> Create(Employee emp)
    {
        var created = await _repo.CreateAsync(emp);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Employee emp)
    {
        if (id != emp.Id) return BadRequest();
        await _repo.UpdateAsync(emp);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}
