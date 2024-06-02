using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Hackathon.Notificatations
{
    public interface INotificationService : IApplicationService
    {
        Task<bool> SendNotification();
    }
}
