
using Infosys.DBFirstCore.DataAccessLayer;
using Infosys.DBFirstCore.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Infosys.DBFirstCore.DataAccessLayer.Models;
using Infosys.DBFirstCore.DataAccessLayer;


namespace PricingServices
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
            builder.Services.AddTransient<PricingDbContext>();
            builder.Services.AddTransient<PricingRepository>(
               c => new PricingRepository(c.GetRequiredService<PricingDbContext>()));


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
