
//using Day2.UnitOfWork;
using Price_Comparison.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore.SqlServer;
using Price_Comparison.UnitOfWork;
using Price_Comparison._ِAutoMigration;

namespace Price_Comparison
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string corsTxt = "";

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ProductComparingDBContext>(o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DeafultConnection")));

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

            builder.Services.AddScoped<UnitOfWOrks>();

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


			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                // app.MapSwagger().RequireAuthorization(op=>op.RequireRole("admin"));
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(corsTxt);

            app.MapControllers();
            app.CreateDbIfNotExisi();

			app.Run();
        }
    }
}
