using Hackathon.Notificatations;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Hackathon
{
    public class Hangfire : ApplicationService
    {
        private readonly INotificationService _notificationService;
        public Hangfire(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public void ConfigureRecurringJob()
        {
            BackgroundJob.Enqueue<INotificationService>(x=>x.SendNotification());
            RecurringJob.AddOrUpdate<INotificationService>("SendNotification",x=>x.SendNotification(), Cron.MinuteInterval(1));
        }

        [AutomaticRetry(Attempts = 3)] // Optionally, configure retry attempts
        public async Task SendNotificationAsync()
        {
            // Your notification sending logic here
            await _notificationService.SendNotification();
        }
    }
}
