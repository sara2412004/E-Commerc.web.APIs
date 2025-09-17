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
            Services.AddScoped<IServiceManager, ServiceManager>();
            return Services;
        }
    }
}
