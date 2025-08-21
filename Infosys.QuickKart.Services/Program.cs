
using Infosys.QuickKart.DAL.Models;
using Infosys.QuickKart.DAL;
using Microsoft.EntityFrameworkCore;

namespace Infosys.QuicKart.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddSingleton<QuickKartRepository>(new QuickKartRepository(new QuickKartDBContext()));
            builder.Services.AddSingleton<QuickKartRepository>(new QuickKartRepository(new QuickKartDbContext(new DbContextOptions<QuickKartDbContext>())));


            var app = builder.Build();

            app.UseCors(
                options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
            );

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
