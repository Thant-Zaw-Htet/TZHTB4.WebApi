
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using TZHTB4.WebApi.Database.AppDbContextModels;
using TZHTB4.WebApi.Service;

namespace TZHTB4.WebApi
{
    public class Program
    {
        public static void Main(string[] args, ProductService productService)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")
            ));
            builder.Services.AddScoped<IProductService, ProductService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
