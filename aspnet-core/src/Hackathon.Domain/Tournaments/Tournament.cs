using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hackathon.Tournaments
{
    public class Tournament : FullAuditedAggregateRoot<Guid>
    {
        public string DisplayName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}