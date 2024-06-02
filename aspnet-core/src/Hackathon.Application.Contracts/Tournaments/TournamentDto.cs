using System;

namespace Hackathon.Tournaments
{
    public class TournamentDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreationTime { get; set; }
    }
}