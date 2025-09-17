using DomainLayer.Contracts;
using E_Commerc.web.Extensions;
using E_Commerc.web.Factories;
using E_Commerc.web.MiddleWares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Service;
using ServiceAbstraction;
using Shared.ErrorModels;

namespace E_Commerc.web
{
    public class Program
    {
        public static async void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add services to the container
            builder.Services.AddControllers();

            #region My Functions
            builder.Services.AddSwaggerServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices(); 
            builder.Services.AddWebApplicationServices();
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


            app.UseAuthorization();
            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
