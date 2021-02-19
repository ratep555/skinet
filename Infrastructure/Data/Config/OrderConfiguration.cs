using System;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //hover over ownsone
            builder.OwnsOne(o => o.ShipToAddress, a => 
            {
                a.WithOwner();
                //here we will not place isrequired etc...because we are relying on dto
                //to handle validation
            });
            builder.Property(s => s.Status)
            //we want to convert our enum to a string rather then integer
            //hover over hasconversion
                .HasConversion(
                    o => o.ToString(),
                    o => (OrderStatus) Enum.Parse(typeof(OrderStatus), o)
                );
        //this ensures that when we delete an order, we delete related orderitems at the same time
                builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}