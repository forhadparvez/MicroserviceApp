using AttendanceService.Data;
using AttendanceService.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceService.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly AttendanceDbContext _context;

    public AttendanceRepository(AttendanceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Attendance>> GetAllAsync()
    {
        return await _context.Attendances.ToListAsync();
    }

    public async Task<Attendance?> GetByIdAsync(int id)
    {
        return await _context.Attendances.FindAsync(id);
    }

    public async Task<Attendance> CreateAsync(Attendance attendance)
    {
        _context.Attendances.Add(attendance);
        await _context.SaveChangesAsync();
        return attendance;
    }

    public async Task UpdateAsync(Attendance attendance)
    {
        _context.Entry(attendance).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var attendance = await _context.Attendances.FindAsync(id);
        if (attendance != null)
        {
            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();
        }
    }

}
