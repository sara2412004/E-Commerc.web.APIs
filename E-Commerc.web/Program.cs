using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Service;
using ServiceAbstraction;

namespace E_Commerc.web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Add services to the container

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //......................... b3ml el configruation w el connection 
            builder.Services.AddDbContext<StoreDbContext>(options =>
              {
                  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
              }); 
            //................Data Seeding
            builder.Services.AddScoped<IDataSeeding,DataSeeding>();
            //.................Register service(Unit Of Work(reposirys))
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //..................auto mapper
            builder.Services.AddAutoMapper(typeof(ProductService).Assembly);
            //........................Register service(servicmanager(services))
            builder.Services.AddScoped<IServiceManager,ServiceManager>();

            #endregion
            var app = builder.Build();

            #region Data Seeding
            using var Scoope = app.Services.CreateScope(); // 3lshan 3iza a5ly el continar da ygebly service mo3ina w create mnha object
            var ObjectOfDataSeeding = Scoope.ServiceProvider.GetRequiredService<IDataSeeding>();// GetRequiredService:bta5od el type bt3 el service w trg3o lia(service b implement el interface idataseeding)
            ObjectOfDataSeeding.DataSeedAsync();//DataSeed el function ely ana 3mlha fel class 
            #endregion


            #region Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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
