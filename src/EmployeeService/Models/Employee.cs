using System;

namespace EmployeeService.Models;

public class Employee
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string Designation { get; set; } = default!;
    public string Department { get; set; } = default!;
    public DateTime JoiningDate { get; set; }

}
