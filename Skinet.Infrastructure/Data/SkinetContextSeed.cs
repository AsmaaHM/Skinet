using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Skinet.Core;
using Microsoft.Extensions.Logging;
using Skinet.Infrastructure;

namespace Infrastructure.Data
{
    public class SkinetContextSeed
    {
        public static async Task SeedAsync(SkinetContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (!context.Brands.Any())
                {
                    var brandsData = File.ReadAllText(path + @"/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                    {
                        context.Brands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Types.Any())
                {
                    var typesData = File.ReadAllText(path + @"/Data/SeedData/Types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        context.Types.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText(path + @"/Data/SeedData/Products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<SkinetContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}