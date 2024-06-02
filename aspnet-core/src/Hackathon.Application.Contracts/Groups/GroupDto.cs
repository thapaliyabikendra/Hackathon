using System;

namespace Hackathon.Groups
{
    public class GroupDto
    {
        public Guid Id { get; set; }
        public Guid TournamentId { get; set; }
        public string GroupName { get; set; }
        public DateTime CreationTime { get; set; }
    }
}