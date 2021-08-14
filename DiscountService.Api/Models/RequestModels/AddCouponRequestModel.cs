using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscountService.Api.Models.RequestModels
{
    public class AddCouponRequestModel
    {
        public string UserId { get; set; }

        public decimal DiscountAmount { get; set; }
    }
}
