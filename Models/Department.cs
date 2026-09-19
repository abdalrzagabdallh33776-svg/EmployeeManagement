using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models;

public class Department
{
    public int Id { get; set; }
    [Required, StringLength(100)] public string Name { get; set; } = "";
    [StringLength(255)] public string? Description { get; set; }
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
