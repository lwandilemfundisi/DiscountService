using DiscountService.Domain.DomainModel.DiscountDomainModel.Events;
using Microservice.Framework.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountService.Domain.DomainModel.DiscountDomainModel
{
    public class Coupon : AggregateRoot<Coupon, CouponId>
    {
        #region Constructors

        public Coupon()
            : base(null)
        {

        }

        public Coupon(CouponId id)
            : base(id)
        {

        }

        #endregion

        #region Properties

        public string UserId { get; set; }

        public decimal DiscountAmount { get; set; }

        #endregion

        #region Methods

        public void AddCoupon(
            string userId, 
            decimal discountAmount)
        {
            UserId = userId;
            DiscountAmount = discountAmount;

            Emit(new AddedCouponEvent(userId, discountAmount));
        }

        #endregion
    }
}
