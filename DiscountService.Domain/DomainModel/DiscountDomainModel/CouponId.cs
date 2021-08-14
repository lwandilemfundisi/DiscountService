using Microservice.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountService.Domain.DomainModel.DiscountDomainModel
{
    public class CouponId : Identity<CouponId>
    {
        public CouponId(string value)
            : base(value)
        {

        }
    }
}
