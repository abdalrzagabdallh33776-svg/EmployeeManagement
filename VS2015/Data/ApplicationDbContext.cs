using System;
using System.Data.Entity;
using EmployeeManagement.Models;

namespace EmployeeManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());
            Database.Initialize(false);

            if (Database.Exists() && !Users.Any())
            {
                Users.Add(new User { UserName = "admin", Password = "admin123", FullName = "مدير النظام" });
                SaveChanges();
            }
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Property(x => x.Salary).HasPrecision(18, 2);
            modelBuilder.Entity<Employee>().HasRequired(x => x.Department)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.DepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>().Property(x => x.UserName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.Password).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.FullName).HasMaxLength(100);
        }
    }
}
