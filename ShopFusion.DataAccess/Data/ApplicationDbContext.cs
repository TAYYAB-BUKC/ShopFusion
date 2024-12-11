using Microsoft.EntityFrameworkCore;
using ShopFusion.Models.Entities;

namespace ShopFusion.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductPrices> ProductPrices { get; set; }
	}
}
