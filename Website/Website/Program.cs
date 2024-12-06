using Website.Client.Pages;
using Website.Components;
using Microsoft.AspNetCore.ResponseCompression;
using Website.Hubs;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using System.Diagnostics;
using Entities;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Website.Client.Data;
using Services;
namespace Website
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();
            builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IGenericRepository<Entities.Activity>, GenericRepository<Entities.Activity>>();
            builder.Services.AddScoped<IGenericRepository<Family>, GenericRepository<Family>>();
            builder.Services.AddScoped<IGenericRepository<CustomList>, GenericRepository<CustomList>>();
            builder.Services.AddScoped<IGenericRepository<CustomListItem>, GenericRepository<CustomListItem>>();
            builder.Services.AddSingleton(new SignalRService());
            builder.Services.AddSingleton(new SessionToken());
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.WithOrigins("https://localhost:7139/") // Add your client URL
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            builder.Services.AddSignalR();

            builder.Services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    ["application/octet-stream"]);
            });
            
            var app = builder.Build();
            app.UseResponseCompression();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);
            app.UseCors("AllowSpecificOrigin");
            app.MapHub<HomeSyncHub>("/homesynchub");
            app.Run();
        }
    }
}
