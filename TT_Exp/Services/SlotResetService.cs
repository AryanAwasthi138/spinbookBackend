using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TableTennisBooking.Data;

namespace TableTennisBooking.Services
{
    public class SlotResetService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public SlotResetService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (DateTime.Now.Hour == 0) // Reset at midnight
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                        var slots = context.Slots.Where(s => s.IsBooked).ToList();

                        foreach (var slot in slots)
                        {
                            slot.IsBooked = false;
                        }

                        context.SaveChanges();
                    }
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
