using Microservice.Framework.Domain.Events;
using Microservice.Framework.Domain.Events.AggregateEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountService.Domain.DomainModel.DiscountDomainModel.Events
{
    [EventVersion("AddedCouponEvent", 1)]
    public class AddedCouponEvent : AggregateEvent<Coupon, CouponId>
    {
        public AddedCouponEvent(
            string userId, 
            decimal discountAmount)
        {
            UserId = userId;
            DiscountAmount = discountAmount;
        }

        public string UserId { get; }
        public decimal DiscountAmount { get; set; }
    }
}
