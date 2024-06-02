using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hackathon.Notifications
{
    public class Notification:FullAuditedAggregateRoot<Guid>
    {
        public string Message { get; set; }
        public string NotificationType { get; set; }
        public Guid ReciverUserId { get; set; } //fk AbpUserId
        
    }
}
