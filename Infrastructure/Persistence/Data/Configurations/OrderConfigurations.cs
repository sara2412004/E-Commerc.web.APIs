using DomainLayer.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");//b8er el table name bs
            builder.Property(O => O.Subtotal)
                      .HasColumnType("decimal(8,2)");
            builder.HasMany(O => O.Items)
                         .WithOne();
            builder.HasOne(O => O.DeliveryMethod)
                       .WithMany()
                       .HasForeignKey(O => O.DeliveryMethodId);
            //ownsOne hena hy5li class el Address yt5zn aw yb2a go2z mn el Order table y3ni el colums bt3to hyb2o fe table el Orders
            //b3ml kda 3shan el Address msh entity 3ady hwa value object
            //value object : didn't have identity of its own, they are defined by their properties
            builder.OwnsOne(O => O.Address);
                          
                          
        }
    }
}
