using Microservice.Framework.Domain.Queries;
using Microservice.Framework.Persistence;
using Microservice.Framework.Persistence.EFCore.Queries.CriteriaQueries;
using Microservice.Framework.Persistence.EFCore.Queries.Filtering;
using Microservice.Framework.Persistence.Extensions;
using Microservice.Framework.Persistence.Queries.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscountService.Domain.DomainModel.DiscountDomainModel.Queries
{
    public class GetCouponQuery
        : EFCoreCriteriaDomainQuery<Coupon>, IQuery<Coupon>
    {
        #region Constructors

        public GetCouponQuery(
            CouponId couponId,
            string userId)
        {
            Id = couponId;
            UserId = userId;
        }

        #endregion

        #region Properties

        public string UserId { get; set; }

        #endregion

        #region Virtual Methods

        protected override void OnBuildDomainCriteria(EFCoreDomainCriteria domainCriteria)
        {
            domainCriteria.SafeAnd(new EqualityFilter("UserId", UserId, FilterType.Equal));
        }

        #endregion
    }

    public class GetCouponByIdQueryHandler
        : EFCoreCriteriaDomainQueryHandler<Coupon>, IQueryHandler<GetCouponQuery, Coupon>
    {
        #region Constructors

        public GetCouponByIdQueryHandler(IPersistenceFactory persistenceFactory)
            : base(persistenceFactory)
        {
        }

        #endregion

        #region IQueryHandler Members

        public async Task<Coupon> ExecuteQueryAsync(
            GetCouponQuery query, 
            CancellationToken cancellationToken)
        {
            return await Find(query);
        }

        #endregion
    }
}
