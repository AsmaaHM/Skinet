using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Skinet.Core;
using Skinet.Core.Entities.OrderAggregate;
using System;
using System.Linq;
using System.Reflection;

namespace Skinet.Infrastructure
{
	public class SkinetContext : DbContext
	{
		public SkinetContext(DbContextOptions<SkinetContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                    var dateTimeProperties = entityType.ClrType.GetProperties()
                        .Where(p => p.PropertyType == typeof(DateTimeOffset));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }

                    foreach (var property in dateTimeProperties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }

        }

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductBrand> Brands { get; set; }
		public DbSet<ProductType> Types { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItemss { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    }
}
