using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hackathon.Stadiums
{
    public class Stadium : FullAuditedAggregateRoot<Guid>
    {
        public string DisplayName { get; set; }
        public string Location { get; set; }
        public string TimeZoneId { get; set; }
        public int TimeZoneDstOffset { get; set; }
        public int TimeZoneRawOffset { get; set; }
    }
}