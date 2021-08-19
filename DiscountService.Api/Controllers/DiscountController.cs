using DiscountService.Api.Models.RequestModels;
using DiscountService.Domain.DomainModel.DiscountDomainModel;
using DiscountService.Domain.DomainModel.DiscountDomainModel.Commands;
using DiscountService.Domain.DomainModel.DiscountDomainModel.Queries;
using Microservice.Framework.Common;
using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiscountService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public DiscountController(
            ICommandBus commandBus,
            IQueryProcessor queryProcessor
            )
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        [HttpPost("addCoupon")]
        public async Task<IActionResult> AddService(AddCouponRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var addCouponResult = await _commandBus
                    .PublishAsync(
                        new AddCouponCommand(CouponId.New, model.UserId, model.DiscountAmount),
                        CancellationToken.None);

                if (addCouponResult.IsSuccess)
                    return Ok(addCouponResult);
                else
                    return BadRequest(addCouponResult);
            }
            else
            {
                return BadRequest(ModelState.Values);
            }
        }

        [HttpGet("getCoupon")]
        public async Task<IActionResult> GetCoupon([FromQuery]GetCouponRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var couponId = model.CouponId.IsNotNullOrEmpty()
                    ? new CouponId(model.CouponId) : null;

                var couponResult = await _queryProcessor
                    .ProcessAsync(
                        new GetCouponQuery(couponId, model.UserId),
                        CancellationToken.None);

                return Ok(couponResult);
            }
            else
            {
                return BadRequest(ModelState.Values);
            }
        }
    }
}
