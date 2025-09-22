using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using E_Commerc.web.Extensions;
using E_Commerc.web.Factories;
using E_Commerc.web.MiddleWares;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositories;
using Service;
using ServiceAbstraction;
using Shared.ErrorModels;

namespace E_Commerc.web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add services to the container
            builder.Services.AddControllers();

            #region My Functions
            builder.Services.AddSwaggerServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices(); 
            builder.Services.AddWebApplicationServices();
            builder.Services.AddJwtService(builder.Configuration);
            #endregion
            var app = builder.Build();

            await app.SeedDataBaseAsync(); //my function

            #region Configure the HTTP request pipeline
            app.UseWebCustomExceptionMiddleWares(); //my function

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWare(); //my function
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
