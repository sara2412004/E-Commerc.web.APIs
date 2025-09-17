using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices (this IServiceCollection Services, IConfiguration configuration)
        {
            //......................... b3ml el configruation w el connection 
           Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            //................Data Seeding
            Services.AddScoped<IDataSeeding, DataSeeding>();

            //.................Register service(Unit Of Work(reposirys))
           Services.AddScoped<IUnitOfWork, UnitOfWork>();
            return Services;
        }
    }
}
