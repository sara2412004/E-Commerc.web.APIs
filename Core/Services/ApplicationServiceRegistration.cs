using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    //Helper class to register application services 
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            //..................auto mapper
            Services.AddAutoMapper(typeof(ProductService).Assembly);
            //........................Register service(servicmanager(services))
            Services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();
            
            Services.AddScoped<IproductService, ProductService>();
            Services.AddScoped<Func<IproductService>>(Provider=>() => Provider.GetRequiredService<IproductService>());

            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<Func<IAuthenticationService>>(Provider => () => Provider.GetRequiredService<IAuthenticationService>());

            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<Func<IBasketService>>(Provider => () => Provider.GetRequiredService<IBasketService>());

            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<Func<IOrderService>>(Provider => () => Provider.GetRequiredService<IOrderService>());

            Services.AddScoped<ICacheService, CacheService>();
            Services.AddScoped<Func<ICacheService>>(Provider => () => Provider.GetRequiredService<ICacheService>());

            return Services;
        }
    } 
}
