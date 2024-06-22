using DataAccess;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore.SqlServer;
using PriceComparing.UnitOfWork;
using PriceComparing.AutoMigration;
using DataAccess.Models;
using PriceComparing.Repository;
using PriceComparing.Services;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Hangfire;
using Hangfire.SqlServer;

namespace PriceComparing
{
	public class Program
	{
		public static void Main(string[] args)
		{
			string corsTxt = "";

			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();

			//this code below is for the json loop reference and will fix the edit in some controllers but it change the json format
			//builder.Services.AddControllers()
			//	.AddJsonOptions(options =>
			//	{
			//		options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
			//	});








			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Add HttpClient 
            builder.Services.AddHttpClient(
                "ScrapingClient",
                client => client.BaseAddress = new Uri(builder.Configuration["ScrapingService"])
            
                );
            

            // Inject ScrappingService
            builder.Services.AddScoped<ScrapingService>();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			//builder.Services.AddEndpointsApiExplorer();
			//builder.Services.AddSwaggerGen();


			builder.Services.AddDbContext<DatabaseContext>(o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			// Add services to the container.
			builder.Services.AddScoped<GenericRepository<Category>>();
			builder.Services.AddScoped<GenericRepository<Brand>>();

            // Register AuthService
            builder.Services.AddScoped<IAuthServices, AuthService>();
			builder.Services.AddScoped<UserRepoNonGenric>();
            // Configure JWT settings
            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

            builder.Services.AddIdentity<AuthUser, IdentityRole>(options =>
				options.Password = new PasswordOptions
				{
					RequireDigit = true,
					RequiredLength = 12,
					RequireLowercase = true,
					RequireUppercase = true,
					RequireNonAlphanumeric = true
				})
               .AddEntityFrameworkStores<DatabaseContext>()
               .AddDefaultTokenProviders();

            builder.Services.AddCors(options =>
			{
				options.AddPolicy(corsTxt,
				builder =>
				{
					builder.AllowAnyOrigin();
					builder.AllowAnyMethod();
					builder.AllowAnyHeader();
				});
			});

            // Configure JWT authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };
            });

            builder.Services.AddScoped<UnitOfWOrks>();
            builder.Services.AddScoped<IUserServices,UserService>();

            #region Security code
            //builder.Services.AddAuthentication(option => option.DefaultAuthenticateScheme = "myscheme")
            //    .AddJwtBearer("myscheme",
            //    //validate token
            //    op =>
            //    {
            //        #region secret key
            //        string key = "welcome to my secret key mohamed elshafie";
            //        var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            //        #endregion
            //        op.TokenValidationParameters = new TokenValidationParameters()
            //        {
            //            IssuerSigningKey = secertkey,
            //            ValidateIssuer = false,
            //            ValidateAudience = false

            //        };
            //    }
            //    );
            #endregion

            builder.Services.AddHangfire(configuration => configuration
               .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
               .UseSimpleAssemblyNameTypeSerializer()
               .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangFire"),
                new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true,
                }
             ));
            builder.Services.AddHangfireServer();
            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				// app.MapSwagger().RequireAuthorization(op=>op.RequireRole("admin"));
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

			app.UseCors(corsTxt);

            app.MapControllers();
            //app.CreateDbIfNotExists();
            app.UseHangfireDashboard();
            app.Run();
        }
    }
}
// reinstall packages
// PM> Update-Package -ProjectName PriceComparing -Reinstall

// used Packages
// EFCore.AutomaticMigrations (8.0.0)
// Microsoft.EntityFrameworkCore (8.0.4)
// Microsoft.EntityFrameworkCore.Design (8.0.4)
// Microsoft.EntityFrameworkCore.Proxies (8.0.4)
// Microsoft.EntityFrameworkCore.Relational (8.0.4)
// Microsoft.EntityFrameworkCore.SqlServer (8.0.4)
// Microsoft.EntityFrameworkCore.Tools (8.0.4)
// Microsoft.EntityFrameworkCore.Metadata.Builders (8.0.4)
// Microsoft.AspNetCore.Mvc.NewtonsoftJson
// Swashbuckle.AspNetCore (6.4.0)
// PM> Install-Package EFCore.AutomaticMigrations -Version 8.0.0
// PM> Install-Package Microsoft.EntityFrameworkCore -Version 8.0.4
// PM> Install-Package Microsoft.EntityFrameworkCore.Design -Version 8.0.4
// PM> Install-Package Microsoft.EntityFrameworkCore.Proxies -Version 8.0.4
// PM> Install-Package Microsoft.EntityFrameworkCore.Relational -Version 8.0.4
// PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.4
// PM> Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.4
// PM> Install-Package Swashbuckle.AspNetCore -Version 6.4.0
// PM> Install-Package Microsoft.AspNetCore.Mvc.NewtonsoftJson
