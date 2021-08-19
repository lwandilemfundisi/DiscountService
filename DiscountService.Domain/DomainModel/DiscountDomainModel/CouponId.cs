using Microservice.Framework.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountService.Domain.DomainModel.DiscountDomainModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class CouponId : Identity<CouponId>
    {
        public CouponId(string value)
            : base(value)
        {

        }
    }
}
