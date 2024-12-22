using Microsoft.AspNetCore.Identity;
using ShopFusion.Server.Services.Interfaces;
using ShopFusion.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using ShopFusion.Common;

namespace ShopFusion.Server.Services
{
	public class DbInitializer : IDbInitializer
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ApplicationDbContext _dbContext;

		public DbInitializer(UserManager<IdentityUser> userManager,
			RoleManager<IdentityRole> roleManager,
			ApplicationDbContext dbContext)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_dbContext = dbContext;
		}

		public void Initialize()
		{
			if(_dbContext.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Count() > 0)
			{
				_dbContext.Database.Migrate();
			}

			if (!_roleManager.RoleExistsAsync(CommonConfiguration.Role_Admin).GetAwaiter().GetResult())
			{
				_roleManager.CreateAsync(new IdentityRole(CommonConfiguration.Role_Admin)).GetAwaiter().GetResult();
			}
			if (!_roleManager.RoleExistsAsync(CommonConfiguration.Role_Customer).GetAwaiter().GetResult())
			{
				_roleManager.CreateAsync(new IdentityRole(CommonConfiguration.Role_Customer)).GetAwaiter().GetResult();
			}
		}
	}
}