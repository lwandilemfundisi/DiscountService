using DiscountService.Domain.DomainModel.DiscountDomainModel;
using DiscountService.Persistence.ValueObjectConverters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountService.Persistence.Mappings
{
    public static class CouponModelMapping
    {
        public static ModelBuilder CouponModelMap(this ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<Coupon>()
            .Property(o => o.Id)
            .HasConversion(new SingleValueObjectIdentityValueConverter<CouponId>());

            return modelBuilder;
        }
    }
}
