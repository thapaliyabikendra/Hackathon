using System;

namespace Hackathon.Tournaments
{
    public class TournamentFilter
    {
        public string DisplayName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}