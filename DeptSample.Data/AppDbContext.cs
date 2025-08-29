
using Microsoft.EntityFrameworkCore;
using DeptSample.Data.Models;

namespace DeptSample.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
    }
}
