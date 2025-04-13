using Domain.Contract;
using Domain.Entities;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence
{
    public class DbInitializer : IDbInitializer 
    {
        private readonly StoreDbContext _context;
        public DbInitializer(StoreDbContext context)
        {
            _context = context;

        }

        public async Task InitializeAsync()
        {

            //if(_context.Database.GetPendingMigrations().Any())
            //{
            //    _context.Database.Migrate();
            //}

            if (!_context.ProductTypes.Any())
            {
                var typeData = File.ReadAllText(@"..\Persistence\Data\Seeding\types.json");

                var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);

                if (types is not null && types.Any())
                {
                   await _context.ProductTypes.AddRangeAsync(types);
                   await  _context.SaveChangesAsync();
                }
            }

            if (!_context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText(@"..\Persistence\Data\Seeding\brands .json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands is not null && brands.Any())
                {
                   await _context.ProductBrands.AddRangeAsync(brands);
                   await _context.SaveChangesAsync();
                }
            }

            if (!_context.Products.Any())
            {
                var ProductsData = File.ReadAllText(@"..\Persistence\Data\Seeding\products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (products is not null && products.Any())
                {
                   await _context.Products.AddRangeAsync(products);
                   await  _context.SaveChangesAsync();
                }
            }
        }

        
    }
}
