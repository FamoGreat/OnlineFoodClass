using Microsoft.EntityFrameworkCore;
using OnlineFoodClass.Models;

namespace OnlineFoodClass.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }


        public DbSet<Course> Courses { get; set; }
    }
}
