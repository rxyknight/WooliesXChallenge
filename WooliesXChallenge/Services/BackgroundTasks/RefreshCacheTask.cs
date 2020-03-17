using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using WooliesXChallenge.Cache;
using WooliesXChallenge.Services.Interfaces;

namespace WooliesXChallenge.Services.BackgroundTasks
{
    //
    // Summary:
    //      This class provides a background task to update the popularity cache periodically.  
    //      the cron job timer can be set via "CacheRefreshTime" in appsetting.json
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

        //
        // Summary:
        //     Triggered when the application host is ready to start the service.
        //
        // Parameters:
        //   cancellationToken:
        //     Indicates that the start process has been aborted.
        public Task StartAsync(CancellationToken cancellationToken)
        {
            double refreshTime = double.Parse(_configuration["CacheRefreshTime"]);
            Console.WriteLine($"Refresh cache task running. Refresh Time: {refreshTime} seconds");

            _timer = new Timer(RefreshCache, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(refreshTime));

            return Task.CompletedTask;
        }
        //
        // Summary:
        //      A delegate representing a method to be executed to update the popularity cache.
        private void RefreshCache(object state)
        {
            Console.WriteLine("Start refreshing product popularity cache.");
            _cache.Refresh(_productPopularityService.GetPolularityTable());
        }

        //
        // Summary:
        //     Triggered when the application host is performing a graceful shutdown.
        //
        // Parameters:
        //   cancellationToken:
        //     Indicates that the shutdown process should no longer be graceful.
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Refresh cache task is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
        //
        // Summary:
        //      release timer
        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
