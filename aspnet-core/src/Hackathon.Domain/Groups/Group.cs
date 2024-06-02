using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hackathon.Groups
{
    public class Group : FullAuditedAggregateRoot<Guid>
    {
        public Guid TournamentId { get; set; }
        public string GroupName { get; set; }
    }
}