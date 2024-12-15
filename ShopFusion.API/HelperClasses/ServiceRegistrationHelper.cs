using Microsoft.EntityFrameworkCore;
using ShopFusion.Business.Interfaces;
using ShopFusion.Business.Repositories;
using ShopFusion.DataAccess.Data;
using ShopFusion.Models.Mappers;

namespace ShopFusion.API.HelperClasses
{
	public class ServiceRegistrationHelper
	{
		public static void RegisterServices(WebApplicationBuilder builder)
		{
			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddAutoMapper(typeof(MappingProfile));

			builder.Services.AddScoped<IProductRepository, ProductRepository>();
		}
	}
}