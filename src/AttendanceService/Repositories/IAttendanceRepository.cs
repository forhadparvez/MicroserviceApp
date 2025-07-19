using System;
using AttendanceService.Models;

namespace AttendanceService.Repositories;

public interface IAttendanceRepository
{
    Task<IEnumerable<Attendance>> GetAllAsync();
    Task<Attendance?> GetByIdAsync(int id);
    Task<Attendance> CreateAsync(Attendance attendance);
    Task UpdateAsync(Attendance attendance);
    Task DeleteAsync(int id);
}