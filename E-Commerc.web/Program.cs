using E_Commerc.web.Extensions;
using Persistence;
using Service;

namespace E_Commerc.web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddCors(Options =>
            {
                Options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
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
            app.UseCors("AllowAll");  
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
