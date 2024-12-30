using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShopFusion.Business.Interfaces;
using ShopFusion.Business.Repositories;
using ShopFusion.DataAccess.Data;
using ShopFusion.Models.Entities;
using ShopFusion.Models.Mappers;
using Stripe;
using System.Text;
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
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopFusion_Api", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please Bearer and then token in the field",
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement {
				   {
					 new OpenApiSecurityScheme
					 {
					   Reference = new OpenApiReference
					   {
						 Type = ReferenceType.SecurityScheme,
						 Id = "Bearer"
					   }
					  },
					  new string[] { }
					}
				});
			});
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

			var apiSettings = builder.Configuration.GetSection("APISettings");
			builder.Services.Configure<Configuration>(apiSettings);

			var _apiSettings = apiSettings.Get<Configuration>();

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(bearer => 
			{
				bearer.RequireHttpsMetadata = false;
				bearer.SaveToken = true;
				bearer.TokenValidationParameters = new()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey)),
					ValidAudience = _apiSettings.ValidAudience,
					ValidIssuer = _apiSettings.ValidIssuer,
					ClockSkew = TimeSpan.Zero
				};
			});

			StripeConfiguration.ApiKey = builder.Configuration.GetSection("StripeSettings")["Secretkey"];

			builder.Services.AddScoped<IEmailSender, EmailHelper>();
		}
	}
}