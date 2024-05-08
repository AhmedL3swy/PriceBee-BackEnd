using DataAccess;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore.SqlServer;
using PriceComparing.UnitOfWork;
using PriceComparing.AutoMigration;

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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DBContext>(o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DeafultConnection")));

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
// Swashbuckle.AspNetCore (6.4.0)
// PM> Install-Package EFCore.AutomaticMigrations -Version 8.0.0
// PM> Install-Package Microsoft.EntityFrameworkCore -Version 8.0.4
// PM> Install-Package Microsoft.EntityFrameworkCore.Design -Version 8.0.4
// PM> Install-Package Microsoft.EntityFrameworkCore.Proxies -Version 8.0.4
// PM> Install-Package Microsoft.EntityFrameworkCore.Relational -Version 8.0.4
// PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.4
// PM> Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.4
// PM> Install-Package Swashbuckle.AspNetCore -Version 6.4.0
