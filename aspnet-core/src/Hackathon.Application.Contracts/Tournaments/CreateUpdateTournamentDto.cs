using System;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Tournaments
{
    public class CreateUpdateTournamentDto
    {
        public string DisplayName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}