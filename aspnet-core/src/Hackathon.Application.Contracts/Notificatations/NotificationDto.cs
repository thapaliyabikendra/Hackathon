using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Hackathon.Notificatations
{
    public class NotificationDto:FullAuditedEntityDto<Guid>
    {
        public string Message { get; set; }
        public string NotificationType { get; set; }
        public bool Seen { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReciverUserId { get; set; } //fk AbpUserId
        public string? SenderUserName { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}
