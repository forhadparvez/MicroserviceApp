using System;
using EmployeeService.Models;
using EmployeeService.Repositories;

namespace EmployeeService.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        => await _unitOfWork.Employees.GetAllAsync();

    public async Task<int> AddEmployeeAsync(Employee employee)
    {
        await _unitOfWork.Employees.AddAsync(employee);
        await _unitOfWork.SaveAsync();
        return employee.Id;
    }

    public async Task<Employee> GetByIdAsync(long id)
    {
        return await _unitOfWork.Employees.GetByIdAsync(id);
    }

    public async Task<int> UpdateAsync(Employee employee)
    {
        _unitOfWork.Employees.Update(employee);
        return await _unitOfWork.SaveAsync();
    }

    public async Task<int> RemoveAsync(long id)
    {
        await _unitOfWork.Employees.SoftRemove(id);
        return await _unitOfWork.SaveAsync();
    }
}
