using Microservice.Framework.Domain;
using Microservice.Framework.Domain.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiscountService.Domain
{
    public static class DiscountServiceDomainExtensions
    {
        public static Assembly Assembly { get; } = typeof(DiscountServiceDomainExtensions).Assembly;

        public static IDomainContainer ConfigureDiscountServiceDomain(
            this IServiceCollection services)
        {
            return DomainContainer.New(services)
                .AddDefaults(Assembly);
        }
    }
}
