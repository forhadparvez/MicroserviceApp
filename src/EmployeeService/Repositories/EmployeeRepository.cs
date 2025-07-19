using Microsoft.EntityFrameworkCore;
using EmployeeService.Models;
using EmployeeService.Data;

namespace EmployeeService.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly EmployeeDbContext _context;
    public EmployeeRepository(EmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync() => await _context.Employees.ToListAsync();

    public async Task<Employee?> GetByIdAsync(int id) => await _context.Employees.FindAsync(id);

    public async Task<Employee> CreateAsync(Employee emp)
    {
        _context.Employees.Add(emp);
        await _context.SaveChangesAsync();
        return emp;
    }

    public async Task UpdateAsync(Employee emp)
    {
        _context.Entry(emp).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var emp = await _context.Employees.FindAsync(id);
        if (emp is not null)
        {
            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
        }
    }
}
