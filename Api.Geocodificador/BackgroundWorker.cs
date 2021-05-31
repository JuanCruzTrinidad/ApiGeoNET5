using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Geocodificador.Services.Interface;
using Api.Geocodificador.Models;

namespace Api.Geocodificador
{
    public class BackgroundWorker : BackgroundService
    {
        private readonly IBackgroundQueue<Process> _queue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<BackgroundWorker> _logger;
        public BackgroundWorker(IBackgroundQueue<Process> queue, IServiceScopeFactory scopeFactory,
            ILogger<BackgroundWorker> logger)
        {
            _queue = queue;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Geocoding  is now running in the background.");
            await BackgroundProcessing(stoppingToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogCritical( "The Geocoding is stopping due to a host shutdown, queued items might not be processed anymore.",
                nameof(BackgroundWorker)
            );

            return base.StopAsync(cancellationToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(500, stoppingToken); 
                    var queueItem = _queue.Dequeue(); 
                    if (queueItem == null) continue; 

                    _logger.LogInformation("Excel found! Starting to process ..");

                    using (var scope = _scopeFactory.CreateScope()) 
                    {
                        //Servicios requeridos.
                        IGeocodingServices geocodingServices = scope.ServiceProvider.GetRequiredService<IGeocodingServices>();
                        await geocodingServices.Geocoding(queueItem);
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogCritical("An error occurred when Geocoding. Exception: {@Exception}", ex);
                }
            }
        }
    }


}
