using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skinet.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Config
{
	public class OrderConfig : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.OwnsOne(o => o.ShipToAddress, a => a.WithOwner());
			builder.Property(s => s.Status).HasConversion(o => 
			o.ToString(),
			o => (OrderStatus)Enum.Parse(typeof(OrderStatus),
			o));
			builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

		}
	}
}
