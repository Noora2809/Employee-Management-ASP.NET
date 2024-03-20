using ASP.NET_CRUD_Operation_Project.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_CRUD_Operation_Project.Data
{
    public class EmployeeManagementDbContext : DbContext
    {
        public EmployeeManagementDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
