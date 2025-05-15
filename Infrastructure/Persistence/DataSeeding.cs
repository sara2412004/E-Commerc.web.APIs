using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbcontext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            ////bt2ked en kol el migrations done el awal 
            try
            {
                if ( (await _dbcontext.Database.GetPendingMigrationsAsync()).Any()) 
                {
                     await _dbcontext.Database.MigrateAsync();
                }
                //bshof fe ay data fe table el product wala la 3lshan el data seeding bt7sl mara wa7da bs fel awal 
                if (!_dbcontext.Products.Any()) 
                {
                    //1.Read Data
                    var ProductData = File.OpenRead(@"..\Infrastructure\Persistence\DataSeed\products.json");
                   //2.convert Data "string" =>c# objects
                   var Products=await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);//ProductData da ely h7walo l =>List<Product>
                    //3.Save to DB
                    if (Products is not null || Products.Any())
                    {
                       await _dbcontext.Products.AddRangeAsync(Products);//de el table bt3y el dbset <Products>
                    }

                    //4.hro7 add scoop fel program
                 }

                if (!_dbcontext.ProductBrands.Any())
                {
                    //1.Read Data
                    var ProductBrandData = File.OpenRead(@"..\Infrastructure\Persistence\DataSeed\brands.json");
                    //2.convert Data "string" =>c# objects
                    var ProductBrands =await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);//ProductData da ely h7walo l =>List<Product>
                    //3.Save to DB
                    if (ProductBrands is not null || ProductBrands.Any())
                    {
                       await _dbcontext.ProductBrands.AddRangeAsync(ProductBrands);//_dbcontext.ProductBrands (DbSet)
                    }

                    //4.hro7 add scoop fel program
                }
                if(!_dbcontext.ProductTypes.Any())//bshof en mfhosh ay data
                {
                    //1-ReadData
                    var TypeData=File.OpenRead(@"..\Infrastructure\Persistence\DataSeed\types.json");
                    //2-Convert el string to C#
                    var productTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(TypeData);
                    //3-Save to Db
                    if (productTypes is not null || productTypes.Any())
                    {
                        _dbcontext.ProductTypes.AddRangeAsync(productTypes);
                    }
                }

               await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //TODoooooooooooooooooooooooooooooooooooooo
            }
            
            
        }
    }
}
