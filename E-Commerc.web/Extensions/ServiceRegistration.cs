using E_Commerc.web.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace E_Commerc.web.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();

            Services.AddSwaggerGen(Options => 
            {
            Options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                Description = "Enter 'Bearer' Followed By Space and Your Token"
            });  

            Options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference=new OpenApiReference()
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
            });
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
        public static IServiceCollection AddJwtService(this IServiceCollection Services, IConfiguration _configuration)
        {
            Services.AddAuthentication(configureOptions => {
                configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                  }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JwtOptions:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["JwtOptions:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JwtOptions:SecretKey"])),
                };
            });
            return Services;
        }
    }   
}
