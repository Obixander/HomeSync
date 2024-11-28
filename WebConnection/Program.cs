
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace WebConnection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IGenericRepository<Family>, GenericRepository<Family>>();
            builder.Services.AddScoped<IGenericRepository<CustomListItem>, GenericRepository<CustomListItem>>();
            builder.Services.AddScoped<IGenericRepository<CustomList>, GenericRepository<CustomList>>();
            builder.Services.AddScoped<IGenericRepository<Activity>, GenericRepository<Activity>>();

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
