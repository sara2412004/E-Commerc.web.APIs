using DomainLayer.Contracts;
using E_Commerc.web.MiddleWares;

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
            app.UseSwaggerUI();
            return app;
        }

    }
}
