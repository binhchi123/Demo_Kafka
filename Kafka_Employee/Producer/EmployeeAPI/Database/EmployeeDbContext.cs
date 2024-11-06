using EmployeeAPI.Appllication.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Database
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext()
        {
            
        }
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options ) : base( options ) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
