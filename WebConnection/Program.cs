
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services;
using System.Text.Json.Serialization;
using WebConnection.Hubs;

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
            builder.Services.AddSignalR();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
            builder.Services.AddScoped<IFamilyRepository, FamilyRepository>();
            builder.Services.AddScoped<ICustomListRepository, CustomListRepository>();

            builder.Services.AddScoped<IGenericRepository<CustomListItem>, GenericRepository<CustomListItem>>();
            builder.Services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            builder.Services.AddControllers() //fixes some ef issues somehow?? (cycle depth of 32 or more)
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(7139); // Ensure the app listens on port 7139
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazorApp", builder =>
                {
                    builder.WithOrigins("https://localhost:7144", "https://localhost:7256", "https://localhost:8000", "http://localhost:8000")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });


            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();
            app.UseCors("AllowBlazorApp");
            app.MapHub<HomeSyncHub>("/homesynchub");
            app.Run();
        }
    }
}
