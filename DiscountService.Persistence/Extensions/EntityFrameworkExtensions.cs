using Microservice.Framework.Domain;
using Microservice.Framework.Domain.Extensions;
using Microservice.Framework.Persistence;
using Microservice.Framework.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountService.Persistence.Extensions
{
    public static class EntityFrameworkExtensions
    {
        public static IDomainContainer ConfigureDiscountPersistence(
            this IDomainContainer domainContainer,
            IConfiguration configuration)
        {
            return domainContainer
                .ConfigureEntityFramework(EntityFrameworkConfiguration.New)
                .AddDbContextProvider<DiscountContext, DiscountContextProvider>()
                .RegisterServices(sr =>
                {
                    sr.AddTransient<IPersistenceFactory, EntityFrameworkPersistenceFactory<DiscountContext>>();
                    sr.AddSingleton(rctx => { return configuration; });
                });
        }
        
        public static IDomainContainer ConfigureEntityFramework(
            this IDomainContainer domainContainer,
            IEntityFrameworkConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            configuration.Apply(domainContainer.ServiceCollection);
            return domainContainer;
        }

        public static IDomainContainer AddDbContextProvider<TDbContext, TContextProvider>(
            this IDomainContainer domainContainer)
            where TContextProvider : class, IDbContextProvider<TDbContext>
            where TDbContext : DbContext
        {
            domainContainer
                .ServiceCollection
                .AddTransient<IDbContextProvider<TDbContext>, TContextProvider>();

            return domainContainer;
        }
    }
}
