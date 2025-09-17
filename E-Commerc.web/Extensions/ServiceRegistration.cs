using E_Commerc.web.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerc.web.Extensions
{
    public static class ServiceRegistration
    {
        public static  IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {


            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();



            return Services;
        }
        public static IServiceCollection AddWebApplicationServices(this IServiceCollection Services)
        {
            #region Validation Error Response Configuration
            Services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorsResponse;
            });
            #endregion

            return Services;
        }
    }
}
