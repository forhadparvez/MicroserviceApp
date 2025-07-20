using Microsoft.EntityFrameworkCore;
using EmployeeService.Models;
using EmployeeService.Data;

namespace EmployeeService.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(DbContext context) : base(context)
    {
    }
}
