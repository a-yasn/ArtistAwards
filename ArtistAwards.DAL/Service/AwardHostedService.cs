using ArtistAwards.DAL.Interface;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArtistAwards.DAL.Service
{
    public class AwardHostedService : IHostedService, IDisposable
    {
        private readonly ILogger logger;
        private  IAwardService awardService;
        private Timer timer;

        public AwardHostedService(ILogger<AwardHostedService> logger, IAwardService awardService)
        {
            this.logger = logger;
            this.awardService = awardService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Award Background Service is starting.");

            timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            logger.LogInformation("Award Background Service is working.");

            var awards = awardService.GetAll();
            var year = awards.Max(x => x.Year);

            year++;

            awardService.Update(new Domain.Award {
                Name = $"Grammy of {year}",
                Role = "Mister X",
                Show = $"Undergound Show of {year}",
                Year = year
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Award Background Service is stopping.");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
