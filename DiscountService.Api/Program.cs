using DiscountService.Domain.DomainModel.DiscountDomainModel;
using DiscountService.Persistence;
using Microservice.Framework.Common;
using Microservice.Framework.Domain.Aggregates;
using Microservice.Framework.Persistence.EFCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DiscountService.Api
{
    public class Program
    {
        private static IAggregateStore _aggregateStore;

        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                _aggregateStore = scope.ServiceProvider.GetRequiredService<IAggregateStore>();

                var db = scope.ServiceProvider.GetRequiredService<IDbContextProvider<DiscountContext>>();
                db.CreateContext().Database.Migrate();

                await Task.WhenAll(Coupons.GetCoupons().Select(AddCouponsAsync));
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private async static Task UpdateAsync<TAggregate, TIdentity>(TIdentity id, Action<TAggregate> action)
            where TAggregate : class, IAggregateRoot<TIdentity>
            where TIdentity : IIdentity
        {
            await _aggregateStore.UpdateAsync<TAggregate, TIdentity>(
                id,
                SourceId.New,
                (a, c) =>
                {
                    action(a);
                    return Task.FromResult(0);
                },
                CancellationToken.None);
        }

        public static Task AddCouponsAsync(Coupon coupon)
        {
            return UpdateAsync<Coupon, CouponId>(coupon.Id, a => a.AddCoupon(coupon.UserId, coupon.DiscountAmount));
        }
    }

    public static class Coupons
    {
        public static readonly Coupon Coupon1 = new Coupon(CouponId.New) 
        {
            UserId = Guid.NewGuid().ToString(),
            DiscountAmount = 67
        };

        public static readonly Coupon Coupon2 = new Coupon(CouponId.New)
        {
            UserId = Guid.NewGuid().ToString(),
            DiscountAmount = 96
        };

        public static IEnumerable<Coupon> GetCoupons()
        {
            var fieldInfos = typeof(Coupons).GetFields(BindingFlags.Public | BindingFlags.Static);
            return fieldInfos.Select(fi => (Coupon)fi.GetValue(null));
        }
    }
}
