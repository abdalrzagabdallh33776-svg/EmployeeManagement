using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required, StringLength(50)] public string UserName { get; set; }
        [Required, StringLength(100)] public string Password { get; set; }
        [StringLength(100)] public string FullName { get; set; }
        [Required, StringLength(20)] public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}
