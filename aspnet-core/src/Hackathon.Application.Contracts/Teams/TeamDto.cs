using System;

namespace Hackathon.Teams
{
    public class TeamDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreationTime { get; set; }
    }
}