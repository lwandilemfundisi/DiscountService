using Microservice.Framework.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountService.Persistence
{
    public class DiscountContextProvider : IDbContextProvider<DiscountContext>, IDisposable
    {
        private readonly DbContextOptions<DiscountContext> _options;

        public DiscountContextProvider(IConfiguration configuration)
        {
            _options = new DbContextOptionsBuilder<DiscountContext>()
                .UseSqlServer(configuration["DataConnection:Database"])
                .Options;
        }

        public DiscountContext CreateContext()
        {
            return new DiscountContext(_options);
        }

        public void Dispose()
        {
        }
    }
}
