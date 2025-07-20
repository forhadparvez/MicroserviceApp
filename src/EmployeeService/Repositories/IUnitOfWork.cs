using System;

namespace EmployeeService.Repositories;

public interface IUnitOfWork : IDisposable
{
    IEmployeeRepository Employees { get; }
    Task<int> SaveAsync();
}
