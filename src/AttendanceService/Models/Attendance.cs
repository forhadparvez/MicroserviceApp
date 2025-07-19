namespace AttendanceService.Models;

public class Attendance
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public string? Status { get; set; } // e.g., Present, Absent, Late
}
