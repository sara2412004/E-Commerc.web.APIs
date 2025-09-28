using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositories;
using StackExchange.Redis;
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
            //......................... b3ml el configruation w el connection  for identity
            Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });
            //................Data Seeding
            Services.AddScoped<IDataSeeding, DataSeeding>();

            //.................Register service(Unit Of Work(reposirys))
           Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //..................Register Identity for user and role managment (ely fel Data seeding) 
            Services.AddIdentityCore<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDbContext>();
            //..................Register Basket Repositroy
            Services.AddScoped<IBasketRepository, BasketRepositroy>();
            Services.AddSingleton<IConnectionMultiplexer>((_)=>
            {
               return   ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnectionString"));   

            });
            return Services;

        }
    }
}
