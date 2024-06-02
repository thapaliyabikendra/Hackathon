using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hackathon.Matchs
{
    public class Match : FullAuditedAggregateRoot<Guid>
    {
        public Guid TournamentId { get; set; }
        public Guid StadiumId { get; set; }
        public Guid? GroupId { get; set; }
        public Guid TeamAId { get; set; }
        public Guid TeamBId { get; set; }
        public DateTime MatchDate { get; set; }
        public int? TeamAScore { get; set; }
        public int? TeamBScore { get; set; }
    }
}