using Microsoft.EntityFrameworkCore;
using ShopFusion.DataAccess.Data;
using ShopFusion.Server.Data;

namespace ShopFusion.Server.HelperClasses
{
	public class ServiceRegistrationHelper
	{
		public static void RegisterServices(WebApplicationBuilder builder)
		{
			// Add services to the container.
			builder.Services.AddRazorPages();
			builder.Services.AddServerSideBlazor();
			builder.Services.AddSingleton<WeatherForecastService>();
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

		}
	}
}
