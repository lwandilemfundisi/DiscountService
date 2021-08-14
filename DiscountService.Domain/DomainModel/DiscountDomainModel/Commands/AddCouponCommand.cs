using Microservice.Framework.Domain.Commands;
using Microservice.Framework.Domain.ExecutionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscountService.Domain.DomainModel.DiscountDomainModel.Commands
{
    public class AddCouponCommand : Command<Coupon, CouponId>
    {
        public AddCouponCommand(
            CouponId id,
            string userId,
            decimal discountAmount)
            : base(id)
        {
            UserId = userId;
            DiscountAmount = discountAmount;
        }

        public string UserId { get; }
        public decimal DiscountAmount { get; set; }
    }

    public class AddCouponCommandHandler : CommandHandler<Coupon, CouponId, AddCouponCommand>
    {
        public override Task<IExecutionResult> ExecuteAsync(
            Coupon aggregate, 
            AddCouponCommand command, 
            CancellationToken cancellationToken)
        {
            aggregate.AddCoupon(command.UserId, command.DiscountAmount);

            return Task.FromResult(ExecutionResult.Success());
        }
    }
}
