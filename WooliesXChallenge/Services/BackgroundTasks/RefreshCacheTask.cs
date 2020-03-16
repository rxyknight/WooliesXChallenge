using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using WooliesXChallenge.Cache;
using WooliesXChallenge.Services.Interfaces;

namespace WooliesXChallenge.Services.BackgroundTasks
{
    public class RefreshCacheTask : IHostedService, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly IProductPopularityCache _cache;
        private readonly IPopularityService _productPopularityService;
        private Timer _timer;

        public RefreshCacheTask(IConfiguration configuration,
            IProductPopularityCache cache, 
            IPopularityService productPopularityService)
        {
            _configuration = configuration;
            _cache = cache;
            _productPopularityService = productPopularityService;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            double refreshTime = double.Parse(_configuration["CacheRefreshTime"]);
            Console.WriteLine($"Refresh cache task running. Refresh Time: {refreshTime} seconds");

            _timer = new Timer(RefreshCache, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(refreshTime));

            return Task.CompletedTask;
        }

        private void RefreshCache(object state)
        {
            Console.WriteLine("Start refreshing product popularity cache.");
            _cache.Refresh(_productPopularityService.GetPolularityTable());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Refresh cache task is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
