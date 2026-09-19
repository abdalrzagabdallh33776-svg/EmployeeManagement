using System.Data.Entity;
using EmployeeManagement.Models;

namespace EmployeeManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection") { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Property(x => x.Salary).HasPrecision(18, 2);
            modelBuilder.Entity<Employee>().HasRequired(x => x.Department).WithMany(x => x.Employees)
                .HasForeignKey(x => x.DepartmentId).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().Property(x => x.UserName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.Password).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(x => x.Role).IsRequired().HasMaxLength(20);
        }
    }

    public class DatabaseInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var admin = new User { UserName = "admin", Password = "admin123", FullName = "مدير النظام", Role = "مدير", IsActive = true };
            var employee = new User { UserName = "employee", Password = "employee123", FullName = "موظف تجريبي", Role = "موظف", IsActive = true };
            context.Users.Add(admin);
            context.Users.Add(employee);
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
