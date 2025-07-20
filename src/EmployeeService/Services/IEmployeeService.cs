using System;
using EmployeeService.Models;

namespace EmployeeService.Services;

public interface IEmployeeService
{
    Task<Employee> GetByIdAsync(long id);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<int> AddEmployeeAsync(Employee employee);
    Task<int> UpdateAsync(Employee employee);

    Task<int> RemoveAsync(long id);

}
