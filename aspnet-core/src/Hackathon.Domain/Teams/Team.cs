using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hackathon.Teams
{
    public class Team : FullAuditedAggregateRoot<Guid>
    {
        public string DisplayName { get; set; }
    }
}