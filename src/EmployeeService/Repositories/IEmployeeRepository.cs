using EmployeeService.Models;

namespace EmployeeService.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<Employee> CreateAsync(Employee emp);
    Task UpdateAsync(Employee emp);
    Task DeleteAsync(int id);
}
