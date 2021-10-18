using DiscountService.Domain.DomainModel.DiscountDomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscountService.Api.Models.RequestModels
{
    public class GetCouponRequestModel
    {
        public CouponId CouponId { get; set; }

        public string UserId { get; set; }
    }
}
