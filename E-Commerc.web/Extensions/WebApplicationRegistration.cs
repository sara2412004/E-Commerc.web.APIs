using DomainLayer.Contracts;
using E_Commerc.web.MiddleWares;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json;

namespace E_Commerc.web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            using var Scoope = app.Services.CreateScope(); // 3lshan 3iza a5ly el continar da ygebly service mo3ina w create mnha object
            var ObjectOfDataSeeding = Scoope.ServiceProvider.GetRequiredService<IDataSeeding>();// GetRequiredService:bta5od el type bt3 el service w trg3o lia(service b implement el interface idataseeding)
            await ObjectOfDataSeeding.DataSeedAsync();//DataSeed el function ely ana 3mlha fel class (DataSeeding)
            await ObjectOfDataSeeding.IdentityDataSeedAsync();// IdentityDataSeed el function ely ana 3mlha fel class(DataSeeding)
        }
        public static IApplicationBuilder UseWebCustomExceptionMiddleWares(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            return app;
        }
        public static IApplicationBuilder UseSwaggerMiddleWare(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.ConfigObject = new ConfigObject()
                {
                    DisplayRequestDuration = true//3lshan y3rd l duration bta3t el request
                };

                options.DocumentTitle = "My E-Commerce API";// title ely byb2a fo2 fl page tab

                options.JsonSerializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase//3lshan yb2a el json bta3y camelCase
                };

                options.DocExpansion(DocExpansion.None);//3lshan yb2a el endpoints kollha m2fola lma a3ml open ll swagger page
                options.EnableFilter();// search bar adwar 3la ay endpoint
                options.EnablePersistAuthorization();//mkan el token  .. w m7taga configur 2 services fe AddSwaggerGen fel servicesRegistration 
            });
            return app;
        }

    }
}
