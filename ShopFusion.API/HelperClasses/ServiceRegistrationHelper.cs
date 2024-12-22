using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopFusion.Business.Interfaces;
using ShopFusion.Business.Repositories;
using ShopFusion.DataAccess.Data;
using ShopFusion.Models.Entities;
using ShopFusion.Models.Mappers;
using System.Text.Json.Serialization;

namespace ShopFusion.API.HelperClasses
{
	public class ServiceRegistrationHelper
	{
		public static void RegisterServices(WebApplicationBuilder builder)
		{
			// Add services to the container.

			builder.Services.AddControllers().AddJsonOptions(options =>
				options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
			);
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddCors(c => c.AddPolicy("AllPolicy", options =>
			{
				options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
			}));

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddDefaultTokenProviders()
				.AddEntityFrameworkStores<ApplicationDbContext>();
			builder.Services.AddAutoMapper(typeof(MappingProfile));

			builder.Services.AddScoped<IProductRepository, ProductRepository>();
			builder.Services.AddScoped<IOrderRepository, OrderRepository>();
		}
	}
}