using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models;

public class Employee
{
    public int Id { get; set; }
    [Required, StringLength(150)] public string FullName { get; set; } = "";
    [Required, EmailAddress, StringLength(150)] public string Email { get; set; } = "";
    [Required, StringLength(50)] public string Phone { get; set; } = "";
    [Required, StringLength(100)] public string Position { get; set; } = "";
    [Range(0, 100000000)] public decimal Salary { get; set; }
    [DataType(DataType.Date)] public DateTime HireDate { get; set; } = DateTime.Today;
    [Required] public string Status { get; set; } = "نشط";
    [Required] public int DepartmentId { get; set; }
    public Department? Department { get; set; }
}
