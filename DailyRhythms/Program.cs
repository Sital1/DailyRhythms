
using DailyRhythms.Models;
using DailyRhythms.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace DailyRhythms
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();

			builder.Services.AddDbContext<DailyRhythmsContext>(options =>
			{
				options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			builder.Services.AddScoped<IAuthService, AuthService>();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();

			builder.Services.AddCors(opt =>
			{
				opt.AddPolicy("CorsPolicy", policy =>
				{
					policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(new string[] {"https://localhost:4200"});
				});
			});


			// JWT Auth
			builder.Services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				//opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				  .AddJwtBearer("Bearer",options =>
				  {
		

					  options.TokenValidationParameters = new TokenValidationParameters
					  {
						  ValidateIssuerSigningKey = true,
						  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
						  ValidIssuer = builder.Configuration["Token:Issuer"],
						  ValidateIssuer = true,
						  ValidateAudience = false
					  };
				  });
			builder.Services.AddAuthorization();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				var securitySchema = new OpenApiSecurityScheme
				{
					Description = "JWT Auth Bearer Scheme",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				};

				c.AddSecurityDefinition("Bearer", securitySchema);

				var securityRequirement = new OpenApiSecurityRequirement
				{
					{
						securitySchema, new[] {"Bearer"}
					}
				};

				c.AddSecurityRequirement(securityRequirement);

			});


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseStaticFiles();
			app.MapFallbackToController("Index","FallBack");

			app.UseCors("CorsPolicy");

			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();
			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var context = services.GetRequiredService<DailyRhythmsContext>();
			var logger = services.GetRequiredService<ILogger<Program>>();
			try
			{
				await context.Database.MigrateAsync();
				await SeedData.SeedAsync(context);
		
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occured during migration");
			}


			app.Run();
		}
	}
}
