using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BusinessLogic.Helper;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.OpenApi.Models;

namespace Back_WorkoutTracket
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddTransient<Seed>();

			builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

			builder.Services.AddScoped<IExerciseTypeService, ExerciseTypeService>();
			builder.Services.AddScoped<IWorkoutTemplateService, WorkoutTemplateService>();
			builder.Services.AddScoped<IWorkoutService, WorkoutService>();
			builder.Services.AddScoped<IWorkoutExerciseService, WorkoutExerciseService>();
			builder.Services.AddScoped<ISetService, SetService>();
			builder.Services.AddScoped<IStatsService,  StatsService>();	

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "Enter JWT token in format: Bearer {token}",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
                });

				options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header
						},
						new List<string>()
					}
				});

            });

			builder.Services.AddDbContext<DataContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddIdentity<User, IdentityRole>()
					.AddEntityFrameworkStores<DataContext>()
					.AddDefaultTokenProviders();

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["Jwt:Issuer"],
					ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
			});

			var app = builder.Build();

			if (args.Length == 1 && args[0].ToLower() == "seeddata")
				SeedData(app);

            async Task SeedData(IHost app)
            {
                var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
                using (var scope = scopedFactory.CreateScope())
                {
                    // Отримуємо всі необхідні сервіси
                    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>(); // <-- Отримуємо RoleManager

                    // Передаємо всі залежності в конструктор Seed
                    var seeder = new Seed(context, userManager, roleManager);
                    await seeder.SeedDataContextAsync();
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
