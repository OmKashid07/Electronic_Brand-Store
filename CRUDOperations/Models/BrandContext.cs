using Microsoft.EntityFrameworkCore;

namespace CRUDOperations.Models
{
    public class BrandContext : DbContext
    {
        public BrandContext(DbContextOptions<BrandContext>options) : base(options)
        {
            
        }
        public DbSet<Brand> Brands { get; set; }

        //when we want to add new table need to add new db set 
       // public DbSet<Brand> Brand2 { get; set; }
    }
}
