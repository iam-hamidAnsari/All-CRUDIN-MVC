using All_CRUDIN_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace All_CRUDIN_MVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Emp> emp { get; set; }
    }

    
}
