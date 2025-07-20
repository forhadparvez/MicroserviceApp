using System;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    public IEmployeeRepository Employees { get; private set; }

    public UnitOfWork(DbContext context)
    {
        _context = context;
        Employees = new EmployeeRepository(context);
    }

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
