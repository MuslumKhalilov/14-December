using _14_December.Entities;
using Microsoft.EntityFrameworkCore;

namespace _14_December.DAL
{
	public class AppDbContext:DbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }
        public DbSet<Category> Categories { get; set; }
    }
}
