using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().Property(e => e.Salary).HasPrecision(18, 2);
        modelBuilder.Entity<Department>().HasMany(d => d.Employees).WithOne(e => e.Department)
            .HasForeignKey(e => e.DepartmentId).OnDelete(DeleteBehavior.Restrict);
    }
}

public static class DbSeeder
{
    public static void Seed(ApplicationDbContext db)
    {
        if (db.Departments.Any()) return;
        var departments = new[]
        {
            new Department { Name = "الإدارة", Description = "إدارة الشركة" },
            new Department { Name = "المبيعات", Description = "قسم المبيعات" },
            new Department { Name = "المالية", Description = "قسم المالية" },
            new Department { Name = "التقنية", Description = "قسم التقنية" }
        };
        db.Departments.AddRange(departments);
        db.SaveChanges();
        db.Employees.AddRange(
            new Employee { FullName = "أحمد محمد", Email = "ahmed@example.com", Phone = "0500000001", Position = "مدير عام", Salary = 15000, HireDate = new DateTime(2020, 1, 10), Status = "نشط", DepartmentId = departments[0].Id },
            new Employee { FullName = "سارة علي", Email = "sara@example.com", Phone = "0500000002", Position = "مبيعات", Salary = 7000, HireDate = new DateTime(2022, 5, 12), Status = "نشط", DepartmentId = departments[1].Id },
            new Employee { FullName = "خالد سالم", Email = "khalid@example.com", Phone = "0500000003", Position = "محاسب", Salary = 6500, HireDate = new DateTime(2021, 7, 3), Status = "في إجازة", DepartmentId = departments[2].Id });
        db.SaveChanges();
    }
}
