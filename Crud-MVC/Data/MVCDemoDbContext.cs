using Crud_MVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Crud_MVC.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
